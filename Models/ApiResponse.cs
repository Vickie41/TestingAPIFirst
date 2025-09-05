namespace FirstTestingAPI.Models
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public bool Success { get; set; }
        public List<string>? Errors { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public static ApiResponse<T> SuccessResponse(T data, string message = "Success", int statusCode = 200)
        {
            return new ApiResponse<T>
            {
                StatusCode = statusCode,
                Message = message,
                Data = data,
                Success = true,
                Timestamp = DateTime.UtcNow
            };
        }

        public static ApiResponse<T> ErrorResponse(string message, int statusCode = 400, List<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                StatusCode = statusCode,
                Message = message,
                Success = false,
                Errors = errors,
                Timestamp = DateTime.UtcNow
            };
        }

        public static ApiResponse<T> ValidationErrorResponse(List<string> validationErrors)
        {
            return new ApiResponse<T>
            {
                StatusCode = 400,
                Message = "Validation failed",
                Success = false,
                Errors = validationErrors,
                Timestamp = DateTime.UtcNow
            };
        }
    }
}