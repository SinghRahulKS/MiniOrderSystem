using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Model.Request;
using UserService.Repository.Entity;
using UserService.Services;

namespace UserService.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // ✅ 1. Get all users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        // ✅ 2. Get user by Id
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound(new { Message = "User not found" });

            return Ok(user);
        }

        // ✅ 3. Update user details (profile)
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest request)
        {
            var user = _mapper.Map<UpdateUserRequest, User>(request);
            var result = await _userService.UpdateAsync(id, user);
            if (!result.IsValid)
                return BadRequest(result);

            return Ok(result);
        }

        // ✅ 4. Delete a user
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _userService.DeleteAsync(id);
            if (!result.IsValid)
                return BadRequest(result);

            return Ok(result);
        }

        // ✅ 5. Add address for a user
        [HttpPost("{id:guid}/addresses")]
        public async Task<IActionResult> AddAddress(Guid id, [FromBody] AddressRequestModel request)
        {
            var address = _mapper.Map<AddressRequestModel, Address>(request);
            var result = await _userService.AddAddressAsync(id, address);
            if (!result.IsValid)
                return BadRequest(result);

            return Ok(result);
        }

        // ✅ 6. Get all addresses for a user
        [HttpGet("{id:guid}/addresses")]
        public async Task<IActionResult> GetAddresses(Guid id)
        {
            var result = await _userService.GetAddressesByUserIdAsync(id);
            return Ok(result);
        }
    }
}
