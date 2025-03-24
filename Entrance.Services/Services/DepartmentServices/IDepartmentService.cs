using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;

namespace Entrance.Services.Services.DepartmentServices;
public interface IDepartmentService
{
    Task<ResponseWrapper<int>> Create(DepartmentRequest request);
    Task<ResponseWrapper<int>> Update(DepartmentUpdate update);
    Task<ResponseWrapper<int>> Delete(int id);
    Task<ResponseWrapper<DepartmentResponse>> Get(int id);
    Task<ResponseWrapper<DepartmentDetailsResponse>> Details(int id);
    Task<ResponseWrapper<PagedList<DepartmentListResponse>>> List(DataGridQuery query);
}

