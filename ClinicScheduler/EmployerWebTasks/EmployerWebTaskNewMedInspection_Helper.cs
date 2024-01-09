using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ClinicScheduler.EmployerWebTasks
{
    public class EmployerWebTaskNewMedInspection_Helper
    {
        public static bool DoEmployerWebTask(ref string _FileBody, out string _Results, out string _ErrorMessage)
        {
            _Results = null;
            _ErrorMessage = null;
            try
            {
                EmployerWebTaskNewMedInspectionRequest taskImportRequestInfo = JsonConvert.DeserializeObject<EmployerWebTaskNewMedInspectionRequest>(_FileBody);

                string fileNameXLS = Path.GetTempFileName();
                File.WriteAllBytes(fileNameXLS, Convert.FromBase64String(taskImportRequestInfo.FileBodyBase64));
                Dictionary<string, object> Params = new Dictionary<string, object>();
                if (taskImportRequestInfo.MedInspectionRequestId.HasValue)
                    Params.Add("MedInspectionRequestId", taskImportRequestInfo.MedInspectionRequestId.Value);
                Params.Add("ContractId", taskImportRequestInfo.ContractId);
                Params.Add("BranchId", taskImportRequestInfo.BranchId);
                Params.Add("MedInspectionTypeId", taskImportRequestInfo.MedInspectionTypeId);
                if (!String.IsNullOrEmpty(taskImportRequestInfo.RequestNumber))
                    Params.Add("RequestNumber", taskImportRequestInfo.RequestNumber);
                Params.Add("PlannedDate", taskImportRequestInfo.PlannedDate);
                Params.Add("MedInspectionPlaceTypeId", taskImportRequestInfo.MedInspectionPlaceTypeId);
                if (taskImportRequestInfo.BranchMedInspectionMobilePlaceId.HasValue)
                    Params.Add("BranchMedInspectionMobilePlaceId", taskImportRequestInfo.BranchMedInspectionMobilePlaceId.Value);

                List<string> Errors = new List<string>();
                bool bRes = ProgramStart.ClinicApp.CurrentDB.ImportCustomRequest(fileNameXLS, Params, out Errors);
                File.Delete(fileNameXLS);

                if (bRes)
                {
                    EmployerWebTaskNewMedInspectionResult resilts = new EmployerWebTaskNewMedInspectionResult();
                    resilts.Success = true;
                    _Results = JsonConvert.SerializeObject(resilts);
                    return true;
                }
                else
                {
                    EmployerWebTaskNewMedInspectionResult resilts = new EmployerWebTaskNewMedInspectionResult();
                    resilts.Success = false;
                    resilts.ErrorMessages = Errors;
                    _Results = JsonConvert.SerializeObject(resilts);
                    return true;
                }
            }
            catch (Exception exc)
            {
                _ErrorMessage = $"{ exc.Message }\r\n{ exc.StackTrace }";
                return false;
            }
        }
    }
}
