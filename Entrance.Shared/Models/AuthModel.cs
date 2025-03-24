using System.ComponentModel.DataAnnotations;

namespace Entrance.Shared.Models;
public class AuthRequest
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}
public class RefreshTokenRequest
{
    public string RefreshToken { get; set; }
}