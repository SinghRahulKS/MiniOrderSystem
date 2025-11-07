using UserService.Repository.Entity;

namespace UserService.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<CommonResponse<User>> CreateAsync(User user);
        Task<CommonResponse<User>> UpdateAsync(Guid id, User user);
        Task<CommonResponse<bool>> DeleteAsync(Guid id);

        Task<CommonResponse<Address>> AddAddressAsync(Guid userId, Address address);
        Task<IEnumerable<Address>> GetAddressesByUserIdAsync(Guid userId);
    }
}
