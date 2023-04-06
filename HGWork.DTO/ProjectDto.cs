using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGWork.DTO
{
    public class ProjectDto
    {
        public ProjectDto() 
        {
            this.CreatedDate = DateTime.Now;
            this.Status = 0;
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
