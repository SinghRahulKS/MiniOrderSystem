namespace ProductService.Model.Response
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal SellPrice { get; set; }
        public string Currency { get; set; }
        public Guid CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? MainImage { get; set; }
        public string? Image1 { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = "system";
        public DateTime? UpdatedOn { get; set; } = DateTime.UtcNow;
        public string UpdatedBy { get; set; } = "system";
    }
}
