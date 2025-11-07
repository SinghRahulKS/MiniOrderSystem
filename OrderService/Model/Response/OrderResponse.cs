using static OrderService.Repository.Entity.Enums;

namespace OrderService.Model.Response
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public Guid BasketId { get; set; }
        public Guid UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemResponse> Items { get; set; } = new();
    }

    public class OrderItemResponse
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string Currency { get; set; } = "INR";
        public string CurrencySymbol { get; set; } = "₹";
    }
}
