using AutoMapper;
using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Mapster;

namespace Entrance.Application.Feature.Management.Applicants.Queries;
public class GetFirstApplicationInfoQuery : BaseGetQuery<FirstApplicationInfoResponse>
{
    public GetFirstApplicationInfoQuery(int id) : base(id) { }
}
public class GetFirstApplicationInfoQueryHandler : BaseGetQueryHandler<GetFirstApplicationInfoQuery, FirstApplicationInfoResponse>
{
    public GetFirstApplicationInfoQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    public async override Task<ResponseWrapper<FirstApplicationInfoResponse>> Handle(GetFirstApplicationInfoQuery get, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<FirstApplicationInfo>().GetAsync(get.Id);
        if (resultInDb == null)
            return new ResponseWrapper<FirstApplicationInfoResponse>().Failed(message: "Not found.");

        return new ResponseWrapper<FirstApplicationInfoResponse>().Success(data: resultInDb.Adapt<FirstApplicationInfoResponse>());
    }
}

