using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;

namespace Entrance.Services.Services.ExamResultServices;
public interface IExamResultService
{
    Task<ResponseWrapper<int>> Create(ExaminationResultRequest request);
    Task<ResponseWrapper<int>> Update(ExaminationResultUpdate update);
    Task<ResponseWrapper<ExaminationResponse>> Get(int id);
}
