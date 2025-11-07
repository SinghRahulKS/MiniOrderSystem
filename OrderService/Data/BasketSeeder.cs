using OrderService.Repository.Entity;

namespace OrderService.Data
{
    public static class BasketSeeder
    {
        public static async Task SeedAsync(OrderDbContext context)
        {
            if (!context.Baskets.Any())
            {
                var basketId = Guid.NewGuid();

                var basket = new Basket
                {
                    Id = basketId,
                    UserId = Guid.NewGuid(),
                    Status = "Active"
                };

                basket.Items.Add(new BasketItem
                {
                    Id = Guid.NewGuid(),
                    BasketId = basketId,
                    ProductId = Guid.NewGuid(),
                    ProductName = "Wireless Mouse",
                    Quantity = 2,
                    Price = 599.50m
                });

                basket.Items.Add(new BasketItem
                {
                    Id = Guid.NewGuid(),
                    BasketId = basketId,
                    ProductId = Guid.NewGuid(),
                    ProductName = "USB Keyboard",
                    Quantity = 1,
                    Price = 899.00m
                });

                context.Baskets.Add(basket);
                await context.SaveChangesAsync();
            }
        }
    }
}
