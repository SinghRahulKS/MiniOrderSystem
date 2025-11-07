using OrderService.Model.Request;
using OrderService.Model.Response;
using OrderService.Repository.Entity;

namespace OrderService.Service
{
    public interface IOrderService
    {
        Task<ApiResponse<OrderResponse>> CreateOrderAsync(CreateOrderRequest request);
        Task<ApiResponse<OrderResponse>> GetOrderByIdAsync(Guid id);
        Task<ApiResponse<IEnumerable<OrderResponse>>> GetOrdersByUserAsync(Guid userId);
        Task<ApiResponse<string>> UpdateOrderStatusAsync(Guid orderId, int status);
    }
}
