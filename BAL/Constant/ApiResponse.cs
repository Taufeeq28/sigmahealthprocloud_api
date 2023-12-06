
namespace BAL.Constant
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static ApiResponse<T> Success(T data, string message = "Operation successful.")
        {
            return new ApiResponse<T> { IsSuccess = true, Message = message, Data = data };
        }

        public static ApiResponse<T> Fail(T data,string message = "Operation failed.")
        {
            return new ApiResponse<T> { IsSuccess = false, Message = message,Data=data };
        }
    }
}
