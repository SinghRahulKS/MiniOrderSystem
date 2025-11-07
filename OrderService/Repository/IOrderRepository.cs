using OrderService.Repository.Entity;

namespace OrderService.Repository
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderByIdAsync(Guid id);
        Task<IEnumerable<Order>> GetOrdersByUserAsync(Guid userId);
        Task<Order> CreateOrderAsync(Order order);
        Task UpdateOrderStatusAsync(Guid orderId, int status);
        Task SaveChangesAsync();
    }
}
