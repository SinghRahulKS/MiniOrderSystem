using OrderService.Model.Request;
using OrderService.Model.Response;
using OrderService.Repository.Entity;

namespace OrderService.Service
{
    public interface IBasketService
    {
        Task<ApiResponse<Basket>> AddItemAsync(AddBasketItemRequest request);
        Task<ApiResponse<Basket>> UpdateItemQuantityAsync(UpdateBasketItemRequest request);
        Task<ApiResponse<Basket>> RemoveItemAsync(Guid basketId, Guid productId);
        Task<ApiResponse<Basket>> GetBasketAsync(Guid basketId);
        Task<ApiResponse<string>> UpdateBasketStatusAsync(Guid basketId, string status);
        Task<ApiResponse<Basket>> GetBasketByUserOrBasketAsync(Guid? userId, Guid? basketId);

    }
}
