using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGWork.DTO.Report
{
    public class ReportRequestDto
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
