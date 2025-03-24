using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;

namespace Entrance.Services.Services.CampusServices;
public interface ICampusService
{
    Task<ResponseWrapper<int>> Create(CampusRequest request);
    Task<ResponseWrapper<int>> Update(CampusUpdate update);
    Task<ResponseWrapper<int>> Delete(int id);
    Task<ResponseWrapper<CampusResponse>> Get(int id);
    Task<ResponseWrapper<CampusDetailsResponse>> Details(int id);
    Task<ResponseWrapper<PagedList<CampusListResponse>>> List(DataGridQuery query);
}
