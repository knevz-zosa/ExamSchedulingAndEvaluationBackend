using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace Entrance.Application.Feature.Management.Applicants.Queries;
public class GetApplicantQuery : BaseGetQuery<ApplicantResponse>
{
    public GetApplicantQuery(int id) : base(id) { }
}
public class GetApplicantQueryHandler : BaseGetQueryHandler<GetApplicantQuery, ApplicantResponse>
{
    public GetApplicantQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    public override async Task<ResponseWrapper<ApplicantResponse>> Handle(GetApplicantQuery get, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Applicant>().Entities
           .AsNoTracking()
           .ProjectTo<ApplicantResponse>(_mapper.ConfigurationProvider)
           .SingleOrDefaultAsync(x => x.Id == get.Id, cancellationToken);

        if (resultInDb == null)
            return new ResponseWrapper<ApplicantResponse>().Failed(message: "Not found.");

        return new ResponseWrapper<ApplicantResponse>().Success(resultInDb);
    }
}
