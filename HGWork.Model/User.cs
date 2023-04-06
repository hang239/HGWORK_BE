using System.ComponentModel.DataAnnotations;

namespace HGWork.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsAdmin { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}