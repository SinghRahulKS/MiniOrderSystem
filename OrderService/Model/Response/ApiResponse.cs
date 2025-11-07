namespace OrderService.Model.Response
{
    public class ApiResponse<T>
    {
        public bool IsValid { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static ApiResponse<T> Success(T data, string message = "Success")
            => new ApiResponse<T> { IsValid = true, Message = message, Data = data };

        public static ApiResponse<T> Fail(string message)
            => new ApiResponse<T> { IsValid = false, Message = message };
    }
}
