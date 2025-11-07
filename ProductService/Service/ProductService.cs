using AutoMapper;
using ProductService.Model.Request;
using ProductService.Model.Response;
using ProductService.Repository.Entity;
using ProductService.Repository;

namespace ProductService.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PagedResponse<ProductResponse>> GetAllAsync(ProductFilterRequest filter, int pageNumber, int pageSize)
        {
            var products = await _repo.GetFilteredAsync(filter, pageNumber, pageSize);
            var totalCount = await _repo.GetTotalCountAsync(filter);

            return new PagedResponse<ProductResponse>
            {
                Items = _mapper.Map<IEnumerable<ProductResponse>>(products),
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<ProductResponse?> GetByIdAsync(Guid id)
        {
            var product = await _repo.GetByIdAsync(id);
            return _mapper.Map<ProductResponse?>(product);
        }

        public async Task<ProductResponse> CreateAsync(CreateProductRequest request)
        {
            var entity = _mapper.Map<Product>(request);
            entity.Id = Guid.NewGuid();

            await _repo.CreateAsync(entity);
            return _mapper.Map<ProductResponse>(entity);
        }

        public async Task<ProductResponse?> UpdateAsync(Guid id, CreateProductRequest request)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            _mapper.Map(request, existing);
            await _repo.UpdateAsync(existing);

            return _mapper.Map<ProductResponse>(existing);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
}
