using Entrance.Infrastructure.Models;

namespace Entrance.Infrastructure.Token;
public interface ITokenService
{
    Task<string> GenerateToken(ApplicationUser user);
    string GenerateRefreshToken();
}
