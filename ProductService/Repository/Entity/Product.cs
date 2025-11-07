using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Repository.Entity
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Sell Price must be greater than 0")]
        public decimal SellPrice { get; set; }

        [Required, MaxLength(10)]
        public string Currency { get; set; } = "INR";

        [Required, MaxLength()]
        public string CurrencyCode { get; set; } = "₹";
        [MaxLength(255)]
        public string? MainImage { get; set; }

        [MaxLength(255)]
        public string? Image1 { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }

        // Navigation Property
        public Category Category { get; set; } = null!;

        // ✅ Timestamps (best practice for tracking)
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = "system";
        public DateTime? UpdatedOn { get; set; } = DateTime.UtcNow;
        public string UpdatedBy { get; set; } = "system";
    }
}
