using Entrance.Services.Extensions;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;

namespace Entrance.Services.Services.ApplicantServices;
public class ApplicantService : IApplicantService
{
    private readonly HttpClient _httpClient;
    public ApplicantService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<ResponseWrapper<int>> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"{ApplicantEndpoints.Delete}/{id}");
        return await response.ToResponse<int>();
    }

    public async Task<ResponseWrapper<ApplicantResponse>> Get(int id)
    {
        var response = await _httpClient.GetAsync(ApplicantEndpoints.Get(id));
        return await response.ToResponse<ApplicantResponse>();
    }

    public async Task<ResponseWrapper<ApplicantApplicationResponse>> GetApplicantApplication(int id)
    {
        var response = await _httpClient.GetAsync(ApplicantEndpoints.GetApplicantApplication(id));
        return await response.ToResponse<ApplicantApplicationResponse>();
    }

    public async Task<ResponseWrapper<ApplicantPersonalInformationResponse>> GetApplicantPersonalInformation(int id)
    {
        var response = await _httpClient.GetAsync(ApplicantEndpoints.GetApplicantPersonalInformation(id));
        return await response.ToResponse<ApplicantPersonalInformationResponse>();
    }

    public async Task<ResponseWrapper<ApplicantStudentInformationResponse>> GetApplicantStudentInformation(int id)
    {
        var response = await _httpClient.GetAsync(ApplicantEndpoints.GetApplicantStudentInformation(id));
        return await response.ToResponse<ApplicantStudentInformationResponse>();
    }

    public async Task<ResponseWrapper<FirstApplicationInfoResponse>> GetFirstApplication(int id)
    {
        var response = await _httpClient.GetAsync(ApplicantEndpoints.GetFirstApplication(id));
        return await response.ToResponse<FirstApplicationInfoResponse>();
    }

    public async Task<ResponseWrapper<ApplicantLrnResponse>> GetLRN(int id)
    {
        var response = await _httpClient.GetAsync(ApplicantEndpoints.GetLRN(id));
        return await response.ToResponse<ApplicantLrnResponse>();
    }

    public async Task<ResponseWrapper<PersonalInformationResponse>> GetPersonalInformation(int id)
    {
        var response = await _httpClient.GetAsync(ApplicantEndpoints.GetPersonalInformation(id));
        return await response.ToResponse<PersonalInformationResponse>();
    }

    public async Task<ResponseWrapper<PagedList<ApplicantListResponse>>> List(DataGridQuery query, string access)
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

        var url = QueryHelpers.AddQueryString(ApplicantEndpoints.List, queryParams);

        var response = await _httpClient.GetAsync(url);
        return await response.ToResponse<PagedList<ApplicantListResponse>>();
    }

    public async Task<ResponseWrapper<PagedList<ApplicantInprogressListResponse>>> ListInProgress(DataGridQuery query, string access)
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

        var url = QueryHelpers.AddQueryString(ApplicantEndpoints.ListInProgress, queryParams);

        var response = await _httpClient.GetAsync(url);
        return await response.ToResponse<PagedList<ApplicantInprogressListResponse>>();
    }

    public async Task<ResponseWrapper<PagedList<ApplicantListScheduleResponse>>> ListScheduleApplicants(DataGridQuery query, string access, int schedId)
    {
        var queryParams = new Dictionary<string, string>();
        queryParams["schedId"] = schedId.ToString();
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

        var url = QueryHelpers.AddQueryString(ApplicantEndpoints.ListScheduleApplicants, queryParams);

        var response = await _httpClient.GetAsync(url);
        return await response.ToResponse<PagedList<ApplicantListScheduleResponse>>();
    }

    public async Task<ResponseWrapper<PagedList<ApplicantApplicationResponse>>> Passers(DataGridQuery query, string access)
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

        var url = QueryHelpers.AddQueryString(ApplicantEndpoints.Passers, queryParams);

        var response = await _httpClient.GetAsync(url);
        return await response.ToResponse<PagedList<ApplicantApplicationResponse>>();
    }

    public async Task<ResponseWrapper<int>> Transfer(ApplicantRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(ApplicantEndpoints.Transfer, request);
        return await response.ToResponse<int>();
    }

    public async Task<ResponseWrapper<int>> UpdateEmergencyContact(EmergencyContactUpdate update)
    {
        var response = await _httpClient.PutAsJsonAsync(ApplicantEndpoints.UpdateEmergencyContact, update);
        return await response.ToResponse<int>();
    }

    public async Task<ResponseWrapper<int>> UpdateGWAStatusTrack(ApplicantUpdateGwaStatusTrack update)
    {
        var response = await _httpClient.PutAsJsonAsync(ApplicantEndpoints.UpdateGwaStatusTrack, update);
        return await response.ToResponse<int>();
    }

    public async Task<ResponseWrapper<int>> UpdateLRN(ApplicantUpdateLrn update)
    {
        var response = await _httpClient.PutAsJsonAsync(ApplicantEndpoints.UpdateLRN, update);
        return await response.ToResponse<int>();
    }

    public async Task<ResponseWrapper<int>> UpdatePersonalInformation(PersonalInformationUpdate update)
    {
        var response = await _httpClient.PutAsJsonAsync(ApplicantEndpoints.UpdatePersonalInformation, update);
        return await response.ToResponse<int>();
    }

    public async Task<ResponseWrapper<int>> UpdateRegistered(ApplicantUpdateRegistered update)
    {
        var response = await _httpClient.PutAsJsonAsync(ApplicantEndpoints.UpdateRegistered, update);
        return await response.ToResponse<int>();
    }

    public async Task<ResponseWrapper<int>> UpdateStudentId(ApplicantUpdateStudentId update)
    {
        var response = await _httpClient.PutAsJsonAsync(ApplicantEndpoints.UpdateStudentId, update);
        return await response.ToResponse<int>();
    }

    public async Task<ResponseWrapper<int>> UpdateTransfer(ApplicantTransfer update)
    {
        var response = await _httpClient.PutAsJsonAsync(ApplicantEndpoints.UpdateTransfer, update);
        return await response.ToResponse<int>();
    }
}
