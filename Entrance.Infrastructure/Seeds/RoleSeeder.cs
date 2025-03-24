using Microsoft.AspNetCore.Identity;

namespace Entrance.Infrastructure.Seeds;
public class RoleSeeder
{
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public RoleSeeder(RoleManager<IdentityRole<int>> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task SeedRolesAsync()
    {
        // List of roles to seed
        var roles = new List<string> { "Administrator", "Manager", "Registrar" };

        foreach (var role in roles)
        {
            var roleExist = await _roleManager.RoleExistsAsync(role);
            if (!roleExist)
            {
                // Create the role if it doesn't exist
                await _roleManager.CreateAsync(new IdentityRole<int>(role));
            }
        }
    }
}
