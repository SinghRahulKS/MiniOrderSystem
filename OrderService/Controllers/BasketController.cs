using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Model.Request;
using OrderService.Service;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPost("add-item")]
        public async Task<IActionResult> AddItem([FromQuery] Guid? basketId, [FromBody] AddBasketItemRequest request)
        {
            request.BasketId = basketId;
            var result = await _basketService.AddItemAsync(request);
            return result.IsValid ? Ok(result) : BadRequest(result);
        }

        [HttpPut("update-quantity")]
        public async Task<IActionResult> UpdateQuantity([FromBody] UpdateBasketItemRequest request)
        {
            var result = await _basketService.UpdateItemQuantityAsync(request);
            return result.IsValid ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("remove-item")]
        public async Task<IActionResult> RemoveItem([FromQuery] Guid basketId, [FromQuery] Guid productId)
        {
            var result = await _basketService.RemoveItemAsync(basketId, productId);
            return result.IsValid ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{basketId:guid}")]
        public async Task<IActionResult> GetBasket(Guid basketId)
        {
            var result = await _basketService.GetBasketAsync(basketId);
            return result.IsValid ? Ok(result) : NotFound(result);
        }
        [HttpGet("by-user")]
        public async Task<IActionResult> GetBasketByUserOrBasket([FromQuery] Guid? userId, [FromQuery] Guid? basketId)
        {
            if (userId == null && basketId == null)
                return BadRequest(new { Message = "Either userId or basketId must be provided." });

            var result = await _basketService.GetBasketByUserOrBasketAsync(userId, basketId);
            return result.IsValid ? Ok(result) : NotFound(result);
        }
    }
}
