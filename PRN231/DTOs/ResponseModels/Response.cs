using PRN231.DTOs.Enum;

namespace PRN231.DTOs.ResponseModels
{
    public class Response
    {
        public ResponseStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public bool IsError { get; set; }
        public int ID { get; set; }
    }
    public class ListDataOutput<T> : Response
    {
        public ListDataOutput()
        {
            Data = new List<T>();
        }
        public int TotalRecord { get; set; }
        public List<T> Data { get; set; }
    }
    public class DataOutput<T> : Response
    {
        public T Data { get; set; }
    }
}
