using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClinicScheduler.EmployerWebTasks
{
    public class EmployerWebTaskExportSelectedResults_Request
    {
        public string MedOrgOGRN { get; set; }
        public string MedOrgINN { get; set; }
        public string MedOrgKPP { get; set; }
        public string OrgOGRN { get; set; }
        public string OrgINN { get; set; }
        public string OrgKPP { get; set; }
        public int MedInspectionRequestId { get; set; }
        public string JobId { get; set; }
        public int JobTypeId { get; set; }
        public DateTime JobExpiredAt { get; set; }
        public DateTime? FilterPeriodFrom { get; set; }
        public DateTime? FilterPeriodTill { get; set; }
        public int? FilterDocNum { get; set; }
        public int? FilterDocState { get; set; }
        public int? FilterPostSubDiv { get; set; }
        public int? FilterRes { get; set; }
        public string FilterFindText { get; set; }
        public string ArchivePassword { get; set; }
        public string CallbackURL { get; set; }
    }
}
