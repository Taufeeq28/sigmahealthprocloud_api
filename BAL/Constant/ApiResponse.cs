
namespace BAL.Constant
{
    public class ApiResponse<T> where T : class
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static ApiResponse<T> Success(T? data, string message = "Operation successful.")
        {
            return new ApiResponse<T> { Status = "Success",Message = message, Data = data };
        }

        public static ApiResponse<T> Fail(string message = "Operation failed.")
        {
            return new ApiResponse<T> { Status = "Fail", Message = message, Data = null };
        }
    }

}
