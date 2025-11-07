using System.ComponentModel.DataAnnotations;

namespace ProductService.Model.Request
{
    public class CreateProductRequest
    {
        [Required, MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal SellPrice { get; set; }

        [Required, MaxLength(10)]
        public string Currency { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        public string? MainImage { get; set; }
        public string? Image1 { get; set; }
    }
}
