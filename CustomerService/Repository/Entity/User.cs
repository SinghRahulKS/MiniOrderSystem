using System.ComponentModel.DataAnnotations;
using System.Net;

namespace UserService.Repository.Entity
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Phone]
        public string PhoneNo { get; set; }

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public string? Role { get; set; } = "Customer";
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public string CreatedBy { get; set; } = "system";
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
