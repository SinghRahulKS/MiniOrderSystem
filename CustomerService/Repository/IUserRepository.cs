using UserService.Repository.Entity;

namespace UserService.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task<User> AddAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Address>> GetAddressesByUserIdAsync(Guid userId);
        Task<Address> AddAddressAsync(Address address);
    }
}
