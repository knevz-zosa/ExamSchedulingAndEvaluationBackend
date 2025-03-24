using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace Entrance.Application.Feature.Management.Applicants.Queries;
public class GetApplicantApplicationQuery : BaseGetQuery<ApplicantApplicationResponse>
{
    public GetApplicantApplicationQuery(int id) : base(id) { }
}

public class GetApplicantApplicationQueryHandler : BaseGetQueryHandler<GetApplicantApplicationQuery, ApplicantApplicationResponse>
{
    public GetApplicantApplicationQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) 
        : base(unitOfWork, mapper) { }

    public override async Task<ResponseWrapper<ApplicantApplicationResponse>> Handle(GetApplicantApplicationQuery get, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Applicant>().Entities
           .AsNoTracking()
           .ProjectTo<ApplicantApplicationResponse>(_mapper.ConfigurationProvider)
           .SingleOrDefaultAsync(x => x.Id == get.Id, cancellationToken);

        if (resultInDb == null)
            return new ResponseWrapper<ApplicantApplicationResponse>().Failed(message: "Not found.");

        return new ResponseWrapper<ApplicantApplicationResponse>().Success(resultInDb);
    }
}
