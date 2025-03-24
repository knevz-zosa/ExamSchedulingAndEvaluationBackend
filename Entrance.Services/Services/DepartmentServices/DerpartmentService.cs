using Entrance.Services.Extensions;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;

namespace Entrance.Services.Services.DepartmentServices;
public class DepartmentService : IDepartmentService
{
    private readonly HttpClient _httpClient;
    public DepartmentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ResponseWrapper<int>> Create(DepartmentRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(DepartmentEndpoints.Create, request);
        return await response.ToResponse<int>();
    }

    public async Task<ResponseWrapper<int>> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"{DepartmentEndpoints.Delete}/{id}");
        return await response.ToResponse<int>();
    }

    public async Task<ResponseWrapper<DepartmentDetailsResponse>> Details(int id)
    {
        var response = await _httpClient.GetAsync(DepartmentEndpoints.Details(id));
        return await response.ToResponse<DepartmentDetailsResponse>();
    }

    public async Task<ResponseWrapper<DepartmentResponse>> Get(int id)
    {
        var response = await _httpClient.GetAsync(DepartmentEndpoints.Get(id));
        return await response.ToResponse<DepartmentResponse>();
    }

    public async Task<ResponseWrapper<PagedList<DepartmentListResponse>>> List(DataGridQuery query)
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

        var url = QueryHelpers.AddQueryString(DepartmentEndpoints.List, queryParams);

        var response = await _httpClient.GetAsync(url);
        return await response.ToResponse<PagedList<DepartmentListResponse>>();
    }

    public async Task<ResponseWrapper<int>> Update(DepartmentUpdate update)
    {
        var response = await _httpClient.PutAsJsonAsync(DepartmentEndpoints.Update, update);
        return await response.ToResponse<int>();
    }
}
