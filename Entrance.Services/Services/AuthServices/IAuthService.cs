using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;

namespace Entrance.Services.Services.AuthServices;
public interface IAuthService
{
    Task<ResponseWrapper<UserResponse>> Login(AuthRequest request);
    Task<ResponseWrapper<UserResponse>> RefreshToken(RefreshTokenRequest request);
    Task<ResponseWrapper<string>> RemoveRefreshToken(RefreshTokenRequest request);
    Task<ResponseWrapper<UserResponse>> GetCurrentUser();
}
