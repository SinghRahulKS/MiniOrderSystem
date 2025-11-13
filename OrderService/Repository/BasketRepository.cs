using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Repository.Entity;

namespace OrderService.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly OrderDbContext _context;
        public BasketRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Basket?> GetBasketAsync(Guid basketId) =>
            await _context.Baskets.Include(b => b.Items).FirstOrDefaultAsync(b => b.Id == basketId);

        public async Task<Basket?> GetBasketByUserIdAsync(Guid userId) =>
            await _context.Baskets
                .Include(b => b.Items)
                .FirstOrDefaultAsync(b => b.UserId == userId && b.Status == "Active");

        public async Task<Basket> CreateBasketAsync(Basket basket)
        {
            if (basket.Id == Guid.Empty)
                basket.Id = Guid.NewGuid();

            foreach (var item in basket.Items)
            {
                item.Id = Guid.NewGuid();
                item.BasketId = basket.Id;
            }

            _context.Baskets.Add(basket);
            await _context.SaveChangesAsync();
            return basket;
        }

        public async Task<Basket> UpdateBasketAsync(Basket basket)
        {
            var existingBasket = await _context.Baskets
                .Include(b => b.Items)
                .FirstOrDefaultAsync(b => b.Id == basket.Id);

            if (existingBasket == null)
                throw new InvalidOperationException($"Basket with Id {basket.Id} not found.");

            // ✅ Update basket properties
            existingBasket.TotalPrice = basket.TotalPrice;
            existingBasket.Status = basket.Status ?? existingBasket.Status;
            existingBasket.UpdatedOn = DateTime.UtcNow;
            existingBasket.Currency = basket.Currency ?? existingBasket.Currency;
            existingBasket.CurrencySymbol = basket.CurrencySymbol ?? existingBasket.CurrencySymbol;

            // ✅ Sync basket items
            foreach (var item in basket.Items)
            {
                var existingItem = existingBasket.Items.FirstOrDefault(i => i.ProductId == item.ProductId);
                if (existingItem != null)
                {
                    existingItem.Quantity = item.Quantity;
                    existingItem.Price = item.Price;
                }
                else
                {
                    var newItem = new BasketItem
                    {
                        Id = Guid.NewGuid(),
                        BasketId = existingBasket.Id,
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        ImageUrl = item.ImageUrl,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        Currency = item.Currency,
                        CurrencySymbol = item.CurrencySymbol
                    };
                    existingBasket.Items.Add(newItem);
                }
            }

            await _context.SaveChangesAsync();
            return existingBasket;
        }

        public async Task DeleteItemAsync(Guid basketId, Guid productId)
        {
            var item = await _context.BasketItems
                .FirstOrDefaultAsync(x => x.BasketId == basketId && x.ProductId == productId);

            if (item != null)
            {
                _context.BasketItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> UpdateBasketStatusAsync(Guid basketId, string status)
        {
            var basket = await _context.Baskets.FirstOrDefaultAsync(b => b.Id == basketId);
            if (basket == null)
                return false;

            basket.Status = status;
            basket.UpdatedOn = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid basketId)
        {
            return await _context.Baskets.AnyAsync(b => b.Id == basketId);
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
