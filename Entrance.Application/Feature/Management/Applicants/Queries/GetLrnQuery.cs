using AutoMapper;
using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Mapster;

namespace Entrance.Application.Feature.Management.Applicants.Queries;
public class GetLrnQuery : BaseGetQuery<ApplicantLrnResponse>
{
    public GetLrnQuery(int id) : base(id) { }
}
public class GetLrnQueryHandler : BaseGetQueryHandler<GetLrnQuery, ApplicantLrnResponse>
{
    public GetLrnQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    public override async Task<ResponseWrapper<ApplicantLrnResponse>> Handle(GetLrnQuery get, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Applicant>().GetAsync(get.Id);
        if (resultInDb == null)
            return new ResponseWrapper<ApplicantLrnResponse>().Failed(message: "Not found.");

        return new ResponseWrapper<ApplicantLrnResponse>().Success(data: resultInDb.Adapt<ApplicantLrnResponse>());
    }
}