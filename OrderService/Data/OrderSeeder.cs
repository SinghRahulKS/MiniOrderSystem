using OrderService.Repository.Entity;
using static OrderService.Repository.Entity.Enums;

namespace OrderService.Data
{
    public static class OrderSeeder
    {
        public static async Task SeedAsync(OrderDbContext context)
        {
            if (!context.Orders.Any())
            {
                // ✅ Assume one Basket exists for this demo
                var basket = context.Baskets.FirstOrDefault();

                var orderId = Guid.NewGuid();

                var order = new Order
                {
                    Id = orderId,
                    UserId = basket?.UserId ?? Guid.NewGuid(),
                    BasketId = basket?.Id ?? Guid.NewGuid(),
                    OrderNumber = $"ORD-{DateTime.UtcNow.Ticks}",
                    Status = OrderStatus.Pending,
                    TotalAmount = 3499.99m,
                    CurrencyCode = "INR",
                    ShippingAddress = "123, MG Road, Bengaluru",
                    BillingAddress = "123, MG Road, Bengaluru",
                    CreatedBy = "system"
                };

                order.Items.Add(new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = orderId,
                    ProductId = Guid.NewGuid(),
                    ProductName = "iPhone 15 Pro",
                    Quantity = 1,
                    UnitPrice = 129999.00m,
                    TotalPrice = 129999.00m,
                    MainImage = "https://cdn.example.com/images/iphone15pro_main.jpg",
                    Image1 = "https://cdn.example.com/images/iphone15pro_side.jpg"
                });

                order.Items.Add(new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = orderId,
                    ProductId = Guid.NewGuid(),
                    ProductName = "AirPods Pro",
                    Quantity = 1,
                    UnitPrice = 24999.00m,
                    TotalPrice = 24999.00m,
                    MainImage = "https://cdn.example.com/images/airpodspro_main.jpg",
                    Image1 = "https://cdn.example.com/images/airpodspro_box.jpg"
                });


                context.Orders.Add(order);
                await context.SaveChangesAsync();
            }
        }
    }
}
