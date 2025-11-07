using Microsoft.AspNetCore.Mvc;
using ProductService.Model.Request;
using ProductService.Model.Response;
using ProductService.Service;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(
            IProductService productService,
            ICategoryService categoryService,
            ILogger<ProductsController> logger)
        {
            _productService = productService;
            _categoryService = categoryService;
            _logger = logger;
        }

        // ✅ GET: api/products
        [HttpGet]
        public async Task<ActionResult<PagedResponse<ProductResponse>>> GetAll([FromQuery] ProductFilterRequest filter,[FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
                return BadRequest("PageNumber and PageSize must be greater than 0.");

            var result = await _productService.GetAllAsync(filter, pageNumber, pageSize);
            return Ok(result);
        }
        // ✅ GET: api/products/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductResponse>> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid product ID.");

            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            return Ok(product);
        }

        // ✅ POST: api/products
        [HttpPost]
        public async Task<ActionResult<ProductResponse>> Create([FromBody] CreateProductRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryExists = await _categoryService.ExistsByIdAsync(request.CategoryId);
            if (!categoryExists)
                return BadRequest($"Category with ID {request.CategoryId} does not exist.");

            var created = await _productService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // ✅ PUT: api/products/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateProductRequest request)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid product ID.");

            var existing = await _productService.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Product with ID {id} not found.");

            await _productService.UpdateAsync(id, request);
            return NoContent();
        }

        // ✅ DELETE: api/products/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid product ID.");

            var deleted = await _productService.DeleteAsync(id);
            if (!deleted)
                return NotFound($"Product with ID {id} not found.");

            return NoContent();
        }
    }
}
