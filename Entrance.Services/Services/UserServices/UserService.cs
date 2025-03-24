using Entrance.Services.Extensions;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Entrance.Services.Services.UserServices;
public class UserService : IUserService
{

    private readonly HttpClient _httpClient;
    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ResponseWrapper<int>> Create(UserRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(UserEndpoints.Create, request);
        return await response.ToResponse<int>();
    }

    public async Task<ResponseWrapper<int>> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"{UserEndpoints.Delete}/{id}");
        return await response.ToResponse<int>();
    }

    public async Task<ResponseWrapper<UserResponse>> Get(int id)
    {
        var response = await _httpClient.GetAsync(UserEndpoints.Get(id));
        return await response.ToResponse<UserResponse>();
    }

    public async Task<ResponseWrapper<int>> UpdateAccess(UserAccessUpdate update)
    {
        var response = await _httpClient.PutAsJsonAsync(UserEndpoints.UpdateAccess, update);
        return await response.ToResponse<int>();
    }

    public async Task<ResponseWrapper<int>> UpdatePassword(UserPasswordUpdate update)
    {
        var response = await _httpClient.PutAsJsonAsync(UserEndpoints.UpdatePassword, update);
        return await response.ToResponse<int>();
    }

    public async Task<ResponseWrapper<int>> UpdateProfile(UserProfileUpdate update)
    {
        var response = await _httpClient.PutAsJsonAsync(UserEndpoints.UpdateProfile, update);
        return await response.ToResponse<int>();
    }

    public async Task<ResponseWrapper<int>> UpdateRole(UserRoleUpdate update)
    {
        var response = await _httpClient.PutAsJsonAsync(UserEndpoints.UpdateRole, update);
        return await response.ToResponse<int>();
    }

    public async Task<ResponseWrapper<int>> UpdateUsername(UserUsernameUpdate update)
    {
        var response = await _httpClient.PutAsJsonAsync(UserEndpoints.UpdateUsername, update);
        return await response.ToResponse<int>();
    }
    public async Task<ResponseWrapper<int>> UpdateStatus(UserStatusUpdate update)
    {
        var response = await _httpClient.PutAsJsonAsync(UserEndpoints.UpdateIsActive, update);
        return await response.ToResponse<int>();
    }

    public async Task<ResponseWrapper<PagedList<IdentityRole<int>>>> Roles(DataGridQuery query)
    {
        var response = await _httpClient.GetAsync(UserEndpoints.Roles(query));
        return await response.ToResponse<PagedList<IdentityRole<int>>>();
    }

    public async Task<ResponseWrapper<PagedList<UserResponse>>> List(DataGridQuery query)
    {
        var queryParams = new Dictionary<string, string>();

        if (!string.IsNullOrWhiteSpace(query.Search))
            queryParams["search"] = query.Search;

        if (!string.IsNullOrWhiteSpace(query.SchoolYear))
            queryParams["schoolYear"] = query.SchoolYear;

        if (query.Page.HasValue)
            queryParams["page"] = query.Page.Value.ToString();

        if (query.PageSize.HasValue)
            queryParams["pageSize"] = query.PageSize.Value.ToString();

        if (!string.IsNullOrWhiteSpace(query.SortField))
            queryParams["sortField"] = query.SortField;

        queryParams["sortDir"] = query.SortDir.ToString().ToLower();

        var url = QueryHelpers.AddQueryString(UserEndpoints.List, queryParams);

        var response = await _httpClient.GetAsync(url);
        return await response.ToResponse<PagedList<UserResponse>>();
    }
}
