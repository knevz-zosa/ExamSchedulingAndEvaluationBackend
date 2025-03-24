using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;

namespace Entrance.Services.Services.ScheduleServices;

public interface IScheduleService
{
    Task<ResponseWrapper<int>> Create(ScheduleRequest request);
    Task<ResponseWrapper<int>> Delete(int id);
    Task<ResponseWrapper<ScheduleResponse>> Get(int id);
    Task<ResponseWrapper<ScheduleDetailsResponse>> Details(int id);
    Task<ResponseWrapper<PagedList<ScheduleListResponse>>> List(DataGridQuery query, string access);
    Task<ResponseWrapper<List<string>>> SchoolYears();
}
