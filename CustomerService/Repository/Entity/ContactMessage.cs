using System.ComponentModel.DataAnnotations;

namespace UserService.Repository.Entity
{
    public class ContactMessage 
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, EmailAddress, MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(150)]
        public string? Subject { get; set; }

        [Required, MaxLength(1000)]
        public string Message { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
    }
}
