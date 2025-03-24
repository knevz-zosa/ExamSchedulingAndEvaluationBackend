using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace Entrance.Application.Feature.Management.Applicants.Queries;
public class GetApplicantStudentInformationQuery : BaseGetQuery<ApplicantStudentInformationResponse>
{
    public GetApplicantStudentInformationQuery(int id) : base(id) { }
}

public class GetApplicantStudentInformationQueryHandler : BaseGetQueryHandler<GetApplicantStudentInformationQuery, ApplicantStudentInformationResponse>
{
    public GetApplicantStudentInformationQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) 
        : base(unitOfWork, mapper) { }

    public override async Task<ResponseWrapper<ApplicantStudentInformationResponse>> Handle(GetApplicantStudentInformationQuery get, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Applicant>().Entities
           .AsNoTracking()
           .ProjectTo<ApplicantStudentInformationResponse>(_mapper.ConfigurationProvider)
           .SingleOrDefaultAsync(x => x.Id == get.Id, cancellationToken);

        if (resultInDb == null)
            return new ResponseWrapper<ApplicantStudentInformationResponse>().Failed(message: "Not found.");

        return new ResponseWrapper<ApplicantStudentInformationResponse>().Success(resultInDb);
    }
}
