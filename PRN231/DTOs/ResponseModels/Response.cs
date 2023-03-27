using PRN231.DTOs.Enum;
using PRN231.Entities;

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

    public class JobResponse
    {
        public int ID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ExpiredDate { get; set; }

        public string JobTitle { get; set; } = null!;

        public decimal? MinSalary { get; set; }

        public decimal? MaxSalary { get; set; }

        public string? Skills { get; set; }
    }
    public class JobByIdResponse : Job
    {

        public string? Skills { get; set; }
    }
    public class AnalysisResponse 
    {
        public int NumDe { get; set; }
        public int NumJob { get; set; }
        public int NumCan { get; set; }
        public int NumInter { get; set; }
    }
    public class LoginRequest
    {
        public string? email { get; set; }
        public string? password { get; set; }
    }
    public class MailRequest
    {
        public string? ToEmail { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }

    }
    public class LoginResponse
    {
        public string? FullName { get; set; }
    }
    public class ForgotResponse
    {
        public string? Message { get; set; }
    }
}
