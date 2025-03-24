using AutoMapper;
using Entrance.Infrastructure.Models;
using Entrance.Infrastructure.Token;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Entrance.Infrastructure.Features.Auths.Queries;
public class RefreshTokenQuery : IRequest<ResponseWrapper<UserResponse>>
{
    public RefreshTokenRequest Get { get; set; }
}

public class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, ResponseWrapper<UserResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    public RefreshTokenQueryHandler(UserManager<ApplicationUser> userManager, ITokenService tokenService, IMapper mapper)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    public async Task<ResponseWrapper<UserResponse>> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
    {
        using var sha256 = SHA256.Create();
        var refreshTokenHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(request.Get.RefreshToken));
        var hashedRefreshToken = Convert.ToBase64String(refreshTokenHash);

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == hashedRefreshToken);
        if (user == null)
        {
            return new ResponseWrapper<UserResponse>().Failed("Invalid refresh token");
        }

        if (user.RefreshTokenExpiry < DateTime.Now)
        {
            return new ResponseWrapper<UserResponse>().Failed("Refresh token expired");
        }

        var newAccessToken = await _tokenService.GenerateToken(user);

        var currentUserResponse = _mapper.Map<UserResponse>(user);
        currentUserResponse.AccessToken = newAccessToken;

        currentUserResponse.RefreshToken = request.Get.RefreshToken;

        return new ResponseWrapper<UserResponse>().Success(currentUserResponse);
    }
}
