using AutoMapper;
using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Mapster;

namespace Entrance.Application.Feature.Management.AssessmentsResults.Interviews.Queries;
public class GetInterviewResultQuery : BaseGetQuery<InterviewResponse>
{
    public GetInterviewResultQuery(int id) : base(id) { }
}
public class GetInterviewResultQueryHandler : BaseGetQueryHandler<GetInterviewResultQuery, InterviewResponse>
{
    public GetInterviewResultQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    public override async Task<ResponseWrapper<InterviewResponse>> Handle(GetInterviewResultQuery get, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Interview>().GetAsync(get.Id);
        if (resultInDb == null)
            return new ResponseWrapper<InterviewResponse>().Failed(message: "Not found.");

        return new ResponseWrapper<InterviewResponse>().Success(data: resultInDb.Adapt<InterviewResponse>());
    }
}