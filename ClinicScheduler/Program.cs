using System;
using System.Data;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration.Install;
using System.Configuration;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Management;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.IO.IsolatedStorage;
using System.Diagnostics;
using SiMed;
using SiMed.Clinic;
using SiMed.Clinic.Singleton;
using Newtonsoft.Json;
using ClinicScheduler.EmployerWebTasks;

namespace ClinicScheduler
{
    class ProgramStart
    {
        const string EmployerWebTaskFilesMutexId = "Global\\{A79EFC1D-844F-483C-B3B5-565C33A16DD4}";

        static public CClinicApp ClinicApp;

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            //args = new string[2];
            //args[0] = "CheckEmployerWebTasksConfig";
            //args[0] = "SendReports";
            //args[1] = "task";

            //args[0] = "CheckResults"; //Получение результатов лабораторных исследований
            //args[0] = "CheckResultsConfig"; //Получение результатов лабораторных исследований

            //args = new string[1];
            //args[0] = "CheckEmployerWebTasks";

            string OperationName = null;
            if (args.Length == 0)
            {
                F_ChoiceSettings formChoiceSettings = new F_ChoiceSettings();
                if (formChoiceSettings.ShowDialog() == DialogResult.OK)
                    OperationName = formChoiceSettings.SelectedConfigName.ToLower();
                else
                    return;
            }
            else
                OperationName = args[0].ToLower();

