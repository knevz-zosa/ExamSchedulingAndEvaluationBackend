using AutoMapper;
using Entrance.Infrastructure.Models;
using Entrance.Infrastructure.Token;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace Entrance.Infrastructure.Features.Auths.Queries;
public class AuthQuery : IRequest<ResponseWrapper<UserResponse>>
{
    public AuthRequest Get { get; set; }
}

public class AuthQueryHandler : IRequestHandler<AuthQuery, ResponseWrapper<UserResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    public AuthQueryHandler(UserManager<ApplicationUser> userManager, ITokenService tokenService, IMapper mapper)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _mapper = mapper;
    }
    public async Task<ResponseWrapper<UserResponse>> Handle(AuthQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Get.Username);
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Get.Password))
        {
            return new ResponseWrapper<UserResponse>().Failed("Invalid credentials.");
        }

        if (!user.IsActive)
        {
            return new ResponseWrapper<UserResponse>().Failed("Your account is inactive, please contact Administrator.");
        }

        var token = await _tokenService.GenerateToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        using var sha256 = SHA256.Create();

        var refreshTokenHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(refreshToken));

        user.RefreshToken = Convert.ToBase64String(refreshTokenHash);
        user.RefreshTokenExpiry = DateTime.Now.AddDays(7);

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return new ResponseWrapper<UserResponse>().Failed($"Failed to update user: {errors}");
        }
        var roles = await _userManager.GetRolesAsync(user);
        var userResponse = _mapper.Map<UserResponse>(user);

        userResponse.Role = roles.FirstOrDefault() ?? "No Assigned Role";
        userResponse.RefreshToken = refreshToken;
        userResponse.AccessToken = token;

        return new ResponseWrapper<UserResponse>().Success(userResponse);
    }
}

