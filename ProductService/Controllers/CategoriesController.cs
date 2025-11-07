using Microsoft.AspNetCore.Mvc;
using ProductService.Model.Request;
using ProductService.Model.Response;
using ProductService.Service;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoryService service, ILogger<CategoriesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // ✅ GET: api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetAll()
        {
            var categories = await _service.GetAllAsync();
            return Ok(categories);
        }

        // ✅ GET: api/categories/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponse>> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid category ID.");

            var category = await _service.GetByIdAsync(id);
            if (category == null)
                return NotFound($"Category with ID {id} not found.");

            return Ok(category);
        }

        // ✅ POST: api/categories
        [HttpPost]
        public async Task<ActionResult<CategoryResponse>> Create([FromBody] CategoryCreateRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exists = await _service.ExistsByNameAsync(req.Name);
            if (exists)
                return Conflict($"Category with name '{req.Name}' already exists.");

            var created = await _service.CreateAsync(req);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // ✅ PUT: api/categories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CategoryCreateRequest req)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid category ID.");

            var exists = await _service.ExistsByIdAsync(id);
            if (!exists)
                return NotFound($"Category with ID {id} not found.");

            await _service.UpdateAsync(id, req);
            return NoContent();
        }

        // ✅ DELETE: api/categories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid category ID.");

            var exists = await _service.ExistsByIdAsync(id);
            if (!exists)
                return NotFound($"Category with ID {id} not found.");

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
