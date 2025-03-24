using Entrance.Services.Extensions;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;

namespace Entrance.Services.Services.CampusServices;
public class CampusService : ICampusService
{
    private readonly HttpClient _httpClient;
    public CampusService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ResponseWrapper<int>> Create(CampusRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(CampusEndpoints.Create, request);
        return await response.ToResponse<int>();
    }

    public async Task<ResponseWrapper<int>> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"{CampusEndpoints.Delete}/{id}");
        return await response.ToResponse<int>();
    }

    public async Task<ResponseWrapper<CampusDetailsResponse>> Details(int id)
    {
        var response = await _httpClient.GetAsync(CampusEndpoints.Details(id));
        return await response.ToResponse<CampusDetailsResponse>();
    }

    public async Task<ResponseWrapper<CampusResponse>> Get(int id)
    {
        var response = await _httpClient.GetAsync(CampusEndpoints.Get(id));
        return await response.ToResponse<CampusResponse>();
    }

    public async Task<ResponseWrapper<PagedList<CampusListResponse>>> List(DataGridQuery query)
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

        var url = QueryHelpers.AddQueryString(CampusEndpoints.List, queryParams);

        var response = await _httpClient.GetAsync(url);
        return await response.ToResponse<PagedList<CampusListResponse>>();
    }

    public async Task<ResponseWrapper<int>> Update(CampusUpdate update)
    {
        var response = await _httpClient.PutAsJsonAsync(CampusEndpoints.Update, update);
        return await response.ToResponse<int>();
    }
}

