using UserService.Repository;
using UserService.Repository.Entity;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            // Fetch all users from repository
            return await _userRepository.GetAllAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            // Fetch single user by ID
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<CommonResponse<User>> CreateAsync(User user)
        {
            // Assign new GUID if not already set
            user.Id = user.Id == Guid.Empty ? Guid.NewGuid() : user.Id;
            //user.CreatedAt = DateTime.UtcNow;

            await _userRepository.AddAsync(user);

            return CommonResponse<User>.Ok(user, "User created successfully");
        }

        public async Task<CommonResponse<User>> UpdateAsync(Guid id, User user)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
                return CommonResponse<User>.Fail("User not found");

            // Map updated properties
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.PhoneNo = user.PhoneNo;
            //existingUser.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(existingUser);

            return CommonResponse<User>.Ok(existingUser, "User updated successfully");
        }

        public async Task<CommonResponse<bool>> DeleteAsync(Guid id)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
                return CommonResponse<bool>.Fail("User not found");

            await _userRepository.DeleteAsync(existingUser.Id);

            return CommonResponse<bool>.Ok(true, "User deleted successfully");
        }

        public async Task<CommonResponse<Address>> AddAddressAsync(Guid userId, Address address)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return CommonResponse<Address>.Fail("User not found");

            address.Id = Guid.NewGuid();
            address.UserId = userId;
            //address.CreatedAt = DateTime.UtcNow;

            await _userRepository.AddAddressAsync(address);

            return CommonResponse<Address>.Ok(address, "Address added successfully");
        }

        public async Task<IEnumerable<Address>> GetAddressesByUserIdAsync(Guid userId)
        {
            return await _userRepository.GetAddressesByUserIdAsync(userId);
        }
    }

}
