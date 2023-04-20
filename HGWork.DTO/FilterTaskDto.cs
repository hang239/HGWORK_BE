namespace HGWork.DTO
{
    public class FilterTaskDto
    {
        public int UserId { get; set; }
        public int Status { get; set; }
        public int ProjectId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
