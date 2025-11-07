using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OrderService.Repository.Entity
{
    public class Basket
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string Status { get; set; } = "Active"; // Active, CheckedOut, Abandoned
        public decimal TotalPrice { get; set; }
        public string Currency { get; set; } = "INR";
        public string CurrencySymbol { get; set; } = "₹";

        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; } = DateTime.UtcNow;

        public ICollection<BasketItem> Items { get; set; } = new List<BasketItem>();
    }

    public class BasketItem
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [ForeignKey(nameof(Basket))]
        public Guid BasketId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required, MaxLength(150)]
        public string ProductName { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public string Currency { get; set; } = "INR";
        public string CurrencySymbol { get; set; } = "₹";
        [JsonIgnore]
        public Basket Basket { get; set; } = null!;
    }
}