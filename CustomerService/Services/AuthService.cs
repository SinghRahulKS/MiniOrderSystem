using Google.Apis.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Model.Request;
using UserService.Repository;
using UserService.Repository.Entity;

namespace UserService.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepo;

        public AuthService(IConfiguration config, IUserRepository userRepo)
        {
            _config = config;
            _userRepo = userRepo;
        }

        public async Task<CommonResponse<AuthResponse>> RegisterAsync(RegisterRequestModel req)
        {
            if (await _userRepo.GetByEmailAsync(req.Email) != null)
                return CommonResponse<AuthResponse>.Fail("Email already exists");

            var newUser = new User
            {
                Name = req.Name,
                Email = req.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password),
                PhoneNo = req.PhoneNo,
                CreatedBy = "system",
                
            };

            await _userRepo.AddAsync(newUser);
            var token = GenerateJwtToken(newUser);
            var response = new AuthResponse
            {
                Token = token,
                UserId = newUser!.Id,
                Email = newUser.Email,
                Name = newUser.Name
            };

            return CommonResponse<AuthResponse>.Ok(response);
        }
        public async Task<CommonResponse<AuthResponse>> LoginAsync(LoginRequestModel req)
        {
            User? user = null;
            if (req.IsGoogleLogin)
            {
                try
                {
                    var payload = await GoogleJsonWebSignature.ValidateAsync(req.GoogleToken!);

                    user = await _userRepo.GetByEmailAsync(payload.Email);

                    if (user == null)
                    {
                        user = new User
                        {
                            Email = payload.Email,
                            Name = payload.Name ?? payload.Email.Split('@')[0],
                            PhoneNo = "0000000000",
                            Role = "Customer", // default
                            PasswordHash = "" // Google users won't have password
                        };
                        await _userRepo.AddAsync(user);
                    }
                }
                catch (Exception ex)
                {
                    return CommonResponse<AuthResponse>.Fail("Invalid Google token: " + ex.Message);
                }
            }
            else
            {
                user = await _userRepo.GetByEmailAsync(req.Email!);
                if (user == null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
                    return CommonResponse<AuthResponse>.Fail("Invalid credentials");
            }

            var token = GenerateJwtToken(user!);
            var response = new AuthResponse
            {
                Token = token,
                UserId = user!.Id,
                Email = user.Email,
                Name = user.Name
            };

            return CommonResponse<AuthResponse>.Ok(response);
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role ?? "Customer")
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpiryMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
