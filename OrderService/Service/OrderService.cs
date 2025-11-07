using AutoMapper;
using OrderService.Model.Request;
using OrderService.Model.Response;
using OrderService.Repository;
using OrderService.Repository.Entity;
using static OrderService.Repository.Entity.Enums;

namespace OrderService.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;

        public OrderService(
            IOrderRepository orderRepo,
            IBasketService basketService,
            IMapper mapper,
            ILogger<OrderService> logger)
        {
            _orderRepo = orderRepo;
            _basketService = basketService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponse<OrderResponse>> CreateOrderAsync(CreateOrderRequest request)
        {
            // ✅ Validate Basket
            var basketResult = await _basketService.GetBasketAsync(request.BasketId);
            if (!basketResult.IsValid || basketResult.Data == null)
                return ApiResponse<OrderResponse>.Fail("Basket not found.");

            var basket = basketResult.Data;

            if (basket.Items == null || !basket.Items.Any())
                return ApiResponse<OrderResponse>.Fail("Basket is empty. Cannot create an order.");

            // ✅ Create Order
            var order = _mapper.Map<Order>(request);
            order.Status = OrderStatus.Pending;
            order.TotalAmount = basket.TotalPrice;
            order.CreatedAt = DateTime.UtcNow;

            // Map basket items → order items
            order.Items = basket.Items.Select(item => new OrderItem
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                UnitPrice = item.Price,
                MainImage = item.ImageUrl,
                TotalPrice = item.Quantity * item.Price
            }).ToList();

            var createdOrder = await _orderRepo.CreateOrderAsync(order);

            // ✅ Update Basket Status
            await _basketService.UpdateBasketStatusAsync(request.BasketId, "CheckedOut");

            var response = _mapper.Map<OrderResponse>(createdOrder);
            return ApiResponse<OrderResponse>.Success(response, "Order created successfully.");
        }

        public async Task<ApiResponse<OrderResponse>> GetOrderByIdAsync(Guid id)
        {
            var order = await _orderRepo.GetOrderByIdAsync(id);
            if (order == null)
                return ApiResponse<OrderResponse>.Fail("Order not found.");

            var response = _mapper.Map<OrderResponse>(order);
            return ApiResponse<OrderResponse>.Success(response, "Order retrieved successfully.");
        }

        public async Task<ApiResponse<IEnumerable<OrderResponse>>> GetOrdersByUserAsync(Guid userId)
        {
            var orders = await _orderRepo.GetOrdersByUserAsync(userId);
            var response = _mapper.Map<IEnumerable<OrderResponse>>(orders);
            return ApiResponse<IEnumerable<OrderResponse>>.Success(response, "User orders fetched successfully.");
        }

        public async Task<ApiResponse<string>> UpdateOrderStatusAsync(Guid orderId, int status)
        {
            var order = await _orderRepo.GetOrderByIdAsync(orderId);
            if (order == null)
                return ApiResponse<string>.Fail("Order not found.");

            order.Status = (OrderStatus)status;
            await _orderRepo.SaveChangesAsync();

            return ApiResponse<string>.Success("Order status updated successfully.");
        }
    }
}
