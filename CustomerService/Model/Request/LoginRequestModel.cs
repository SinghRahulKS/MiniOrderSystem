using System.ComponentModel.DataAnnotations;

namespace UserService.Model.Request
{
    public class LoginRequestModel
    {
        [EmailAddress]
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsGoogleLogin { get; set; } = false;
        public string? GoogleToken { get; set; }
    }
}
