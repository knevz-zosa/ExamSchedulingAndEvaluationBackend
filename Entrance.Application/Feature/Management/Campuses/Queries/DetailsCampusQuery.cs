using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace Entrance.Application.Feature.Management.Campuses.Queries;
public class DetailsCampusQuery : BaseDetailsQuery<CampusDetailsResponse>
{
    public DetailsCampusQuery(int id) : base(id) { }
}

public class DetailsCampusQuertHandler : BaseDetailsQueryHandler<DetailsCampusQuery, CampusDetailsResponse>
{
    public DetailsCampusQuertHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
    : base(unitOfWork, mapper) { }

    public async override Task<ResponseWrapper<CampusDetailsResponse>> Handle(DetailsCampusQuery get, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Campus>().Entities
             .AsNoTracking()
             .ProjectTo<CampusDetailsResponse>(_mapper.ConfigurationProvider)
             .SingleOrDefaultAsync(x => x.Id == get.Id, cancellationToken);

        if (resultInDb == null)
            return new ResponseWrapper<CampusDetailsResponse>().Failed(message: "Not found.");

        return new ResponseWrapper<CampusDetailsResponse>().Success(resultInDb);
    }
}
