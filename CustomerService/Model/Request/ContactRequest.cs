using System.ComponentModel.DataAnnotations;

namespace UserService.Model.Request
{
    public class ContactRequest
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, EmailAddress, MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(150)]
        public string? Subject { get; set; }

        [Required, MaxLength(1000)]
        public string Message { get; set; }
    }
}