            if (OperationName == "Config".ToLower())
            {
                ClinicApp = new CClinicApp();
                ClinicApp.Init();
                SiMed.Clinic.GUI.FormUserLogin form = new SiMed.Clinic.GUI.FormUserLogin();
                form.DialogResult = DialogResult.None;
                while (form.DialogResult != DialogResult.OK && form.DialogResult != DialogResult.Cancel && form.DialogResult != DialogResult.Abort)
                    form.ShowDialog();
                if (form.DialogResult != DialogResult.OK)
                    return;

                AppConfig config;
                try
                {
                    config = AppConfig.GetPropertiesFromConfigFile();
                }
                catch (Exception Exc)
                {
                    config = new AppConfig();
                }
                F_PropertiesSend f_properties = new F_PropertiesSend(config);
                if (f_properties.ShowDialog() == DialogResult.OK)
                {
                    config.SaveConfigFile();
                    MessageBox.Show("Сохранение настроек успешное произведено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (OperationName == "CreateFileProperties".ToLower())
            {
                AppConfig.GenerateConfigFile();
            }
            else if (OperationName == "SendReports".ToLower())
            {
                string ReportTaskName = "";
                try
                {
                    ReportTaskName = args[1].ToLower();
                }
                catch (Exception)
                {
                    WriteToLog("При запуске программы на отправку отчетов необходимо указывать название задачи", true);
                    return;
                }

                WriteToLog("Программа запущена для выполнения задачи \"" + ReportTaskName + "\"", false);
                ClinicApp = new CClinicApp();
                ClinicApp.Init();
                try
                {
                    int TaskIndex = -1;
                    AppConfig config = AppConfig.GetPropertiesFromConfigFile();
                    WriteToLog("Файл конфигурации задач успешно загружен", false);
                    for (int i = 0; i < config.SendReportTasksProperties.Count; i++)
                        if (config.SendReportTasksProperties[i].TaskName.ToLower() == ReportTaskName)
                        {
                            TaskIndex = i;
                            break;
                        }
                    if (TaskIndex == -1)
                    {
                        //WriteToLog("Задача \"" + ReportTaskName + "\" не была найдена в файле конфигурации", false);
                        WriteToLog("Задача \"" + ReportTaskName + "\" не была найдена в файле конфигурации", true);
                        return;
                    }
                    WriteToLog("Старт выполнения задачи \"" + ReportTaskName + "\"", false);
                    SendReports(TaskIndex, ref config);
                    WriteToLog("Задача \"" + ReportTaskName + "\" успешно выполнена", false);
                }
                catch(Exception exc)
                {
                    //WriteToLog("Задача \"" + ReportTaskName + "\" не была выполнена из-за ошибки", false);
                    WriteToLog("Задача \"" + ReportTaskName + "\" не была выполнена из-за ошибки: " + exc.Message, true);
                }
            }
            //--------------------
            else if (OperationName == "CheckResults".ToLower())
            {

                WriteToLog("Программа запущена для получения результатов лабораторных исследований", false);

                ClinicApp = new CClinicApp();
                ClinicApp.InitSiMedDB();

                try
                {
//                    MessageBox.Show("1");
                    AppConfig config = AppConfig.GetPropertiesFromConfigFile();

                    
                    if (config != null && config.LaboratoryTestsResultsProperties != null)
                    {
                        if (config.LaboratoryTestsResultsProperties.ClinicId <= 0
                            || config.LaboratoryTestsResultsProperties.NumberOfDays <= 0
                            || config.LaboratoryTestsResultsProperties.BranchId < 0
                            || config.LaboratoryTestsResultsProperties.UserId <= 0)
                        {
                            //WriteToLog("Задача получения результатов лабораторных исследований не была выполнена. Не заданы корректные настройки", false);
                            WriteToLog("Задача получения результатов лабораторных исследований не была выполнена. Не заданы корректные настройки", true);
                        }
                        else
                        {
                            SiMed.Clinic.DataModel.Security lSecurity = SiMed.Clinic.Singleton.ClinicDatabase.SiMedDatabase.GetDataModelElement(SiMed.Clinic.DataModel.DataModelElementType.Security) as SiMed.Clinic.DataModel.Security;
                            dtsSecurity.SEC_USERRow SelectedUserRow = lSecurity.GetUserRowByID(config.LaboratoryTestsResultsProperties.UserId);
                            if (SelectedUserRow == null)
                                throw new Exception($"Не найден пользователь <{config.LaboratoryTestsResultsProperties.UserId}>, от имени которого получаются результаты");

                            SiMed.Clinic.Singleton.ClinicSecurity.UserSecurityToken = lSecurity.CreateSecurityToken(SelectedUserRow);

                            var date = DateTime.Now.AddDays(-config.LaboratoryTestsResultsProperties.NumberOfDays);
                            DateTime beginDate = new DateTime(date.Year, date.Month, date.Day);

                            date = DateTime.Now;
                            DateTime endDate = new DateTime(date.Year, date.Month, date.Day).AddDays(1).AddMinutes(-1);

                            SiMed.Clinic.DataModel.ResultsInfoCollection results = new SiMed.Clinic.DataModel.ResultsInfoCollection();

                            SiMed.Clinic.DataModel.MedicalLaboratoryOrders orders = SiMed.Clinic.Singleton.ClinicDatabase.SiMedDatabase.GetDataModelElement(SiMed.Clinic.DataModel.DataModelElementType.MedicalLaboratoryOrder) as SiMed.Clinic.DataModel.MedicalLaboratoryOrders;

                            bool checkResult = orders.CheckResultsForPeriod(
                                _MedOrg_ID: config.LaboratoryTestsResultsProperties.ClinicId,
                                _Branch_ID: config.LaboratoryTestsResultsProperties.BranchId,
                                _BeginDate: beginDate,
                                _EndDate: endDate,
                                _ResultsInfoCollection: ref results);
                            var ex = orders.LastException;

                            if (results != null && results.Count > 0)
                            {
                                WriteToLog(String.Format("За период с {0} по {1} было получено результатов: {2}. Мед. орг. ID={3}, филиал ID={4}", 
                                    beginDate, 
                                    endDate, 
                                    results.Count, 
                                    config.LaboratoryTestsResultsProperties.ClinicId, 
                                    config.LaboratoryTestsResultsProperties.BranchId), false);
                            }
                            else
                            {
                                WriteToLog(String.Format("Не получено ни одного результата для мед. орг. ID={0}, филиал ID={1} за период с {2} по {3}",
                                    config.LaboratoryTestsResultsProperties.ClinicId,
                                    config.LaboratoryTestsResultsProperties.BranchId,
                                    beginDate, endDate), false);
                            }
                        }
                    }
                    else
                    {
                        //WriteToLog("Задача получения результатов лабораторных исследований не была выполнена. Не заданы корректные настройки", false);
                        WriteToLog("Задача получения результатов лабораторных исследований не была выполнена. Не заданы корректные настройки", true);
                    }

                    //WriteToLog("Файл конфигурации задач успешно загружен", false);
                    //for (int i = 0; i < config.SendReportTasksProperties.Count; i++)
                    //    if (config.SendReportTasksProperties[i].TaskName.ToLower() == ReportTaskName)
                    //    {
                    //        TaskIndex = i;
                    //        break;
                    //    }
                    //if (TaskIndex == -1)
                    //{
                    //    WriteToLog("Задача \"" + ReportTaskName + "\" не была найдена в файле конфигурации", false);
                    //    WriteToLog("Задача \"" + ReportTaskName + "\" не была найдена в файле конфигурации", true);
                    //    return;
                    //}
                    //WriteToLog("Старт выполнения задачи \"" + ReportTaskName + "\"", false);
                    //SendReports(TaskIndex, ref config);
                    //WriteToLog("Задача \"" + ReportTaskName + "\" успешно выполнена", false);
                }
                catch (Exception exc)
                {
                    //WriteToLog("Задача получения результатов лабораторных исследований не была выполнена из-за ошибки", false);
                    WriteToLog("Задача получения результатов лабораторных исследований не была выполнена из-за ошибки: " + exc.Message, true);
                }
            }
            else if (OperationName == "CheckResultsConfig".ToLower())
            {
                ClinicApp = new CClinicApp();
                ClinicApp.InitSiMedDB();




                SiMed.Clinic.GUI.FormUserLogin form = new SiMed.Clinic.GUI.FormUserLogin();
                form.DialogResult = DialogResult.None;
                while (form.DialogResult != DialogResult.OK && form.DialogResult != DialogResult.Cancel && form.DialogResult != DialogResult.Abort)
                    form.ShowDialog();
                if (form.DialogResult != DialogResult.OK)
                    return;

                AppConfig config;
                try
                {
                    config = AppConfig.GetPropertiesFromConfigFile();
                }
                catch (Exception Exc)
                {
                    config = new AppConfig();
                }

                F_ResultTaskManager form_Prop = new F_ResultTaskManager(config, ClinicApp);

                if (form_Prop.ShowDialog() == DialogResult.OK)
                {
                    config.SaveConfigFile();
                    MessageBox.Show("Сохранение настроек успешное произведено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            //--------------------
            else if (OperationName == "CheckIEMKRecordsState".ToLower())
            {

                WriteToLog("Программа запущена для проверки статусов документов, отправленных в ЕГИСЗ", false);

                ClinicApp = new CClinicApp();
                ClinicApp.InitSiMedDB();

                try
                {
                    //                    MessageBox.Show("1");
                    AppConfig config = AppConfig.GetPropertiesFromConfigFile();


                    if (config != null && config.CheckIEMKRecordsStateProperties != null)
                    {
                        if (config.CheckIEMKRecordsStateProperties.ClinicId <= 0
                            || config.CheckIEMKRecordsStateProperties.NumberOfDays <= 0
                            || config.CheckIEMKRecordsStateProperties.BranchId < 0
                            || config.CheckIEMKRecordsStateProperties.UserId <= 0)
                        {
                            WriteToLog("Задача проверки статусов документов, отправленных в ЕГИСЗ не была выполнена. Не заданы корректные настройки", true);
                        }
                        else
                        {
                            SiMed.Clinic.DataModel.Security lSecurity = SiMed.Clinic.Singleton.ClinicDatabase.SiMedDatabase.GetDataModelElement(SiMed.Clinic.DataModel.DataModelElementType.Security) as SiMed.Clinic.DataModel.Security;
                            dtsSecurity.SEC_USERRow SelectedUserRow = lSecurity.GetUserRowByID(config.CheckIEMKRecordsStateProperties.UserId);
                            if (SelectedUserRow == null)
                                throw new Exception($"Не найден пользователь <{config.CheckIEMKRecordsStateProperties.UserId}>, от имени которого выполняется проверка статусов документов, отправленных в ИЭМК");

                            SiMed.Clinic.Singleton.ClinicSecurity.UserSecurityToken = lSecurity.CreateSecurityToken(SelectedUserRow);

                            var date = DateTime.Now.AddDays(-config.CheckIEMKRecordsStateProperties.NumberOfDays);
                            DateTime beginDate = new DateTime(date.Year, date.Month, date.Day);

                            date = DateTime.Now;
                            DateTime endDate = new DateTime(date.Year, date.Month, date.Day).AddDays(1).AddMinutes(-1);

                            SiMed.Clinic.DataModel.Visits visits = SiMed.Clinic.Singleton.ClinicDatabase.SiMedDatabase.GetDataModelElement(SiMed.Clinic.DataModel.DataModelElementType.Visit) as SiMed.Clinic.DataModel.Visits;

                            bool checkResult = visits.CheckVisitEDocumentsEvents(
                                _MedOrg_ID: config.CheckIEMKRecordsStateProperties.ClinicId,
                                _Branch_ID: config.CheckIEMKRecordsStateProperties.BranchId,
                                _BeginDate: beginDate,
                                _EndDate: endDate);
                            var ex = visits.LastException;
                            if(checkResult)
                            {
                                WriteToLog(String.Format("Проверка статусов документов за период с {0} по {1} выполнена. Мед. орг. ID={2}, филиал ID={3}",
                                    beginDate,
                                    endDate,
                                    config.CheckIEMKRecordsStateProperties.ClinicId,
                                    config.CheckIEMKRecordsStateProperties.BranchId), false);
                            }
                            else
                            {
                                string ErrMes = "";
                                if (ex != null)
                                    ErrMes = $"{ex.Message}{Environment.NewLine}{ex.StackTrace}";
                                WriteToLog(String.Format("При проверке статусов документов за период с {0} по {1} (Мед. орг. ID={2}, филиал ID={3}) произошла ошибка: {4}",
                                    beginDate,
                                    endDate,
                                    config.CheckIEMKRecordsStateProperties.ClinicId,
                                    config.CheckIEMKRecordsStateProperties.BranchId,
                                    ErrMes
                                    ), true);
                            }
                        }
                    }
                    else
                    {
                        WriteToLog("Задача проверки статусов документов, отправленных в ЕГИСЗ не была выполнена. Не заданы корректные настройки", true);
                    }

                    //WriteToLog("Файл конфигурации задач успешно загружен", false);
                    //for (int i = 0; i < config.SendReportTasksProperties.Count; i++)
                    //    if (config.SendReportTasksProperties[i].TaskName.ToLower() == ReportTaskName)
                    //    {
                    //        TaskIndex = i;
                    //        break;
                    //    }
                    //if (TaskIndex == -1)
                    //{
                    //    WriteToLog("Задача \"" + ReportTaskName + "\" не была найдена в файле конфигурации", false);
                    //    WriteToLog("Задача \"" + ReportTaskName + "\" не была найдена в файле конфигурации", true);
                    //    return;
                    //}
                    //WriteToLog("Старт выполнения задачи \"" + ReportTaskName + "\"", false);
                    //SendReports(TaskIndex, ref config);
                    //WriteToLog("Задача \"" + ReportTaskName + "\" успешно выполнена", false);
                }
                catch (Exception exc)
                {
                    WriteToLog("Задача проверки статусов документов, отправленных в ЕГИСЗ не была выполнена из-за ошибки: " + exc.Message, true);
                }
            }
            else if (OperationName == "CheckIEMKRecordsStateConfig".ToLower())
            {
                ClinicApp = new CClinicApp();
                ClinicApp.InitSiMedDB();




                SiMed.Clinic.GUI.FormUserLogin form = new SiMed.Clinic.GUI.FormUserLogin();
                form.DialogResult = DialogResult.None;
                while (form.DialogResult != DialogResult.OK && form.DialogResult != DialogResult.Cancel && form.DialogResult != DialogResult.Abort)
                    form.ShowDialog();
                if (form.DialogResult != DialogResult.OK)
                    return;

                AppConfig config;
                try
                {
                    config = AppConfig.GetPropertiesFromConfigFile();
                }
                catch (Exception Exc)
                {
                    config = new AppConfig();
                }

                F_CheckIEMKTaskConfig form_Prop = new F_CheckIEMKTaskConfig(config, ClinicApp);

                if (form_Prop.ShowDialog() == DialogResult.OK)
                {
                    config.SaveConfigFile();
                    MessageBox.Show("Сохранение настроек успешное произведено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (OperationName == "ProcessMobileReceived".ToLower())
            {

                WriteToLog("Программа запущена для обработки объектов, полученных от SiMedMobile", false);

                ClinicApp = new CClinicApp();
                ClinicApp.InitSiMedDB();

                try
                {
                    //                    MessageBox.Show("1");
                    AppConfig config = AppConfig.GetPropertiesFromConfigFile();

                    if(config == null || 
                            config.SiMedMobileReceivedProcessedProperties == null || 
                            (config.SiMedMobileReceivedProcessedProperties.MedOrgId <= 0 &&
                            config.SiMedMobileReceivedProcessedProperties.MedOrgId != -1) ||
                            config.SiMedMobileReceivedProcessedProperties.UserId <= 0)
                    {
                        //WriteToLog("Задача обработки объектов, полученных от SiMedMobile не была выполнена. Не заданы корректные настройки", false);
                        WriteToLog("Задача обработки объектов, полученных от SiMedMobile не была выполнена. Не заданы корректные настройки", true);
                        return;
                    }
                    SiMed.Clinic.DataModel.Security lSecurity = SiMed.Clinic.Singleton.ClinicDatabase.SiMedDatabase.GetDataModelElement(SiMed.Clinic.DataModel.DataModelElementType.Security) as SiMed.Clinic.DataModel.Security;
                    dtsSecurity.SEC_USERRow SelectedUserRow = lSecurity.GetUserRowByID(config.SiMedMobileReceivedProcessedProperties.UserId);
                    if (SelectedUserRow == null)
                        throw new Exception($"Не найден пользователь <{config.SiMedMobileReceivedProcessedProperties.UserId}>, от имени которого обрабатываются объекты, полученные от SiMedMobile ");

                    SiMed.Clinic.Singleton.ClinicSecurity.UserSecurityToken = lSecurity.CreateSecurityToken(SelectedUserRow);
                    Dictionary<SiMed.Clinic.DataModel.MobileReceivedObjectType, int> processedObjects = new Dictionary<SiMed.Clinic.DataModel.MobileReceivedObjectType, int>();
                    bool bResult = ClinicApp.CurrentDB.ProcessMobileReceivedObjects(config.SiMedMobileReceivedProcessedProperties.MedOrgId, out processedObjects);
                    if (bResult)
                    {
                        string strResult = "Обработано объектов, полученных от SiMedMobile: ";
                        if (processedObjects.Count > 0)
                        {
                            WriteToLog("Обработано объектов, полученных от SiMedMobile:", false);
                            foreach (var val in processedObjects)
                            {
                                strResult += Environment.NewLine + $" - {val.Key.ToString()} = {val.Value}";
                            } 
                        }
                        else
                        {
                            strResult = "Обработано объектов, полученных от SiMedMobile: 0";
                        }
                        WriteToLog(strResult, false);
                    }
                    else
                    {
                        string ErrMes = $"При обработке объектов, полученных от SiMedMobile произошла ошибка: {ClinicApp.CurrentDB.LastException.ToString()}";
                        //WriteToLog(ErrMes, false);
                        WriteToLog(ErrMes, true);
                    }
                    WriteToLog("Проверка состояния чеков облачных касс", false);
                    int CheckForCheckingCount = 0, ReceivedCheckCount = 0;
                    bResult = ClinicApp.CurrentDB.CheckOnlinePaymentState(config.SiMedMobileReceivedProcessedProperties.MedOrgId, out CheckForCheckingCount, out ReceivedCheckCount);
                    if (bResult)
                    {
                        string strResult = $"Получена информация о {ReceivedCheckCount} чеках из {CheckForCheckingCount}";
                        WriteToLog(strResult, false);
                    }
                    else
                    {
                        string ErrMes = $"При проверке состояния чеков облачных касс произошла ошибка: {ClinicApp.CurrentDB.LastException.ToString()}";
                        //WriteToLog(ErrMes, false);
                        WriteToLog(ErrMes, true);
                    }
                }
                catch (Exception exc)
                {
                    string ErrMes = $"Задача обработки объектов, полученных от SiMedMobile, не была выполнена из-за ошибки: {exc.Message.ToString()}";
                    //WriteToLog(ErrMes, false);
                    WriteToLog(ErrMes, true);
                }
            }
            else if (OperationName == "ProcessMobileReceivedConfig".ToLower())
            {
                ClinicApp = new CClinicApp();
                ClinicApp.InitSiMedDB();


                SiMed.Clinic.GUI.FormUserLogin form = new SiMed.Clinic.GUI.FormUserLogin();
                form.DialogResult = DialogResult.None;
                while (form.DialogResult != DialogResult.OK && form.DialogResult != DialogResult.Cancel && form.DialogResult != DialogResult.Abort)
                    form.ShowDialog();
                if (form.DialogResult != DialogResult.OK)
                    return;

                AppConfig config;
                try
                {
                    config = AppConfig.GetPropertiesFromConfigFile();
                }
                catch (Exception Exc)
                {
                    config = new AppConfig();
                }
                if (config.SiMedMobileReceivedProcessedProperties == null)
                    config.SiMedMobileReceivedProcessedProperties = new SiMedMobileReceivedProcessedProperties();
                F_ProcessedSiMedMobileReceivedTaskManager form_Prop = new F_ProcessedSiMedMobileReceivedTaskManager(ClinicApp, config, config.SiMedMobileReceivedProcessedProperties, "ProcessMobileReceived", "ProcessMobileReceived");

                if (form_Prop.ShowDialog() == DialogResult.OK)
                {
                    config.SaveConfigFile();
                    MessageBox.Show("Сохранение настроек успешное произведено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (OperationName == "AutoCreateAccountingActs".ToLower())
            {
                WriteToLog("Программа запущена для автоматического создания актов оказания услуг", false);

                ClinicApp = new CClinicApp();
                ClinicApp.InitSiMedDB();

                try
                {
                    AppConfig config = AppConfig.GetPropertiesFromConfigFile();

                    if (config?.SiMedAutoCreateAccountingActsProperties == null ||
                        config.SiMedAutoCreateAccountingActsProperties.MedOrgId <= 0 ||
                        config.SiMedAutoCreateAccountingActsProperties.UserId <= 0)
                    {
                        WriteToLog("Задача автоматического создания актов оказания услуг не была выполнена. Не заданы корректные настройки", true);
                        return;
                    }
                    SiMed.Clinic.DataModel.Security lSecurity = SiMed.Clinic.Singleton.ClinicDatabase.SiMedDatabase.GetDataModelElement(SiMed.Clinic.DataModel.DataModelElementType.Security) as SiMed.Clinic.DataModel.Security;
                    dtsSecurity.SEC_USERRow SelectedUserRow = lSecurity.GetUserRowByID(config.SiMedAutoCreateAccountingActsProperties.UserId);
                    if (SelectedUserRow == null)
                        throw new Exception($"Не найден пользователь <{config.SiMedAutoCreateAccountingActsProperties.UserId}>, от имени которого создаются акты оказания услуг");

                    SiMed.Clinic.Singleton.ClinicSecurity.UserSecurityToken = lSecurity.CreateSecurityToken(SelectedUserRow);
                    List<String> Errors = new List<string>();
                    List<SiMed.Clinic.DataModel.AccountingActFiller> NoOrgEmailListAccountingActs = new List<SiMed.Clinic.DataModel.AccountingActFiller>();
                    int _CountSuccess = ClinicApp.CurrentDB.TryCreateAccountingActs(config.SiMedAutoCreateAccountingActsProperties.MedOrgId,
                        config.SiMedAutoCreateAccountingActsProperties.SignEDocuments,
                        config.SiMedAutoCreateAccountingActsProperties.SendEDocuments,
                        config.SiMedAutoCreateAccountingActsProperties.CopyToEmailAddress,
                        out NoOrgEmailListAccountingActs, out Errors);

                    WriteToLog($"Задача завершена. Создано {_CountSuccess} актов оказания услуг", false);

                    if (Errors.Count > 0)
                    {
                        foreach (var error in Errors)
                        {
                            WriteToLog(error, true);
                        }
                    }
                    if (NoOrgEmailListAccountingActs.Count > 0)
                    {
                        foreach (var notPaidAccountingAct in NoOrgEmailListAccountingActs)
                        {
                            WriteToLog($"Для организации {notPaidAccountingAct.OrgFullName} отсутствует почта!", true);
                        }
                    }
                    //else
                    //{
                    //    WriteToLog("Задача успешно выполнена. Для всех актов была указана корректная почта организации!", false);
                    //}
                }
                catch (Exception ex)
                {
                    string ErrMes = $"Задача автоматического создания актов оказания услуг не была выполнена из-за ошибки: {ex.Message}";
                    WriteToLog(ErrMes, true);
                }
            }
            else if (OperationName == "AutoCreateAccountingActsConfig".ToLower())
            {
                ClinicApp = new CClinicApp();
                ClinicApp.InitSiMedDB();

                SiMed.Clinic.GUI.FormUserLogin form = new SiMed.Clinic.GUI.FormUserLogin();
                form.DialogResult = DialogResult.None;
                while (form.DialogResult != DialogResult.OK && form.DialogResult != DialogResult.Cancel && form.DialogResult != DialogResult.Abort)
                    form.ShowDialog();
                if (form.DialogResult != DialogResult.OK)
                    return;

                AppConfig config;
                try
                {
                    config = AppConfig.GetPropertiesFromConfigFile();
                }
                catch (Exception Exc)
                {
                    config = new AppConfig();
                }
                if (config.SiMedAutoCreateAccountingActsProperties == null)
                    config.SiMedAutoCreateAccountingActsProperties = new SiMedAutoCreateAccountingActsProperties();
                F_AutoCreateAccountingActsTaskManager form_Prop = new F_AutoCreateAccountingActsTaskManager(ClinicApp, config, config.SiMedAutoCreateAccountingActsProperties, "AutoCreateAccountingActs", "AutoCreateAccountingActs");

                if (form_Prop.ShowDialog() == DialogResult.OK)
                {
                    config.SaveConfigFile();
                    MessageBox.Show("Сохранение настроек успешное произведено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (OperationName == "AutoCheckPaymentAccountingActs".ToLower())
            {
                WriteToLog("Программа запущена для автоматической проверки оплаты актов оказания услуг", false);

                ClinicApp = new CClinicApp();
                ClinicApp.InitSiMedDB();

                try
                {
                    AppConfig config = AppConfig.GetPropertiesFromConfigFile();

                    if (config?.SiMedAutoCheckAccountingActsProperties == null ||
                        config.SiMedAutoCheckAccountingActsProperties.MedOrgId <= 0 ||
                        config.SiMedAutoCheckAccountingActsProperties.UserId <= 0)
                    {
                        WriteToLog("Задача автоматической проверки оплаты актов оказания услуг не была выполнена. Не заданы корректные настройки", true);
                        return;
                    }
                    SiMed.Clinic.DataModel.Security lSecurity = SiMed.Clinic.Singleton.ClinicDatabase.SiMedDatabase.GetDataModelElement(SiMed.Clinic.DataModel.DataModelElementType.Security) as SiMed.Clinic.DataModel.Security;
                    dtsSecurity.SEC_USERRow SelectedUserRow = lSecurity.GetUserRowByID(config.SiMedAutoCheckAccountingActsProperties.UserId);
                    if (SelectedUserRow == null)
                        throw new Exception($"Не найден пользователь <{config.SiMedAutoCheckAccountingActsProperties.UserId}>, от имени которого проверяется оплата актов оказания услуг");

                    SiMed.Clinic.Singleton.ClinicSecurity.UserSecurityToken = lSecurity.CreateSecurityToken(SelectedUserRow);
                    List<String> Errors = new List<string>();
                    List<SiMed.Clinic.DataModel.AccountingActFiller> NoOrgEmailListAccountingActs = new List<SiMed.Clinic.DataModel.AccountingActFiller>();
                    int sendedSuccessCount = ClinicApp.CurrentDB.TryCheckPaymentAccountingActs(config.SiMedAutoCheckAccountingActsProperties.MedOrgId,
                        config.SiMedAutoCheckAccountingActsProperties.CopyToEmailAddress, 
                        config.SiMedAutoCheckAccountingActsProperties.CreatePretensionEDocuments, config.SiMedAutoCheckAccountingActsProperties.SignPretensionEDocuments,
                        out NoOrgEmailListAccountingActs, out Errors);

                    WriteToLog($"Задача завершена. Отправлено {sendedSuccessCount} уведомлений о наличие просроченных задолженностей по актам оказания услуг", false);

                    if (Errors.Count > 0)
                    {
                        foreach (var error in Errors)
                        {
                            WriteToLog(error, true);
                        }
                    }
                    if (NoOrgEmailListAccountingActs.Count > 0)
                    {
                        foreach (var notPaidAccountingAct in NoOrgEmailListAccountingActs)
                        {
                            WriteToLog($"Для организации {notPaidAccountingAct.OrgFullName} отсутствует почта!", true);
                        }
                    }
                    //else
                    //{
                    //    WriteToLog("Задача успешно выполнена. Для всех актов была указана корректная почта организации!", false);
                    //}
                }
                catch (Exception ex)
                {
                    string ErrMes = $"Задача автоматической проверки оплаты актов оказания услуг не была выполнена из-за ошибки: {ex.Message}";
                    WriteToLog(ErrMes, true);
                }
            }
            else if (OperationName == "AutoCheckPaymentAccountingActsConfig".ToLower())
            {
                ClinicApp = new CClinicApp();
                ClinicApp.InitSiMedDB();

                SiMed.Clinic.GUI.FormUserLogin form = new SiMed.Clinic.GUI.FormUserLogin();
                form.DialogResult = DialogResult.None;
                while (form.DialogResult != DialogResult.OK && form.DialogResult != DialogResult.Cancel && form.DialogResult != DialogResult.Abort)
                    form.ShowDialog();
                if (form.DialogResult != DialogResult.OK)
                    return;

                AppConfig config;
                try
                {
                    config = AppConfig.GetPropertiesFromConfigFile();
                }
                catch (Exception Exc)
                {
                    config = new AppConfig();
                }
                if (config.SiMedAutoCheckAccountingActsProperties == null)
                    config.SiMedAutoCheckAccountingActsProperties = new SiMedAutoCheckAccountingActsProperties();
                F_AutoCheckAccountingActsTaskManager form_Prop = new F_AutoCheckAccountingActsTaskManager(ClinicApp, config, config.SiMedAutoCheckAccountingActsProperties, "AutoCheckPaymentAccountingActs", "AutoCheckPaymentAccountingActs");

                if (form_Prop.ShowDialog() == DialogResult.OK)
                {
                    config.SaveConfigFile();
                    MessageBox.Show("Сохранение настроек успешное произведено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (OperationName == "SendReceptionRecordsReminder".ToLower())
            {

                WriteToLog("Программа запущена для отправки напоминаний о записях на прием", false);

                ClinicApp = new CClinicApp();
                ClinicApp.InitSiMedDB();

                try
                {
                    //                    MessageBox.Show("1");
                    AppConfig config = AppConfig.GetPropertiesFromConfigFile();

                    if (config == null ||
                            config.ReceptionRecordsReminderProperties == null ||
                            (config.ReceptionRecordsReminderProperties.MedOrgId <= 0 &&
                            config.ReceptionRecordsReminderProperties.MedOrgId != -1) ||
                            config.ReceptionRecordsReminderProperties.UserId <= 0)
                    {
                        //WriteToLog("Задача отправки напоминаний о записях на прием не была выполнена. Не заданы корректные настройки", false);
                        WriteToLog("Задача отправки напоминаний о записях на прием не была выполнена. Не заданы корректные настройки", true);
                        return;
                    }
                    SiMed.Clinic.DataModel.Security lSecurity = SiMed.Clinic.Singleton.ClinicDatabase.SiMedDatabase.GetDataModelElement(SiMed.Clinic.DataModel.DataModelElementType.Security) as SiMed.Clinic.DataModel.Security;
                    dtsSecurity.SEC_USERRow SelectedUserRow = lSecurity.GetUserRowByID(config.SiMedMobileReceivedProcessedProperties.UserId);
                    if (SelectedUserRow == null)
                        throw new Exception($"Не найден пользователь <{config.SiMedMobileReceivedProcessedProperties.UserId}>, от имени которого отправляются напоминания о записях на прием ");

                    SiMed.Clinic.Singleton.ClinicSecurity.UserSecurityToken = lSecurity.CreateSecurityToken(SelectedUserRow);
                    int remindersSended = 0;
                    bool bResult = ClinicApp.CurrentDB.SendReceptionRecordReminder(config.ReceptionRecordsReminderProperties.MedOrgId, out remindersSended);
                    if (bResult)
                    {
                        string strResult = $"Отправлено уведомлений: {remindersSended}";
                        WriteToLog(strResult, false);
                    }
                    else
                    {
                        string ErrMes = $"При отправке напоминаний о записях на прием произошла ошибка: {ClinicApp.CurrentDB.LastException.ToString()}";
                        //WriteToLog(ErrMes, false);
                        WriteToLog(ErrMes, true);
                    }
                }
                catch (Exception exc)
                {
                    string ErrMes = $"Задача отправки напоминаний о записях на прием не была выполнена из-за ошибки: {exc.Message.ToString()}";
                    //WriteToLog(ErrMes, false);
                    WriteToLog(ErrMes, true);
                }
            }
            else if (OperationName == "SendReceptionRecordsReminderConfig".ToLower())
            {
                ClinicApp = new CClinicApp();
                ClinicApp.InitSiMedDB();




                SiMed.Clinic.GUI.FormUserLogin form = new SiMed.Clinic.GUI.FormUserLogin();
                form.DialogResult = DialogResult.None;
                while (form.DialogResult != DialogResult.OK && form.DialogResult != DialogResult.Cancel && form.DialogResult != DialogResult.Abort)
                    form.ShowDialog();
                if (form.DialogResult != DialogResult.OK)
                    return;

                AppConfig config;
                try
                {
                    config = AppConfig.GetPropertiesFromConfigFile();
                }
                catch (Exception Exc)
                {
                    config = new AppConfig();
                }
                if (config.ReceptionRecordsReminderProperties == null)
                    config.ReceptionRecordsReminderProperties = new ReceptionRecordsReminderProperties();
                F_ProcessedSiMedMobileReceivedTaskManager form_Prop = new F_ProcessedSiMedMobileReceivedTaskManager(ClinicApp, config, config.ReceptionRecordsReminderProperties, "SendReceptionRecordsReminder", "SendReceptionRecordsReminder");

                if (form_Prop.ShowDialog() == DialogResult.OK)
                {
                    config.SaveConfigFile();
                    MessageBox.Show("Сохранение настроек успешное произведено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (OperationName == "SendDefferedNotifications".ToLower())
            {
                WriteToLog("Программа запущена для отправки отложенных уведомлений", false);
                ClinicApp = new CClinicApp();
                ClinicApp.InitSiMedDB();

                try
                {
                    AppConfig config = AppConfig.GetPropertiesFromConfigFile();

                    if (config == null ||
                            config.SendDefferedNotificationsProperties == null ||
                            (config.SendDefferedNotificationsProperties.MedOrgId <= 0 &&
                            config.SendDefferedNotificationsProperties.MedOrgId != -1) ||
                            config.SendDefferedNotificationsProperties.UserId <= 0)
                    {
                        WriteToLog("Задача отправки уведомлений об отложенных уведомлений не была выполнена. Не заданы корректные настройки", true);
                        return;
                    }
                    SiMed.Clinic.DataModel.Security lSecurity = SiMed.Clinic.Singleton.ClinicDatabase.SiMedDatabase.GetDataModelElement(SiMed.Clinic.DataModel.DataModelElementType.Security) as SiMed.Clinic.DataModel.Security;
                    dtsSecurity.SEC_USERRow SelectedUserRow = lSecurity.GetUserRowByID(config.SendDefferedNotificationsProperties.UserId);
                    if (SelectedUserRow == null)
                        throw new Exception($"Не найден пользователь <{config.SendDefferedNotificationsProperties.UserId}>, от имени которого отправляются отложенные уведомления ");

                    SiMed.Clinic.Singleton.ClinicSecurity.UserSecurityToken = lSecurity.CreateSecurityToken(SelectedUserRow);
                    int sentNotifications = 0;
                    bool bResult = ClinicApp.CurrentDB.SendDefferedNotifications(config.SendDefferedNotificationsProperties.MedOrgId, out sentNotifications);
                    if (bResult)
                    {
                        string strResult = $"Отправлено {sentNotifications} уведомлений!";
                        WriteToLog(strResult, false);
                    }
                    else
                    {
                        string ErrMes = $"При отправке отложенных уведомлений произошла ошибка: {ClinicApp.CurrentDB.LastException}";
                        WriteToLog(ErrMes, true);
                    }
                }
                catch (Exception exc)
                {
                    string ErrMes = $"Задача отправки отложенных уведомлений не была выполнена из-за ошибки: {exc.Message}";
                    WriteToLog(ErrMes, true);
                }
            }
            else if (OperationName == "SendDefferedNotificationsConfig".ToLower())
            {
                ClinicApp = new CClinicApp();
                ClinicApp.InitSiMedDB();

                SiMed.Clinic.GUI.FormUserLogin form = new SiMed.Clinic.GUI.FormUserLogin();
                form.DialogResult = DialogResult.None;
                while (form.DialogResult != DialogResult.OK && form.DialogResult != DialogResult.Cancel && form.DialogResult != DialogResult.Abort)
                    form.ShowDialog();
                if (form.DialogResult != DialogResult.OK)
                    return;

                AppConfig config;
                try
                {
                    config = AppConfig.GetPropertiesFromConfigFile();
                }
                catch (Exception Exc)
                {
                    config = new AppConfig();
                }
                if (config.SendDefferedNotificationsProperties == null)
                    config.SendDefferedNotificationsProperties = new SendDefferedNotificationsProperties();
                F_ProcessedSiMedMobileReceivedTaskManager form_Prop = new F_ProcessedSiMedMobileReceivedTaskManager(ClinicApp, config, config.SendDefferedNotificationsProperties, "SendDefferedNotifications", "SendDefferedNotifications");

                if (form_Prop.ShowDialog() == DialogResult.OK)
                {
                    config.SaveConfigFile();
                    MessageBox.Show("Сохранение настроек успешное произведено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (OperationName == "CheckEmployerWebTasksConfig".ToLower())
            {
                ClinicApp = new CClinicApp();
                ClinicApp.InitSiMedDB();

                SiMed.Clinic.GUI.FormUserLogin form = new SiMed.Clinic.GUI.FormUserLogin();
                form.DialogResult = DialogResult.None;
                while (form.DialogResult != DialogResult.OK && form.DialogResult != DialogResult.Cancel && form.DialogResult != DialogResult.Abort)
                    form.ShowDialog();
                if (form.DialogResult != DialogResult.OK)
                    return;

                AppConfig config;
                try
                {
                    config = AppConfig.GetPropertiesFromConfigFile();
                }
                catch (Exception Exc)
                {
                    config = new AppConfig();
                }
                if (config.EmployerWebTasksProperties == null)
                    config.EmployerWebTasksProperties = new EmployerWebTasksProperties();
                F_EmployerWebTaskConfig form_Prop = new F_EmployerWebTaskConfig(ClinicApp, config);

                if (form_Prop.ShowDialog() == DialogResult.OK)
                {
                    config.SaveConfigFile();
                    MessageBox.Show("Сохранение настроек успешное произведено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (OperationName == "CheckEmployerWebTasks".ToLower())
            {
                string taskTypeName_command = "All";
                if (args.Length > 1 && args[1].ToLower() == "ImportMedInspectionRequest".ToLower())
                    taskTypeName_command = "ImportMedInspectionRequest";
                if (args.Length > 1 && args[1].ToLower() == "ExportMedInspectionResults".ToLower())
                    taskTypeName_command = "ExportMedInspectionResults";

                ClinicApp = new CClinicApp();
                ClinicApp.InitSiMedDB();

                AppConfig config = AppConfig.GetPropertiesFromConfigFile();

                if (config == null ||
                    config.EmployerWebTasksProperties == null ||
                    String.IsNullOrEmpty(config.EmployerWebTasksProperties.TaskNewFolder) ||
                    String.IsNullOrEmpty(config.EmployerWebTasksProperties.TaskProcessFolder) ||
                    String.IsNullOrEmpty(config.EmployerWebTasksProperties.TaskArchiveFolder) ||
                    String.IsNullOrEmpty(config.EmployerWebTasksProperties.TaskResultFolder) ||
                    String.IsNullOrEmpty(config.EmployerWebTasksProperties.TaskTmpFolder) ||
                    config.EmployerWebTasksProperties.SendSpeedKbps == 0 ||
                    !config.EmployerWebTasksProperties.MedInspectionRequestsCreatedFromUserId.HasValue)
                {
                    WriteToLog("Задача обработки задач от кабинета работодателя не была выполнена. Не заданы корректные настройки", true);
                    return;
                }

                SiMed.Clinic.DataModel.Security lSecurity = SiMed.Clinic.Singleton.ClinicDatabase.SiMedDatabase.GetDataModelElement(SiMed.Clinic.DataModel.DataModelElementType.Security) as SiMed.Clinic.DataModel.Security;
                dtsSecurity.SEC_USERRow SelectedUserRow = lSecurity.GetUserRowByID(config.EmployerWebTasksProperties.MedInspectionRequestsCreatedFromUserId.Value);
                if (SelectedUserRow == null)
                    throw new Exception($"Не найден пользователь <{ config.EmployerWebTasksProperties.MedInspectionRequestsCreatedFromUserId.Value }>, от имени которого обрабатываются объекты, полученные от SiMedMobile ");

                SiMed.Clinic.Singleton.ClinicSecurity.UserSecurityToken = lSecurity.CreateSecurityToken(SelectedUserRow);

                int tasksCompleted = 0;

                //если имеются файлы в процессе обработки (т.е. они не обработались по какой-то причине), то начинаем с них
                DirectoryInfo directoryInfo = new DirectoryInfo(config.EmployerWebTasksProperties.TaskProcessFolder);
                FileInfo[] files = directoryInfo.GetFiles().OrderBy(p => p.CreationTime).ToArray();
                foreach (var file in files)
                {
                    EmployerWebTaskCommon taskInfo = null;
                    string filebody;
                    string errorMessage;
                    if (!FileHelper_Mutex.GetFileContent(file.FullName, EmployerWebTaskFilesMutexId, false, out filebody, out errorMessage))
                    {
                        WriteToLog($"Возникла ошибка при обработке задачи '{ taskInfo.JobId }' от Кабинета работодателя: { errorMessage }", true);
                        continue;
                    }
                    if (String.IsNullOrEmpty(filebody))
                        continue;

                    taskInfo = JsonConvert.DeserializeObject<EmployerWebTaskCommon>(filebody);

                    enumEmployerWebJobType taskType;
                    try
                    {
                        taskType = (enumEmployerWebJobType)taskInfo.JobTypeId;
                    }
                    catch (Exception)
                    {
                        WriteToLog($"Возникла ошибка при обработке задачи '{ taskInfo.JobId }' от Кабинета работодателя: неизвестный тип задачи '{ taskInfo.JobId }', код: { taskInfo.JobTypeId }", true);
                        continue;
                    }

                    if (taskTypeName_command != "All" && taskTypeName_command.ToLower() != taskType.ToString().ToLower())
                        continue;

                    if (taskInfo.JobTypeId == (int)enumEmployerWebJobType.ImportMedInspectionRequest)
                    {
                        string result;
                        string error;
                        if (!EmployerWebTaskNewMedInspection_Helper.DoEmployerWebTask(ref filebody, out result, out error))
                        {
                            WriteToLog($"Возникла ошибка при обработке задачи '{ taskInfo.JobId }' от Кабинета работодателя: { error }", true);
                            return;
                        }

                        //перемещение файла задачи в архив
                        string fileTaskArchivePath = Path.Combine(config.EmployerWebTasksProperties.TaskArchiveFolder, file.Name);
                        if (!FileHelper_Mutex.MoveFile(file.FullName, fileTaskArchivePath, EmployerWebTaskFilesMutexId, out errorMessage))
                        {
                            WriteToLog($"Возникла ошибка при обработке задачи '{ taskInfo.JobId }' от Кабинета работодателя при перемещении файла: { errorMessage }", true);
                            continue;
                        }

                        //запись результата
                        string fileTaskResultNewPath = Path.Combine(config.EmployerWebTasksProperties.TaskResultFolder, file.Name);
                        File.WriteAllText(fileTaskResultNewPath, result);

                        tasksCompleted++;
                    }
                    else if (taskInfo.JobTypeId == (int)enumEmployerWebJobType.ExportMedInspectionResults)
                    {
                        string result;
                        string error;
                        if (!EmployerWebTaskExportSelectedResults_Helper.DoEmployerWebTask(config.EmployerWebTasksProperties, taskInfo.JobId, ref filebody, out result, out error))
                        {
                            WriteToLog($"Возникла ошибка при обработке задачи '{ taskInfo.JobId }' от Кабинета работодателя: { error }", true);
                            return;
                        }

                        //перемещение файла задачи в архив
                        string fileTaskArchivePath = Path.Combine(config.EmployerWebTasksProperties.TaskArchiveFolder, file.Name);
                        if (!FileHelper_Mutex.MoveFile(file.FullName, fileTaskArchivePath, EmployerWebTaskFilesMutexId, out errorMessage))
                        {
                            WriteToLog($"Возникла ошибка при обработке задачи '{ taskInfo.JobId }' от Кабинета работодателя при перемещении файла: { errorMessage }", true);
                            continue;
                        }

                        //запись результата
                        string fileTaskResultNewPath = Path.Combine(config.EmployerWebTasksProperties.TaskResultFolder, file.Name);
                        File.WriteAllText(fileTaskResultNewPath, result);

                        tasksCompleted++;
                    }
                    else
                    {
                        WriteToLog($"Возникла ошибка при обработке задачи '{ taskInfo.JobId }' от Кабинета работодателя: неизвестный тип задачи '{ taskInfo.JobId }', код: { taskInfo.JobTypeId }", true);
                        return;
                    }
                }

                //если имеются новые задачи, то их тоже нужно обработать
                directoryInfo = new DirectoryInfo(config.EmployerWebTasksProperties.TaskNewFolder);
                files = directoryInfo.GetFiles().OrderBy(p => p.CreationTime).ToArray();
                foreach (var file in files)
                {
                    EmployerWebTaskCommon taskInfo = null;
                    string filebody;
                    string errorMessage;
                    if (!FileHelper_Mutex.GetFileContent(file.FullName, EmployerWebTaskFilesMutexId, false, out filebody, out errorMessage))
                    {
                        WriteToLog($"Возникла ошибка при обработке задачи '{ taskInfo.JobId }' от Кабинета работодателя при чтении файла: { errorMessage }", true);
                        continue;
                    }
                    if (String.IsNullOrEmpty(filebody))
                        continue;

                    taskInfo = JsonConvert.DeserializeObject<EmployerWebTaskCommon>(filebody);

                    enumEmployerWebJobType taskType;
                    try
                    {
                        taskType = (enumEmployerWebJobType)taskInfo.JobTypeId;
                    }
                    catch (Exception)
                    {
                        WriteToLog($"Возникла ошибка при обработке задачи '{ taskInfo.JobId }' от Кабинета работодателя: неизвестный тип задачи '{ taskInfo.JobId }', код: { taskInfo.JobTypeId }", true);
                        continue;
                    }

                    if (taskTypeName_command != "All" && taskTypeName_command.ToLower() != taskType.ToString().ToLower())
                        continue;

                    if (taskInfo.JobTypeId == (int)enumEmployerWebJobType.ImportMedInspectionRequest)
                    {
                        //перемещение файла задачи в директорию обрабатываемых файлов
                        string fileTaskProcessPath = Path.Combine(config.EmployerWebTasksProperties.TaskProcessFolder, file.Name);
                        if (!FileHelper_Mutex.MoveFile(file.FullName, fileTaskProcessPath, EmployerWebTaskFilesMutexId, out errorMessage))
                        {
                            WriteToLog($"Возникла ошибка при обработке задачи '{ taskInfo.JobId }' от Кабинета работодателя при перемещении файла: { errorMessage }", true);
                            continue;
                        }

                        string result;
                        string error;
                        if (!EmployerWebTaskNewMedInspection_Helper.DoEmployerWebTask(ref filebody, out result, out error))
                        {
                            WriteToLog($"Возникла ошибка при обработке задачи '{ taskInfo.JobId }' от Кабинета работодателя: { error }", true);
                            return;
                        }

                        //перемещение файла задачи в архив
                        string fileTaskArchivePath = Path.Combine(config.EmployerWebTasksProperties.TaskArchiveFolder, file.Name);
                        if (!FileHelper_Mutex.MoveFile(fileTaskProcessPath, fileTaskArchivePath, EmployerWebTaskFilesMutexId, out errorMessage))
                        {
                            WriteToLog($"Возникла ошибка при обработке задачи '{ taskInfo.JobId }' от Кабинета работодателя при перемещении файла: { errorMessage }", true);
                            continue;
                        }

                        //запись результата
                        string fileTaskResultNewPath = Path.Combine(config.EmployerWebTasksProperties.TaskResultFolder, file.Name);
                        File.WriteAllText(fileTaskResultNewPath, result);

                        tasksCompleted++;
                    }
                    else if (taskInfo.JobTypeId == (int)enumEmployerWebJobType.ExportMedInspectionResults)
                    {
                        //перемещение файла задачи в директорию обрабатываемых файлов
                        string fileTaskProcessPath = Path.Combine(config.EmployerWebTasksProperties.TaskProcessFolder, file.Name);
                        if (!FileHelper_Mutex.MoveFile(file.FullName, fileTaskProcessPath, EmployerWebTaskFilesMutexId, out errorMessage))
                        {
                            WriteToLog($"Возникла ошибка при обработке задачи '{ taskInfo.JobId }' от Кабинета работодателя при перемещении файла: { errorMessage }", true);
                            continue;
                        }

                        string result;
                        string error;
                        if (!EmployerWebTaskExportSelectedResults_Helper.DoEmployerWebTask(config.EmployerWebTasksProperties, taskInfo.JobId, ref filebody, out result, out error))
                        {
                            WriteToLog($"Возникла ошибка при обработке задачи '{ taskInfo.JobId }' от Кабинета работодателя: { error }", true);
                            return;
                        }

                        //перемещение файла задачи в архив
                        string fileTaskArchivePath = Path.Combine(config.EmployerWebTasksProperties.TaskArchiveFolder, file.Name);
                        if (!FileHelper_Mutex.MoveFile(fileTaskProcessPath, fileTaskArchivePath, EmployerWebTaskFilesMutexId, out errorMessage))
                        {
                            WriteToLog($"Возникла ошибка при обработке задачи '{ taskInfo.JobId }' от Кабинета работодателя при перемещении файла: { errorMessage }", true);
                            continue;
                        }

                        //запись результата
                        string fileTaskResultNewPath = Path.Combine(config.EmployerWebTasksProperties.TaskResultFolder, file.Name);
                        File.WriteAllText(fileTaskResultNewPath, result);

                        tasksCompleted++;
                    }
                    else
                    {
                        WriteToLog($"Возникла ошибка при обработке задачи '{ taskInfo.JobId }' от Кабинета работодателя: отсутствует обработка задачи '{ taskType.ToString() }'", true);
                        continue;
                    }
                }

                if (tasksCompleted > 0)
                    WriteToLog($"Выполнено задач от кабинета работодателя: { tasksCompleted }", false);
            }
            //--------------------
            else
            {
                MessageBox.Show("Программу нужно запускать с одним из параметров:\n" +
                    //"Config - настройка задач;\n" +
                    "SendReports - отправка отчетов (через пробел указывается имя задачи);\n" +
                    //"CheckResultsConfig - настройка задач по получению результатов лабораторных исследований;\n" +
                    "CheckResults - запрос получения результатов лабораторных исследований;\n" +
                    //"ProcessMobileReceivedConfig - настройка задач по обработке данных, полученых от SiMedMobile;\n" +
                    "ProcessMobileReceived - выполнение задач по обработке данных, полученых от SiMedMobile;\n" +
                    //"SendReceptionRecordsReminderConfig - настройка задач по отправке напоминаний о записях на прием;\n" +
                    "SendReceptionRecordsReminder - отправка напоминаний о записях на прием;\n" +
                    //"CheckEmployerWebTasksConfig - настройка выполнения задач, полученных через личный кабинет работодателя\n" +
                    "CheckEmployerWebTasks TaskType - выполнение задач, полученных через личный кабинет работодателя. All - все, ImportMedInspectionRequest - импорт новой заявки на медосмотр, ExportMedInspectionResults - экспорт результатов медицинских осмотров\n" +
                    //"CheckIEMKRecordsStateConfig - настройка выполнения задач по получению статусов документов, отправленных в ЕГИСЗ" + 
                    "CheckIEMKRecordsState - выполнение задач по получению статусов документов, отправленных в ЕГИСЗ;\n" +
                    //"AutoCheckPaymentAccountingActsConfig - настройка выполнения задач по автоматической проверке оплаты актов оказания услуг" +
                    "AutoCheckPaymentAccountingActs - выполнение задач по автоматической проверке оплаты актов оказания услуг;\n" +
                    //"AutoCreateAccountingActsConfig - настройка выполнения задач по автоматическому созданию актов оказания услуг" +
                    "AutoCreateAccountingActs - выполнение задач по автоматическому созданию актов оказания услуг;\n" +
                    //"SendDefferedNotificationsConfig - настройка выполнения задач по отправке отложенных уведомлений" +
                    "SendDefferedNotifications - выполнение задач по отправке отложенных уведомлений"
                    , "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        public static void SendReports(int TaskNumber, ref AppConfig config)
        {
            string FileName;
            string FullPath = "";
            if (config.MailGlobalProperties.AttachedFilesDirectory.Length > 1 && config.MailGlobalProperties.AttachedFilesDirectory[1] == ':')
                FullPath = config.MailGlobalProperties.AttachedFilesDirectory;
            else
                FullPath = Application.StartupPath + '\\' + config.MailGlobalProperties.AttachedFilesDirectory;

            List<string> AttachedFiles = new List<string>();

            if (!Directory.Exists(FullPath))
                Directory.CreateDirectory(FullPath);

            string FileDirectory = FullPath + config.SendReportTasksProperties[TaskNumber].TaskName + "\\";
            if (!Directory.Exists(FileDirectory))
                Directory.CreateDirectory(FileDirectory);

            //формирование приложенных файлов
            List<SendReportProperties> reports_properties = config.SendReportTasksProperties[TaskNumber].SendReportsProperties;
            for (int i = 0; i < reports_properties.Count; i++)
            {
                FileName = CheckFileName(FileDirectory + reports_properties[i].GetValueFromName("FileName"));
                bool FileCreated = SiMed.Clinic.GUI.RepForm.ExecuteReport(reports_properties[i].ReportType, FileName, reports_properties[i].ReportParameters);

                if (FileCreated)
                    AttachedFiles.Add(FileName);
            }
            ClinicReportsCollection ReportCollection = Reports.GetClinicReports();
            //формирование комментариев к письму
            string Comments = "\n\nКомментарии к прикрепленным документам:\n";
            for (int i = 0; i < config.SendReportTasksProperties[TaskNumber].SendReportsProperties.Count; i++)
            {
                ClinicReportType ReportType = config.SendReportTasksProperties[TaskNumber].SendReportsProperties[i].ReportType;
                ClinicReport report = ReportCollection.Find(o => o.ReportType == ReportType);
                string ReportFileName = config.SendReportTasksProperties[TaskNumber].SendReportsProperties[i].GetValueFromName("FileName");
                string ReportComment = config.SendReportTasksProperties[TaskNumber].SendReportsProperties[i].ReportComment;
                string ReportName = report.Name;
                Comments += (i + 1).ToString() + ". " + ReportFileName + " - " + ReportName;
                if (!String.IsNullOrEmpty(ReportComment))
                    Comments += " // " + ReportComment;
                Comments += "\n";
            }
            
            //отправка письма
            SendMail mail = new SendMail(pSmtpServer: config.MailGlobalProperties.SmtpServer,
                pFromAddress: config.MailGlobalProperties.FromAddress,
                pFromPassword: config.MailGlobalProperties.FromPassword,
                pSenderAddress: config.MailGlobalProperties.SenderAddress,
                pEnableSSL: config.MailGlobalProperties.EnableSSL,
                pPort: config.MailGlobalProperties.Port,
                pMailBodyEncoding: config.MailGlobalProperties.MailBodyEncoding,
                pIsBodyHTML: config.MailGlobalProperties.IsBodyHTML);

            mail.Send(ptoAddresses: config.SendReportTasksProperties[TaskNumber].MailProperties.ToAddresses,
                pCopyToAddresses: config.SendReportTasksProperties[TaskNumber].MailProperties.CopyToAddresses,
                pletterCaption: config.SendReportTasksProperties[TaskNumber].MailProperties.LetterCaption,
                pletterMessage: config.SendReportTasksProperties[TaskNumber].MailProperties.LetterMessage + Comments,
                pattachFile: AttachedFiles);

            if (mail.ErrorMessage != "")
            {
                Console.WriteLine("Error: " + mail.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Письмо успешно отправлено");
            }

            //удаление отправленных файлов
            for (int i = 0; i < AttachedFiles.Count; i++)
                File.Delete(AttachedFiles[i]);
            //Console.WriteLine("Очистка временных файлов произведена успешно");
        }

        static private string CheckFileName(string FileName)
        {
            string FileNameWithoutExtension = Path.GetDirectoryName(FileName) + "\\" + Path.GetFileNameWithoutExtension(FileName);
            string Extention = Path.GetExtension(FileName);
            string tmpFileName = FileName;
            for (int i = 1; File.Exists(tmpFileName); i++)
            {
                tmpFileName = FileNameWithoutExtension + " [" + i.ToString() + "]" + Extention;
            }
            return tmpFileName;
        }

        private static void WriteToFile(string _FileName, string _Message)
        {
            FileStream fs = new FileStream(_FileName, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + _Message);
            sw.Flush();
            sw.Close();

        }
        public static void WriteToLog(string Message, bool Error)
        {
            string LogFileName = Application.StartupPath + "\\" + Properties.Settings.Default.LogFileName;
            string ErrorFileName = Application.StartupPath + "\\" + Properties.Settings.Default.ErrorLogFileName;
            WriteToFile(LogFileName, Message);
            if(Error)
                WriteToFile(ErrorFileName, Message);

            Console.WriteLine(Message);
        }
    }
}
