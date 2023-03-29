using System.ComponentModel.DataAnnotations;

namespace HGWork.Model
{
    public class ProjectUser
    {
        [Key]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }
}
