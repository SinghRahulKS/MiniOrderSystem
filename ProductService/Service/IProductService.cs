using ProductService.Model.Request;
using ProductService.Model.Response;

namespace ProductService.Service
{
    public interface IProductService
    {
        Task<PagedResponse<ProductResponse>> GetAllAsync(ProductFilterRequest filter, int pageNo, int pageSize);
        Task<ProductResponse?> GetByIdAsync(Guid id);
        Task<ProductResponse> CreateAsync(CreateProductRequest request);
        Task<ProductResponse?> UpdateAsync(Guid id, CreateProductRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
