namespace OrderService.Model.Request
{
    public class AddBasketItemRequest
    {
        public Guid? BasketId { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Currency { get; set; } = "INR";
        public string CurrencySymbol { get; set; } = "₹";
    }

    public class UpdateBasketItemRequest
    {
        public Guid BasketId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
