using System.ComponentModel.DataAnnotations;

namespace ProductService.Repository.Entity
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        [MaxLength(255)]
        public string? Image { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = "system";
        public DateTime? UpdatedOn { get; set; } = DateTime.UtcNow;
        public string UpdatedBy { get; set; } = "system";

        public ICollection<Product> Products { get; set; }
    }
}
