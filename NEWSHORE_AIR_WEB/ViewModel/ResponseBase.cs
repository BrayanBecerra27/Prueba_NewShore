namespace NEWSHORE_AIR_API.ViewModel
{
    public class ResponseBase<T>
    {
        public int StatusCode { get; set; }

        public T Data { get; set; }
    }
}
