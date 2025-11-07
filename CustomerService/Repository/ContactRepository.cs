using UserService.Data;
using UserService.Repository.Entity;

namespace UserService.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly UserDbContext _context;
        public ContactRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(ContactMessage contact)
        {
            if (contact == null) return false;

            await _context.Contacts.AddAsync(contact); // Add to DbSet
            var saved = await _context.SaveChangesAsync(); // Save changes

            // Check if record was saved
            return saved > 0;
        }
    }
}
