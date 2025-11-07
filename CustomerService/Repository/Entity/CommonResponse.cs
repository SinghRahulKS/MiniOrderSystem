namespace UserService.Repository.Entity
{
    public class CommonResponse<T>
    {
        public Guid Id { get; set; }
        public bool IsValid { get; set; }
        public string Message { get; set; } = "";
        public T? Data { get; set; }

        public static CommonResponse<T> Ok(T data, string msg = "Success") =>
            new() { IsValid = true, Message = msg, Data = data };

        public static CommonResponse<T> Fail(string msg) =>
            new() { IsValid = false, Message = msg };
    }
}
