using Newtonsoft.Json;
using SiMed.Clinic;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace ClinicScheduler.EmployerWebTasks
{
    public class EmployerWebTaskExportSelectedResults_Helper
    {
        public static bool DoEmployerWebTask(EmployerWebTasksProperties _Options, string _JobId, ref string _JobFileBody, out string _Results, out string _ErrorMessage)
        {
            _Results = null;
            _ErrorMessage = null;
            try
            {
                EmployerWebTaskExportSelectedResults_Request taskExportResultsInfo = JsonConvert.DeserializeObject<EmployerWebTaskExportSelectedResults_Request>(_JobFileBody);

                if (taskExportResultsInfo.JobExpiredAt < DateTime.Now)
                    throw new Exception($"Выполнение задачи '{ _JobId }' просрочено");

                var medorg = ProgramStart.ClinicApp.CurrentDB.Organizations.GetMedOrganizationByOGRN(taskExportResultsInfo.MedOrgOGRN, taskExportResultsInfo.MedOrgINN, taskExportResultsInfo.MedOrgKPP);
                if (medorg == null)
                {
                    _ErrorMessage = $"Медицинская организация не найдена по ОГРН '{ taskExportResultsInfo.MedOrgOGRN }' ИНН '{ taskExportResultsInfo.MedOrgINN }' КПП '{ taskExportResultsInfo.MedOrgKPP }'";
                    return false;
                }

                var org = ProgramStart.ClinicApp.CurrentDB.Organizations.GetOrganizationByOGRN(taskExportResultsInfo.OrgOGRN, taskExportResultsInfo.OrgINN, taskExportResultsInfo.OrgKPP);
                if (org == null)
                {
                    _ErrorMessage = $"Организация работодателя не найдена по ОГРН '{ taskExportResultsInfo.OrgOGRN }' ИНН '{ taskExportResultsInfo.OrgINN }' КПП '{ taskExportResultsInfo.OrgKPP }'";
                    return false;
                }

                SiMed.Clinic.DataModel.Request request = ProgramStart.ClinicApp.CurrentDB.Requests.GetRequest(SiMed.Clinic.DataModel.RequestType.NominalRequest, taskExportResultsInfo.MedInspectionRequestId);
                if (request == null)
                {
                    _ErrorMessage = $"Заявка на медицинский осмотр не найдена по Id '{ taskExportResultsInfo.MedInspectionRequestId }'";
                    return false;
                }

                if (request.RequestInfo.MEDORG_ID != medorg.MEDORG_ID)
                {
                    _ErrorMessage = "В заявке на медицинский осмотр указана медицинская организация, отличная от переданной в запросе";
                    return false;
                }

                if (request.RequestInfo.ORG_ID != org.ORG_ID)
                {
                    _ErrorMessage = "В заявке на медицинский осмотр указана организация работодателя, отличная от переданной в запросе";
                    return false;
                }

                string errorMessage;
                //Проверяем, готов ли архив с результатами медицинских осмотров по данной задаче
                if (!CheckAllResultsArchived(_Options, taskExportResultsInfo.JobId))
                {
                    //Если архив не готов, то нужно его делать
                    if (!GenerateArchiveResults(_Options, taskExportResultsInfo, request, out errorMessage))
                        throw new Exception($"Ошибка при формировании архива с результатами медицинской комиссии: { errorMessage }");
                }

                //Очищаем временные файлы, которые могли остаться после формирования архива
                ClearFilesForArchive(_Options, taskExportResultsInfo.JobId);

                //Отправляем готовый архив с результатами в кабинет работодателя (с учетом ограничения скорости передачи)
                if (!SendArchiveToServerWithSpeedLimit(_Options, taskExportResultsInfo, out errorMessage))
                    throw new Exception(errorMessage);

                //Удаляем архив результатов с клиентской машины
                string fileNameArchive = GetArchiveResultsFileName(_Options, taskExportResultsInfo.JobId);
                if (File.Exists(fileNameArchive))
                    File.Delete(fileNameArchive);

                //Формируем результаты выполнения задачи
                EmployerWebTaskExportSelectedResults_Result resilts = new EmployerWebTaskExportSelectedResults_Result();
                resilts.Success = true;
                _Results = JsonConvert.SerializeObject(resilts);
                return true;
            }
            catch (Exception exc)
            {
                if (exc.Message == $"Выполнение задачи '{ _JobId }' просрочено")
                {
                    //Очищаем временные файлы, которые могли остаться после формирования архива
                    ClearFilesForArchive(_Options, _JobId);

                    //Удаляем архив результатов с клиентской машины
                    string fileNameArchive = GetArchiveResultsFileName(_Options, _JobId);
                    if (File.Exists(fileNameArchive))
                        File.Delete(fileNameArchive);

                    List<string> Errors = new List<string>();
                    Errors.Add(exc.Message);

                    EmployerWebTaskExportSelectedResults_Result resilts = new EmployerWebTaskExportSelectedResults_Result();
                    resilts.Success = false;
                    resilts.ErrorMessages = Errors;
                    _Results = JsonConvert.SerializeObject(resilts);

                    return true;
                }

                _ErrorMessage = exc.Message + "\r\n" + exc.StackTrace;
                return false;
            }
        }

        private static bool GenerateArchiveResults(EmployerWebTasksProperties _Options, EmployerWebTaskExportSelectedResults_Request _JobInfo, SiMed.Clinic.DataModel.Request _Request, out string _ErrorMessage)
        {
            _ErrorMessage = null;
            try
            {
                //Получение рузультатов по заявке на медосмотр с применением фильтра по подразделению
                SiMed.Clinic.DataModel.MedInspectionResults results = null;
                if (_JobInfo.FilterPostSubDiv.HasValue)
                    results = _Request.GetMedInspectionResults(_JobInfo.FilterPostSubDiv.Value);
                else
                    results = _Request.GetMedInspectionResults(0);
                DataView resultsDataView = results.DefaultDataView;
                foreach (DataRowView resultInfoView in resultsDataView)
                {
                    dtsRequests.MEDINSPECTION_RESULTSRow resultInfo = (dtsRequests.MEDINSPECTION_RESULTSRow)resultInfoView.Row;
                    int doc_ID = 0;
                    if (!resultInfo.IsDOC_IDNull())
                        doc_ID = resultInfo.DOC_ID;

                    //Проверка, что по данному результату уже файл сохранен во временную директорию
                    if (CheckPersonResultSaved(_Options, _JobInfo.JobId, resultInfo.PER_FIO, doc_ID))//resultInfo.MI_REQ_CONT_ID
                    {
                        //Удаляем временные файлы, если они есть, которые были необходимы для формирования данного файла
                        if (_Options.ZipMIResultWithExtraction)
                        {
                            string fileName = GetPersonResultFileName(_Options, _JobInfo.JobId, resultInfo.PER_FIO, doc_ID, "example");
                            string directoryName = Path.GetDirectoryName(fileName);
                            string[] fileNames = Directory.GetFiles(directoryName);
                            foreach (var name in fileNames)
                                File.Delete(name);
                            Directory.Delete(directoryName);
                        }

                        continue;
                    }


                    //Применение фильтра по текстовому поиску
                    if (!String.IsNullOrEmpty(_JobInfo.FilterFindText) && !resultInfo.PER_FIO.Contains(_JobInfo.FilterFindText) && !resultInfo.POST_NAME.Contains(_JobInfo.FilterFindText) && !resultInfo.POST_DEPARTMENT.Contains(_JobInfo.FilterFindText))
                        continue;

                    //Применение фильтра по результату медосмотра
                    if (_JobInfo.FilterRes.HasValue && (resultInfo.MI_RES_ID == 0 ? 5 : resultInfo.MI_RES_ID) != _JobInfo.FilterRes.Value)
                        continue;

                    SiMed.Clinic.DataModel.Document doc = ProgramStart.ClinicApp.CurrentDB.Documents.GetDocument(resultInfo.DOC_ID);
                    if (doc.EDocuments == null)
                        continue;

                    //Применение фильтра по периоду "От"
                    if (_JobInfo.FilterPeriodFrom.HasValue && _JobInfo.FilterPeriodFrom.Value > doc.DocumentInfo.DOC_CONCLUSION_DATE)
                        continue;

                    //Применение фильтра по периоду "До"
                    if (_JobInfo.FilterPeriodTill.HasValue && _JobInfo.FilterPeriodTill.Value < doc.DocumentInfo.DOC_CONCLUSION_DATE)
                        continue;

                    //Применение фильтра по номеру документа
                    if (_JobInfo.FilterDocNum.HasValue && _JobInfo.FilterDocNum.Value != doc.DocumentInfo.DOC_NUMBER)
                        continue;

                    SortedSet<string> docTypeIsGot = new SortedSet<string>();
                    var docItems = doc.EDocuments.Items.OrderBy(x => x.EDocumentRow.EDOC_TYPE).ThenByDescending(x => x.EDocumentRow.EDOC_INPUT_DATETIME).ToList();
                    Dictionary<string, int> possibleEDocuments = new Dictionary<string, int>();

                    foreach (var docItem in docItems)
                    {
                        if (docItem.EDocumentRow.IsEDOC_VALIDNull() || !docItem.EDocumentRow.EDOC_VALID || !docItem.IsFullySigned())
                            continue;

                        string docTypeName = docItem.EDocumentRow.EDOC_TYPE;

                        if (docTypeIsGot.Contains(docTypeName))
                            continue;
                        docTypeIsGot.Add(docTypeName);

                        bool documentNeedExport = false;

                        if (docTypeName == "Заключение")
                            documentNeedExport = true;
                        if (_Options.ZipMIResultWithExtraction && docTypeName == "Выписка")
                            documentNeedExport = true;

                        if (!documentNeedExport)
                            continue;
                        possibleEDocuments[docTypeName] = docItem.EDocumentRow.EDOC_ID;

                        //if (!docDetail.IsFullySigned())
                        //    continue;

                        //Проверка, что такой файл уже сохранен во временную директорию
                        if (CheckPersonResultSaved(_Options, _JobInfo.JobId, resultInfo.PER_FIO, doc_ID, docTypeName))
                            continue;
                    }
                    if (!possibleEDocuments.ContainsKey("Заключение"))
                        continue;
                    foreach (var val in possibleEDocuments)
                    {
                        SiMed.Clinic.DataModel.EDocument docItem = docItems.FirstOrDefault(x => x.EDocumentRow.EDOC_ID == val.Value);

                        PdfDocumentDetails pdfDocumentDetail = doc.EDocuments.GetPdfDocumentDetail(docItem.EDocumentRow.EDOC_INSTANCE_ID);

                        //Если тело файла пустое, то считаем, что корректно архив мы сформировать не можем
                        if (pdfDocumentDetail.DocumentBody == null)
                            throw new Exception($"Не удалось получить тело документа из базы с EDOC_ID = '{ docItem.EDocumentRow.EDOC_ID }' и EDOC_INSTANCE_ID = '{ docItem.EDocumentRow.EDOC_INSTANCE_ID }'");

                        //Сохраняем файл во временную директорию
                        string docTypeName = docItem.EDocumentRow.EDOC_TYPE;
                        SavePersonResult(_Options, _JobInfo.JobId, resultInfo.PER_FIO, doc_ID, docTypeName, pdfDocumentDetail);
                    }

                    //После сохранения всех файлов по итогу медицинского осмотра, если нужно - сформировать архив
                    if (_Options.ZipMIResultWithExtraction)
                    {
                        string fileName = GetPersonResultFileName(_Options, _JobInfo.JobId, resultInfo.PER_FIO, doc_ID, "exampleDocumentType");
                        string directoryName = Path.GetDirectoryName(fileName);

                        if (!Directory.Exists(directoryName))
                            continue;

                        fileName = GetPersonResultFileName(_Options, _JobInfo.JobId, resultInfo.PER_FIO, doc_ID);
                        ZipArchiveHelper.GenerateZipArchive(directoryName, fileName, null);

                        //Удаляем временные файлы, если они есть, которые были необходимы для формирования данного файла
                        if (_Options.ZipMIResultWithExtraction)
                        {
                            string[] fileNames = Directory.GetFiles(directoryName);
                            foreach (var name in fileNames)
                                File.Delete(name);
                            Directory.Delete(directoryName);
                        }
                    }
                }

                //После формирования всех файлов по итогу всех медицинских осмотров необходимо сформировать архив
                string fileNameForArchive = GetPersonResultFileName(_Options, _JobInfo.JobId, "Example_Per_FIO", -1);
                string directoryNameForArchive = Path.GetDirectoryName(fileNameForArchive);

                if (!Directory.Exists(directoryNameForArchive))
                    Directory.CreateDirectory(directoryNameForArchive);

                string fileNameArchive = GetArchiveResultsFileName(_Options, _JobInfo.JobId);
                ZipArchiveHelper.GenerateZipArchive(directoryNameForArchive, fileNameArchive, _JobInfo.ArchivePassword);

                return true;
            }
            catch (Exception exc)
            {
                _ErrorMessage = exc.Message + "\r\n" + exc.StackTrace;
                return false;
            }
        }

        private static string GetArchiveResultsFileName(EmployerWebTasksProperties _Options, string _JobId)
        {
            string fileName = Path.Combine(_Options.TaskTmpFolder, $"{ _JobId }.zip");
            return fileName;
        }

        private static string GetPersonResultFileName(EmployerWebTasksProperties _Options, string _JobId, string _PerFIO, int _ResultId)
        {
            string fileName;
            if (_Options.ZipMIResultWithExtraction)
                fileName = Path.Combine(_Options.TaskTmpFolder, _JobId, $"{ _PerFIO }_{ _ResultId }.zip");
            else
                fileName = Path.Combine(_Options.TaskTmpFolder, _JobId, $"{ _PerFIO }_{ _ResultId }.pdf");
            return fileName;
        }

        private static string GetPersonResultFileName(EmployerWebTasksProperties _Options, string _JobId, string _PerFIO, int _ResultId, string _DocumentType)
        {
            string fileName;
            if (_Options.ZipMIResultWithExtraction)
                fileName = Path.Combine(_Options.TaskTmpFolder, _JobId, $"{ _PerFIO }_{ _ResultId }", $"{ _PerFIO }_{ _DocumentType }.pdf");
            else
                fileName = Path.Combine(_Options.TaskTmpFolder, _JobId, $"{ _PerFIO }_{ _ResultId }.pdf");
            return fileName;
        }

        private static bool CheckAllResultsArchived(EmployerWebTasksProperties _Options, string _JobId)
        {
            string archiveFileName = GetArchiveResultsFileName(_Options, _JobId);
            if (File.Exists(archiveFileName))
                return true;
            else
                return false;
        }

        private static bool CheckPersonResultSaved(EmployerWebTasksProperties _Options, string _JobId, string _PerFIO, int _ResultId)
        {
            string fileName = GetPersonResultFileName(_Options, _JobId, _PerFIO, _ResultId);
            if (File.Exists(fileName))
                return true;
            else
                return false;
        }

        private static bool CheckPersonResultSaved(EmployerWebTasksProperties _Options, string _JobId, string _PerFIO, int _ResultId, string _DocumentType)
        {
            string fileName = GetPersonResultFileName(_Options, _JobId, _PerFIO, _ResultId, _DocumentType);
            if (File.Exists(fileName))
                return true;
            else
                return false;
        }

        private static void SavePersonResult(EmployerWebTasksProperties _Options, string _JobId, string _PerFIO, int _ResultId, string _DocumentType, PdfDocumentDetails _DocumentInfo)
        {
            string fileName = GetPersonResultFileName(_Options, _JobId, _PerFIO, _ResultId, _DocumentType);
            string directoryName1 = Path.GetDirectoryName(fileName);
            string directoryName2 = Directory.GetParent(directoryName1).FullName;
            string directoryName3 = Directory.GetParent(directoryName2).FullName;

            if (!Directory.Exists(directoryName3))
                Directory.CreateDirectory(directoryName3);

            if (!Directory.Exists(directoryName2))
                Directory.CreateDirectory(directoryName2);

            if (!Directory.Exists(directoryName1))
                Directory.CreateDirectory(directoryName1);

            File.WriteAllBytes(fileName, _DocumentInfo.DocumentBody);
        }

        private static void ClearFilesForArchive(EmployerWebTasksProperties _Options, string _JobId)
        {
            string fileNameForArchive = GetPersonResultFileName(_Options, _JobId, "Example_Per_FIO", -1);
            string directoryNameForArchive = Path.GetDirectoryName(fileNameForArchive);

            if (Directory.Exists(directoryNameForArchive))
            {
                string[] directories = Directory.GetDirectories(directoryNameForArchive);
                string[] files;
                foreach (var directory in directories)
                {
                    files = Directory.GetFiles(directory);
                    foreach (var fileName in files)
                        File.Delete(fileName);

                    Directory.Delete(directory);
                }

                files = Directory.GetFiles(directoryNameForArchive);
                foreach (var fileName in files)
                    File.Delete(fileName);
                Directory.Delete(directoryNameForArchive);
            }
        }

        private static bool SendArchiveToServerWithSpeedLimit(EmployerWebTasksProperties _Options, EmployerWebTaskExportSelectedResults_Request _JobInfo, out string _ErrorMessage)
        {
            _ErrorMessage = null;

            try
            {
                string fileNameArchive = GetArchiveResultsFileName(_Options, _JobInfo.JobId);

                string uri = _JobInfo.CallbackURL;
                if (!String.IsNullOrEmpty(uri) && uri[uri.Length - 1] != '/')
                    uri += "/";
                uri += "api/EmployerWebAPI/GetTaskExportSelectedResultsUploadedBytesCount";

                //получение информации об уже загруженной части данных
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "GET";
                request.Headers.Add("JobId", _JobInfo.JobId);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode != HttpStatusCode.OK)
                    return false;

                //смещаемся в файле на количество переданных данных
                int bytesUploadedBefore = Convert.ToInt32(response.Headers["BytesUploadedCount"]);
                FileStream fs = null;
                BinaryReader reader = null;
                try
                {
                    fs = new FileStream(fileNameArchive, FileMode.Open, FileAccess.Read);
                    reader = new BinaryReader(fs);

                    uri = _JobInfo.CallbackURL;
                    if (!String.IsNullOrEmpty(uri) && uri[uri.Length - 1] != '/')
                        uri += "/";
                    uri += "api/EmployerWebAPI/UploadTaskExportSelectedResultsFile";

                    if (bytesUploadedBefore > 0)
                        fs.Seek(bytesUploadedBefore, SeekOrigin.Begin);

                    //передаем данные порциями, пока порции не закончатся
                    var startTime = DateTime.Now;
                    byte[] buffer;
                    int bufferSize = 1000000;
                    int bytesUploaded = 0;
                    int totalBytesUploaded = bytesUploaded + bytesUploadedBefore;
                    while (totalBytesUploaded < fs.Length)
                    {
                        request = (HttpWebRequest)WebRequest.Create(uri);
                        request.Method = "POST";
                        request.Headers.Add("JobId", _JobInfo.JobId);
                        request.Headers.Add("FirstByteIndex", (totalBytesUploaded + 1).ToString());
                        request.ContentType = "application/zip";

                        int needSendBytes = bufferSize;
                        if (totalBytesUploaded + needSendBytes > fs.Length)
                            needSendBytes = (int)fs.Length - totalBytesUploaded;

                        buffer = reader.ReadBytes(needSendBytes);

                        request.Timeout = (int)(buffer.Length / 1024.0 / _Options.SendSpeedKbps * 1000 * 1.5 + 5000);
                        request.ContentLength = buffer.Length;
                        Stream requestStream = request.GetRequestStream();
                        requestStream.Write(buffer, 0, buffer.Length);

                        response = (HttpWebResponse)request.GetResponse();
                        if (response.StatusCode != HttpStatusCode.OK)
                            return false;

                        totalBytesUploaded += buffer.Length;
                        bytesUploaded += buffer.Length;

                        // recalc speed and wait
                        var expectedTime = bytesUploaded / 1024.0 / _Options.SendSpeedKbps;
                        var actualTime = (DateTime.Now - startTime).TotalSeconds;
                        if (expectedTime > actualTime)
                            Thread.Sleep(TimeSpan.FromSeconds(expectedTime - actualTime));
                    }
                }
                finally
                {
                    if (reader != null)
                        reader.Close();
                }

                return true;
            }
            catch (WebException exc)
            {
                _ErrorMessage = exc.Message;

                if (exc.Response != null)
                {
                    Stream responseStream = exc.Response.GetResponseStream();
                    if (responseStream != null)
                    { 
                        StreamReader responseStreamReader = new StreamReader(responseStream);
                        string additionMessage = responseStreamReader.ReadToEnd();
                        if (!String.IsNullOrEmpty(additionMessage))
                            _ErrorMessage += "\r\n" + additionMessage;
                    }
                }

                return false;
            }
            catch (Exception exc)
            {
                _ErrorMessage = exc.Message + "\r\n" + exc.StackTrace;
                return false;
            }
        }
    }
}
