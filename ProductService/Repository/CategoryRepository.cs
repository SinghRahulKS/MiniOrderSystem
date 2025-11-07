using Microsoft.EntityFrameworkCore;
using ProductService.Repository.Entity;

namespace ProductService.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ProductDbContext _context;

        public CategoryRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
            => await _context.Categories.AsNoTracking().ToListAsync();

        public async Task<Category?> GetByIdAsync(Guid id)
            => await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

        public async Task<bool> ExistsByNameAsync(string name)
            => await _context.Categories.AnyAsync(c => c.Name.ToLower() == name.ToLower());

        public async Task<bool> ExistsByIdAsync(Guid id)
            => await _context.Categories.AnyAsync(c => c.Id == id);

        public async Task<Category> AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.Categories.FindAsync(id);
            if (entity == null)
                return false;

            _context.Categories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
