using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;

namespace Entrance.Services.Services.InterviewResultServices;
public interface IInterviewResultService
{
    Task<ResponseWrapper<int>> Create(InterviewResultRequest request);
    Task<ResponseWrapper<int>> UpdateRating(InterviewResultUpdate update);
    Task<ResponseWrapper<int>> Activate(InterviewActiveUpdate update);
    Task<ResponseWrapper<InterviewResponse>> Get(int id);
    Task<ResponseWrapper<PagedList<InterviewResponse>>> List(DataGridQuery query);
}
