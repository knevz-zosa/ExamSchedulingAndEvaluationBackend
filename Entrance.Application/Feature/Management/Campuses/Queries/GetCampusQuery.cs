using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace Entrance.Application.Feature.Management.Campuses.Queries;
public class GetCampusQuery : BaseGetQuery<CampusResponse>
{
    public GetCampusQuery(int id) : base(id) { }
}

public class GetCampusQueryHandler : BaseGetQueryHandler<GetCampusQuery, CampusResponse>
{
    public GetCampusQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
    : base(unitOfWork, mapper) { }

    public override async Task<ResponseWrapper<CampusResponse>> Handle(GetCampusQuery get, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Campus>().Entities
            .AsNoTracking()
            .ProjectTo<CampusResponse>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(x => x.Id == get.Id, cancellationToken);

        if (resultInDb == null)
            return new ResponseWrapper<CampusResponse>().Failed(message: "Not found.");

        return new ResponseWrapper<CampusResponse>().Success(resultInDb);
    }
}
