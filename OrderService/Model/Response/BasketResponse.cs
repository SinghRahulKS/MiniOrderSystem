namespace OrderService.Model.Response
{
    public class BasketResponse
    {
        public Guid BasketId { get; set; }
        public Guid UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Currency { get; set; } = "INR";
        public string CurrencySymbol { get; set; } = "₹";
        public List<BasketItemResponse> Items { get; set; } = new();
    }

    public class BasketItemResponse
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalItemPrice { get; set; }
    }
}