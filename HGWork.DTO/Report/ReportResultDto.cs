using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGWork.DTO.Report
{
    public class ReportResultDto
    {
        public int TotalProjects { get; set; }
        public int TotalTasks { get; set; }
        public int TotalTasksDone { get; set; }
        public int TotalTasksDoing { get; set; }
        public int TotalTasksCanceled { get; set; }
    }
}
