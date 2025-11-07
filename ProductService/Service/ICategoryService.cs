using ProductService.Model.Request;
using ProductService.Model.Response;
using ProductService.Repository.Entity;

namespace ProductService.Service
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponse>> GetAllAsync();
        Task<CategoryResponse?> GetByIdAsync(Guid id);
        Task<CategoryResponse> CreateAsync(CategoryCreateRequest request);
        Task<CategoryResponse?> UpdateAsync(Guid id, CategoryCreateRequest request);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsByNameAsync(string name);
        Task<bool> ExistsByIdAsync(Guid id);
    }
}
