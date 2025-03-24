using Entrance.Infrastructure.Models;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Entrance.Infrastructure.Features.Users.Queries;
public class GetUserQuery : IRequest<ResponseWrapper<UserResponse>>
{
    public int Id { get; set; }
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ResponseWrapper<UserResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    public GetUserQueryHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<ResponseWrapper<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());
        if (user == null)
        {
            return new ResponseWrapper<UserResponse>().Failed("User not found.");
        }

        var response = new UserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName ?? string.Empty,
            Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? "No Role",
            Access = user.Access ?? "No Access",
            IsActive = user.IsActive
        };

        return new ResponseWrapper<UserResponse>().Success(response);
    }
}
