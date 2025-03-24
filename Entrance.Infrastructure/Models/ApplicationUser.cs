using Microsoft.AspNetCore.Identity;

namespace Entrance.Infrastructure.Models;
public class ApplicationUser : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Access { get; set; }
    public bool IsActive { get; set; } = false;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
}
 