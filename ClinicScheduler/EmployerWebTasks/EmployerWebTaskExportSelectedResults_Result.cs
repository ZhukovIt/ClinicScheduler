using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClinicScheduler.EmployerWebTasks
{
    public class EmployerWebTaskExportSelectedResults_Result
    {
        public bool Success { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
