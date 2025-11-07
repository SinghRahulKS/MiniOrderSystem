using Microsoft.EntityFrameworkCore;
using ProductService.Model.Request;
using ProductService.Repository.Entity;
using System;

namespace ProductService.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext
            context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetFilteredAsync(ProductFilterRequest filter, int pageNumber, int pageSize)
        {
            // ✅ Start with a plain IQueryable and apply Include later (avoid IIncludableQueryable mismatch)
            IQueryable<Product> query = _context.Products.AsQueryable();

            // 🔍 Search by Name or Description
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                string searchPattern = $"%{filter.Search.Trim()}%";
                query = query.Where(p =>
                    EF.Functions.Like(p.Name, searchPattern) ||
                    (p.Description != null && EF.Functions.Like(p.Description, searchPattern)));
            }
            // 🏷 Filter by Category
            if (filter.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == filter.CategoryId.Value);
            }

            // 💰 Filter by Price Range
            if (filter.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);
            }

            // ↕ Sorting (Case-insensitive, structured, using StringComparison)
            query = ApplySorting(query, filter.SortBy, filter.SortDirection);

            // 📦 Include Category (added after filtering/sorting)
            query = query.Include(p => p.Category);

            // 📄 Pagination
            query = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(ProductFilterRequest filter)
        {
            IQueryable<Product> query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                string searchPattern = $"%{filter.Search.Trim()}%";
                query = query.Where(p =>
                    EF.Functions.Like(p.Name, searchPattern) ||
                    (p.Description != null && EF.Functions.Like(p.Description, searchPattern)));
            }

            if (filter.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == filter.CategoryId.Value);
            }

            if (filter.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);
            }

            return await query.CountAsync();
        }

        // 🔧 Helper method for sorting
        private static IQueryable<Product> ApplySorting(IQueryable<Product> query, string? sortBy, string? sortDirection)
        {
            bool ascending = sortDirection?.Equals("asc", StringComparison.OrdinalIgnoreCase) ?? true;

            return sortBy?.Trim().ToLowerInvariant() switch
            {
                "name" => ascending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name),
                "price" => ascending ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price),
                "sellprice" => ascending ? query.OrderBy(p => p.SellPrice) : query.OrderByDescending(p => p.SellPrice),
                _ => ascending ? query.OrderBy(p => p.CreatedOn) : query.OrderByDescending(p => p.CreatedOn),
            };
        }
        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products.Include(p => p.Category)
                                          .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
