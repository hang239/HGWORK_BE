namespace HGWork.DTO
{
    public class ResponseBase<TData>
    {
        public int StatusCode { get; set; }
        public TData? Data { get; set; }
        public string? Message { get; set; }
    }
}