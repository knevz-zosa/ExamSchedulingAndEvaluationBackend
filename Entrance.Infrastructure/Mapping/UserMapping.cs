using AutoMapper;
using Entrance.Infrastructure.Models;
using Entrance.Shared.Models;

namespace Entrance.Infrastructure.Mapping;

public class UserMapping : Profile
{
	public UserMapping()
	{
        CreateMap<ApplicationUser, UserResponse>();
        CreateMap<ApplicationUser, UserProfileUpdate>();
        CreateMap<ApplicationUser, UserRoleUpdate>();
        CreateMap<ApplicationUser, UserAccessUpdate>();
        CreateMap<ApplicationUser, UserPasswordUpdate>();
        CreateMap<ApplicationUser, UserUsernameUpdate>();
        CreateMap<ApplicationUser, UserLoginResponse>();

        CreateMap<UserResponse, UserRoleUpdate>();
        CreateMap<UserResponse, UserProfileUpdate>();
        CreateMap<UserResponse, UserUsernameUpdate>();
        CreateMap<UserResponse, UserPasswordUpdate>();
        CreateMap<UserResponse, UserStatusUpdate>();
        CreateMap<UserResponse, UserAccessUpdate>();
    }
}
