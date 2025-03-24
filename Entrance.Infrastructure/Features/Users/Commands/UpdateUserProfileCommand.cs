using Entrance.Infrastructure.Models;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Entrance.Infrastructure.Features.Users.Commands;
//UPDATE USER PROFILE
public class UpdateUserProfileCommand : IRequest<ResponseWrapper<int>>
{
    public UserProfileUpdate User { get; set; }
}

public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, ResponseWrapper<int>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    public UpdateUserProfileCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<ResponseWrapper<int>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.User.Id.ToString());
        if (user == null)
        {
            return new ResponseWrapper<int>().Failed("User not found.");
        }

        var existingUser = await _userManager.Users
           .Where(u => u.FirstName == request.User.FirstName && u.LastName == request.User.LastName)
           .Where(u => u.Id != user.Id)
           .FirstOrDefaultAsync(cancellationToken);

        if (existingUser != null)
        {
            return new ResponseWrapper<int>().Failed("A user with the same full name already exists.");
        }

        user.FirstName = request.User.FirstName;
        user.LastName = request.User.LastName;

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return new ResponseWrapper<int>().Success(user.Id, "Profile updated successfully.");
        }

        return new ResponseWrapper<int>().Failed("Failed to update profile: " + string.Join(", ", result.Errors.Select(e => e.Description)));
    }
}

//UPDATE USER USERNAME
public class UpdateUserUsernameCommand : IRequest<ResponseWrapper<int>>
{
    public UserUsernameUpdate User { get; set; }
}

public class UpdateUserUsernameCommandHandler : IRequestHandler<UpdateUserUsernameCommand, ResponseWrapper<int>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    public UpdateUserUsernameCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<ResponseWrapper<int>> Handle(UpdateUserUsernameCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.User.Id.ToString());
        if (user == null)
        {
            return new ResponseWrapper<int>().Failed("User not found.");
        }

        user.UserName = request.User.Username;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            var erroMessage = string.Join(", ", result.Errors.Select(e => e.Description));
            return new ResponseWrapper<int>().Failed(message: $"Failed to update username: {erroMessage}");
        }
        return new ResponseWrapper<int>().Success(user.Id, "Username updated successfully.");
    }
}

//UPDATE USER PASSWORD
public class UpdateUserPasswordCommand : IRequest<ResponseWrapper<int>>
{
    public UserPasswordUpdate User { get; set; }
}

public class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand, ResponseWrapper<int>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    public UpdateUserPasswordCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<ResponseWrapper<int>> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.User.Id.ToString());
        if (user == null)
        {
            return new ResponseWrapper<int>().Failed("User not found.");
        }

        var result = await _userManager.ChangePasswordAsync(user, request.User.OldPassword, request.User.Password);
        if (!result.Succeeded)
        {
            var erroMessage = string.Join(", ", result.Errors.Select(e => e.Description));
            return new ResponseWrapper<int>().Failed(message: $"Failed to update password: {erroMessage}");
        }
        return new ResponseWrapper<int>().Success(user.Id, "Password updated successfully.");
    }
}

//UPDATE USER ROLE
public class UpdateUserRoleCommand : IRequest<ResponseWrapper<int>>
{
    public UserRoleUpdate User { get; set; }
}

public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, ResponseWrapper<int>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    public UpdateUserRoleCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task<ResponseWrapper<int>> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.User.Id.ToString());
        if (user == null)
        {
            return new ResponseWrapper<int>().Failed("User not found.");
        }

        var currentRoles = await _userManager.GetRolesAsync(user);

        // Check if the requested role exists
        var roleExists = await _roleManager.RoleExistsAsync(request.User.Role);
        if (!roleExists)
        {
            return new ResponseWrapper<int>().Failed($"Role '{request.User.Role}' does not exist.");
        }

        // Only remove roles if the user currently has any
        if (currentRoles.Any()) // checks if there are any roles to remove
        {
            var result = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!result.Succeeded)
            {
                return new ResponseWrapper<int>().Failed("Failed to remove current roles.");
            }
        }

        // Add the new role
        var addRoleResult = await _userManager.AddToRoleAsync(user, request.User.Role);
        if (!addRoleResult.Succeeded)
        {
            var errorMessage = string.Join(", ", addRoleResult.Errors.Select(e => e.Description));
            return new ResponseWrapper<int>().Failed($"Failed to update role: {errorMessage}");
        }

        return new ResponseWrapper<int>().Success(user.Id, "Role updated successfully.");
    }
}

//UPDATE USER ACCESS
public class UpdateUserAccessCommand : IRequest<ResponseWrapper<int>>
{
    public UserAccessUpdate User { get; set; }
}

public class UpdateUserAccessCommandHandler : IRequestHandler<UpdateUserAccessCommand, ResponseWrapper<int>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    public UpdateUserAccessCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<ResponseWrapper<int>> Handle(UpdateUserAccessCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.User.Id.ToString());
        if (user == null)
        {
            return new ResponseWrapper<int>().Failed("User not found.");
        }

        user.Access = request.User.Access;

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return new ResponseWrapper<int>().Success(user.Id, "Access updated successfully.");
        }

        return new ResponseWrapper<int>().Failed("Failed to update access: " + string.Join(", ", result.Errors.Select(e => e.Description)));
    }
}

//UPDATE USER STATUS
public class UpdateUserStatusCommand : IRequest<ResponseWrapper<int>>
{
    public UserStatusUpdate User { get; set; }
}

public class UpdateUserStatusCommandHandler : IRequestHandler<UpdateUserStatusCommand, ResponseWrapper<int>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    public UpdateUserStatusCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<ResponseWrapper<int>> Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.User.Id.ToString());
        if (user == null)
        {
            return new ResponseWrapper<int>().Failed("User not found.");
        }

        user.IsActive = request.User.IsActive;

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return new ResponseWrapper<int>().Success(user.Id, "Status updated successfully.");
        }

        return new ResponseWrapper<int>().Failed("Failed to update status: " + string.Join(", ", result.Errors.Select(e => e.Description)));
    }
}

