using UserService.Repository.Entity;

namespace UserService.Repository
{
    public interface IContactRepository
    {
        Task<bool> AddAsync(ContactMessage contact);
    }
}
