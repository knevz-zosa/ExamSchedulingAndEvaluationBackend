using Entrance.Services.Extensions;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;

namespace Entrance.Services.Services.InterviewResultServices;
public class InterviewResultService : IInterviewResultService
{
    private readonly HttpClient _httpClient;
    public InterviewResultService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ResponseWrapper<int>> Activate(InterviewActiveUpdate update)
    {
        var response = await _httpClient.PutAsJsonAsync(InterviewEndpoints.IsActive, update);
        return await response.ToResponse<int>();
    }

    public async Task<ResponseWrapper<int>> Create(InterviewResultRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(InterviewEndpoints.Create, request);
        return await response.ToResponse<int>();
    }

    public async Task<ResponseWrapper<InterviewResponse>> Get(int id)
    {
        var response = await _httpClient.GetAsync(InterviewEndpoints.Get(id));
        return await response.ToResponse<InterviewResponse>();
    }

    public async Task<ResponseWrapper<PagedList<InterviewResponse>>> List(DataGridQuery query)
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

        var url = QueryHelpers.AddQueryString(InterviewEndpoints.List, queryParams);

        var response = await _httpClient.GetAsync(url);
        return await response.ToResponse<PagedList<InterviewResponse>>();
    }

    public async Task<ResponseWrapper<int>> UpdateRating(InterviewResultUpdate update)
    {
        var response = await _httpClient.PutAsJsonAsync(InterviewEndpoints.Update, update);
        return await response.ToResponse<int>();
    }
}
