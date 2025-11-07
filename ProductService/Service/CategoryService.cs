using AutoMapper;
using ProductService.Model.Request;
using ProductService.Model.Response;
using ProductService.Repository;
using ProductService.Repository.Entity;

namespace ProductService.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryResponse>>(categories);
        }

        public async Task<CategoryResponse?> GetByIdAsync(Guid id)
        {
            var category = await _repository.GetByIdAsync(id);
            return category == null ? null : _mapper.Map<CategoryResponse>(category);
        }

        public async Task<CategoryResponse> CreateAsync(CategoryCreateRequest request)
        {
            var category = _mapper.Map<Category>(request);
            category.Id = Guid.NewGuid();

            var created = await _repository.AddAsync(category);
            return _mapper.Map<CategoryResponse>(created);
        }

        public async Task<CategoryResponse?> UpdateAsync(Guid id, CategoryCreateRequest request)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Category with ID {id} not found.");

            _mapper.Map(request, existing);
            var updated = await _repository.UpdateAsync(existing);

            return _mapper.Map<CategoryResponse>(updated);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _repository.ExistsByNameAsync(name);
        }

        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            return await _repository.ExistsByIdAsync(id);
        }
    }
}
