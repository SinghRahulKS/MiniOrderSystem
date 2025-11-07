using ProductService.Model.Request;
using ProductService.Repository.Entity;

namespace ProductService.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetFilteredAsync(ProductFilterRequest filter, int pageNumber, int pageSize);
        Task<int> GetTotalCountAsync(ProductFilterRequest filter);
        Task<Product?> GetByIdAsync(Guid id);
        Task CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task<bool> DeleteAsync(Guid id);
    }
}
