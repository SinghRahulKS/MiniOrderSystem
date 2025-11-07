using ProductService.Repository.Entity;

namespace ProductService.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(Guid id);
        Task<Category> AddAsync(Category category);
        Task<Category?> UpdateAsync(Category category);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsByIdAsync(Guid id);
        Task<bool> ExistsByNameAsync(string name);
    }
}
