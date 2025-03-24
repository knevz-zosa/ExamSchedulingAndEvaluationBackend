using Entrance.Services.Extensions;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;

namespace Entrance.Services.Services.ScheduleServices;
public class ScheduleService : IScheduleService
{
    private readonly HttpClient _httpClient;
    public ScheduleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ResponseWrapper<int>> Create(ScheduleRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(ScheduleEndpoints.Create, request);
        return await response.ToResponse<int>();
    }

    public async Task<ResponseWrapper<int>> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"{ScheduleEndpoints.Delete}/{id}");
        return await response.ToResponse<int>();
    }

    public async Task<ResponseWrapper<ScheduleDetailsResponse>> Details(int id)
    {
        var response = await _httpClient.GetAsync(ScheduleEndpoints.Details(id));
        return await response.ToResponse<ScheduleDetailsResponse>();
    }

    public async Task<ResponseWrapper<ScheduleResponse>> Get(int id)
    {
        var response = await _httpClient.GetAsync(ScheduleEndpoints.Get(id));
        return await response.ToResponse<ScheduleResponse>();
    }

    public async Task<ResponseWrapper<PagedList<ScheduleListResponse>>> List(DataGridQuery query, string access)
    {
        var queryParams = new Dictionary<string, string>();

        if (!string.IsNullOrWhiteSpace(access))
            queryParams["access"] = access;

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

        var url = QueryHelpers.AddQueryString(ScheduleEndpoints.List, queryParams);

        var response = await _httpClient.GetAsync(url);
        return await response.ToResponse<PagedList<ScheduleListResponse>>();
    }

    public async Task<ResponseWrapper<List<string>>> SchoolYears()
    {
        var response = await _httpClient.GetAsync(ScheduleEndpoints.SchoolYears);
        return await response.ToResponse<List<string>>();
    }
}

