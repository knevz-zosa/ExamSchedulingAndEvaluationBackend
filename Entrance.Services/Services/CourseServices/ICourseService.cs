using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;

namespace Entrance.Services.Services.CourseServices;
public interface ICourseService
{
    Task<ResponseWrapper<int>> Create(CourseRequest request);
    Task<ResponseWrapper<int>> Update(CourseUpdate update);
    Task<ResponseWrapper<int>> Delete(int id);
    Task<ResponseWrapper<CourseResponse>> Get(int id);
    Task<ResponseWrapper<CourseDetailsResponse>> Details(int id);
    Task<ResponseWrapper<PagedList<CourseListResponse>>> List(DataGridQuery query, string access);
}
