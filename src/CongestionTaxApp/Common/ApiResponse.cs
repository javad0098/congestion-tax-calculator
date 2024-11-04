namespace CongestionTaxApp.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; } 

        public T? Data { get; set; } 

        public string? ErrorMessage { get; set; } 

        public ApiErrorType? ErrorType { get; set; } 


        public static ApiResponse<T> SuccessResponse(T data)
        {
            return new ApiResponse<T> 
            { 
                Success = true, 
                Data = data 
            };
        }

        public static ApiResponse<T> ErrorResponse(string errorMessage, ApiErrorType errorType)
        {
            return new ApiResponse<T> 
            { 
                Success = false, 
                ErrorMessage = errorMessage, 
                ErrorType = errorType 
            };
        }
    }

    public enum ApiErrorType
    {
        None,
        ValidationError,
        NotFound,
        AlreadyExists,
        ServerError,
        Unauthorized,
        Forbidden
    }
}
