using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace Entrance.Application.Feature.Management.Schedules.Queries;
public class DetailsScheduleQuery : BaseDetailsQuery<ScheduleDetailsResponse>
{
    public DetailsScheduleQuery(int id) : base(id) { }
}

public class DetailsScheduleQueryHandler : BaseDetailsQueryHandler<DetailsScheduleQuery, ScheduleDetailsResponse>
{
    public DetailsScheduleQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) 
        : base(unitOfWork, mapper) { }

    public override async Task<ResponseWrapper<ScheduleDetailsResponse>> Handle(DetailsScheduleQuery get, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Schedule>().Entities
            .AsNoTracking()
            .ProjectTo<ScheduleDetailsResponse>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(x => x.Id == get.Id, cancellationToken);

        if (resultInDb == null)
        {
            return new ResponseWrapper<ScheduleDetailsResponse>().Failed(message: "Not found.");
        }

        return new ResponseWrapper<ScheduleDetailsResponse>().Success(resultInDb);
    }
}
