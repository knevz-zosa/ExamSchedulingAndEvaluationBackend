using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.AspNetCore.Identity;

namespace Entrance.Services.Services.UserServices;
public interface IUserService
{
    Task<ResponseWrapper<int>> Create(UserRequest request);
    Task<ResponseWrapper<int>> UpdateProfile(UserProfileUpdate update);
    Task<ResponseWrapper<int>> UpdateAccess(UserAccessUpdate update);
    Task<ResponseWrapper<int>> UpdateRole(UserRoleUpdate update);
    Task<ResponseWrapper<int>> UpdateUsername(UserUsernameUpdate update);
    Task<ResponseWrapper<int>> UpdatePassword(UserPasswordUpdate update);
    Task<ResponseWrapper<int>> UpdateStatus(UserStatusUpdate update);
    Task<ResponseWrapper<int>> Delete(int id);
    Task<ResponseWrapper<PagedList<IdentityRole<int>>>> Roles(DataGridQuery query);
    Task<ResponseWrapper<UserResponse>> Get(int id);
    Task<ResponseWrapper<PagedList<UserResponse>>> List(DataGridQuery query);
}

