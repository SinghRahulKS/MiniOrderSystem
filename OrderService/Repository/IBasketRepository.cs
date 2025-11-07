using OrderService.Repository.Entity;
using System.Threading.Tasks;

namespace OrderService.Repository
{
    public interface IBasketRepository
    {
        Task<Basket?> GetBasketAsync(Guid basketId);
        Task<Basket> CreateBasketAsync(Basket basket);
        Task<Basket?> UpdateBasketAsync(Basket basket);
        Task DeleteItemAsync(Guid basketId, Guid productId);
        Task SaveChangesAsync();
        Task<bool> UpdateBasketStatusAsync(Guid basketId, string status);
        Task<bool> ExistsAsync(Guid basketId);
        Task<Basket?> GetBasketByUserIdAsync(Guid UserId);
    }
}
