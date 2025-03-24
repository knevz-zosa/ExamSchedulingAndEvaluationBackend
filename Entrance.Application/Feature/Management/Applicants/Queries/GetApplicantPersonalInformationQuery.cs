using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace Entrance.Application.Feature.Management.Applicants.Queries;
public class GetApplicantPersonalInformationQuery : BaseGetQuery<ApplicantPersonalInformationResponse>
{
    public GetApplicantPersonalInformationQuery(int id) : base(id) { }
}

public class GetApplicantPersonalInformationQueryHandler : BaseGetQueryHandler<GetApplicantPersonalInformationQuery, ApplicantPersonalInformationResponse>
{
    public GetApplicantPersonalInformationQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) 
        : base(unitOfWork, mapper) { }

    public override async Task<ResponseWrapper<ApplicantPersonalInformationResponse>> Handle(GetApplicantPersonalInformationQuery get, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Applicant>().Entities
           .AsNoTracking()
           .ProjectTo<ApplicantPersonalInformationResponse>(_mapper.ConfigurationProvider)
           .SingleOrDefaultAsync(x => x.Id == get.Id, cancellationToken);

        if (resultInDb == null)
            return new ResponseWrapper<ApplicantPersonalInformationResponse>().Failed(message: "Not found.");

        return new ResponseWrapper<ApplicantPersonalInformationResponse>().Success(resultInDb);
    }
}
