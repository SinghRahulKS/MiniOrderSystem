using UserService.Model.Request;
using UserService.Repository.Entity;

namespace UserService.Services
{
    public interface IAuthService
    {
        Task<CommonResponse<AuthResponse>> RegisterAsync(RegisterRequestModel request);
        Task<CommonResponse<AuthResponse>> LoginAsync(LoginRequestModel request);
    }
}
