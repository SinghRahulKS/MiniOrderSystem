namespace UserService.Model.Request
{
    public class RegisterRequestModel
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? PhoneNo { get; set; }
    }
}
