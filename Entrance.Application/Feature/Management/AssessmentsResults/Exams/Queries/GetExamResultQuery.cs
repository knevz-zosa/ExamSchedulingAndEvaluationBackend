using AutoMapper;
using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Mapster;

namespace Entrance.Application.Feature.Management.AssessmentsResults.Exams.Queries;
public class GetExamResultQuery : BaseGetQuery<ExaminationResponse>
{
    public GetExamResultQuery(int id) : base(id) { }
}

public class GetExamResultQueryHandler : BaseGetQueryHandler<GetExamResultQuery, ExaminationResponse>
{
    public GetExamResultQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    public async override Task<ResponseWrapper<ExaminationResponse>> Handle(GetExamResultQuery get, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Examination>().GetAsync(get.Id);
        if (resultInDb == null)
            return new ResponseWrapper<ExaminationResponse>().Failed(message: "Result does not exists.");

        return new ResponseWrapper<ExaminationResponse>().Success(data: resultInDb.Adapt<ExaminationResponse>());
    }
}
