using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClinicScheduler
{
    public class EmployerWebTaskNewMedInspectionRequest
    {
        public int? MedInspectionRequestId { get; set; }
        public string JobId { get; set; }
        public int JobTypeId { get; set; }
        public DateTime JobExpiredAt { get; set; }
        public int BranchId { get; set; }
        public int MedInspectionTypeId { get; set; }
        public int ContractId { get; set; }
        public DateTime? PlannedDate { get; set; }
        public string RequestNumber { get; set; }
        public string FileBodyBase64 { get; set; }
        public int MedInspectionPlaceTypeId { get; set; }
        public int? BranchMedInspectionMobilePlaceId { get; set; }
    }
}
