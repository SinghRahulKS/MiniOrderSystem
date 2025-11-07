using AutoMapper;
using OrderService.Model.Request;
using OrderService.Model.Response;
using OrderService.Repository;
using OrderService.Repository.Entity;

namespace OrderService.Service
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<Basket>> AddItemAsync(AddBasketItemRequest request)
        {
            // 🧩 Load existing basket or create new
            var basket = request.BasketId.HasValue
                ? await _basketRepository.GetBasketAsync(request.BasketId.Value)
                : await _basketRepository.GetBasketByUserIdAsync(request.UserId) ?? new Basket
                {
                    Id = request.BasketId ?? Guid.NewGuid(),
                    UserId = request.UserId,
                    Status = "Active",
                    Currency = "INR",
                    CurrencySymbol = "₹",
                    Items = new List<BasketItem>()
                };

            // 🧩 Add or update item
            var existingItem = basket.Items.FirstOrDefault(i => i.ProductId == request.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += request.Quantity;
            }
            else
            {
                var newItem = _mapper.Map<BasketItem>(request);
                newItem.Id = Guid.NewGuid();
                newItem.BasketId = basket.Id;
                basket.Items.Add(newItem);
            }

            // 🧩 Update total
            basket.TotalPrice = basket.Items.Sum(i => i.Price * i.Quantity);

            // 🧩 Save changes
            if (await _basketRepository.ExistsAsync(basket.Id))
                basket = await _basketRepository.UpdateBasketAsync(basket);
            else
                basket = await _basketRepository.CreateBasketAsync(basket);

            return ApiResponse<Basket>.Success(basket, "Item added or updated successfully");
        }

        public async Task<ApiResponse<Basket>> UpdateItemQuantityAsync(UpdateBasketItemRequest request)
        {
            var basket = await _basketRepository.GetBasketAsync(request.BasketId);
            if (basket == null) return ApiResponse<Basket>.Fail("Basket not found");

            var item = basket.Items.FirstOrDefault(i => i.ProductId == request.ProductId);
            if (item == null) return ApiResponse<Basket>.Fail("Product not found in basket");

            // ✅ Update quantity and recalc total
            item.Quantity = request.Quantity;
            basket.TotalPrice = basket.Items.Sum(i => i.Price * i.Quantity);

            basket = await _basketRepository.UpdateBasketAsync(basket);
            return ApiResponse<Basket>.Success(basket, "Quantity updated successfully");
        }

        public async Task<ApiResponse<Basket>> RemoveItemAsync(Guid basketId, Guid productId)
        {
            await _basketRepository.DeleteItemAsync(basketId, productId);

            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket == null) return ApiResponse<Basket>.Fail("Basket not found");

            basket.TotalPrice = basket.Items.Sum(i => i.Price * i.Quantity);
            basket = await _basketRepository.UpdateBasketAsync(basket);

            return ApiResponse<Basket>.Success(basket, "Item removed successfully");
        }

        public async Task<ApiResponse<Basket>> GetBasketAsync(Guid basketId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            return basket == null
                ? ApiResponse<Basket>.Fail("Basket not found")
                : ApiResponse<Basket>.Success(basket);
        }

        public async Task<ApiResponse<Basket>> GetBasketByUserOrBasketAsync(Guid? userId, Guid? basketId)
        {
            Basket basket = null;

            if (userId.HasValue && userId != Guid.Empty)
                basket = await _basketRepository.GetBasketByUserIdAsync(userId.Value);

            if (basket == null && basketId.HasValue && basketId != Guid.Empty)
                basket = await _basketRepository.GetBasketAsync(basketId.Value);

            if (basket == null)
                return ApiResponse<Basket>.Fail("Basket not found");

            return ApiResponse<Basket>.Success(basket, "Basket retrieved successfully");
        }

        public async Task<ApiResponse<string>> UpdateBasketStatusAsync(Guid basketId, string status)
        {
            // Check if basket exists
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket == null)
                return ApiResponse<string>.Fail("Basket not found.");

            // Update the status and timestamp
            basket.Status = status;
            basket.UpdatedOn = DateTime.UtcNow;

            await _basketRepository.SaveChangesAsync();

            return ApiResponse<string>.Success($"Basket status updated to '{status}'.");
        }

    }
}
