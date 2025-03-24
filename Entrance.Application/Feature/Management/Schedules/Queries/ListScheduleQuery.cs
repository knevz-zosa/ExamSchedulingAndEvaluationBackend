using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace Entrance.Application.Feature.Management.Schedules.Queries;
public class ListScheduleQuery : BaseListQuery<ScheduleListResponse>
{
    public string Access { get; set; }
}

public class ListScheduleQueryHandler : BaseListQueryHandler<ListScheduleQuery, ScheduleListResponse>
{
    public ListScheduleQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    public override async Task<ResponseWrapper<PagedList<ScheduleListResponse>>> Handle(ListScheduleQuery list, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.ReadRepositoryFor<Schedule>().Entities;
        var data = repository;
        if (list.Access == "All" || list.Access == string.Empty)
        {
            data = repository
             .AsNoTracking();
        }
        else
        {
            data = data
             .Where(x => x.Campus.Name == list.Access)
                .AsNoTracking();
        }

        var query = data.ProjectTo<ScheduleListResponse>(_mapper.ConfigurationProvider);


        if (!string.IsNullOrEmpty(list.GridQuery.SchoolYear))
        {
            query = query.Where(c =>
                 c.SchoolYear == list.GridQuery.SchoolYear);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var sortField = list.GridQuery.SortField ?? nameof(ScheduleListResponse.ScheduleDate);
        query = QuerySort(query, sortField, list.GridQuery.SortDir);

        var page = list.GridQuery.Page ?? 0;
        var size = list.GridQuery.PageSize ?? 20;
        var models = await query.Skip(page * size).Take(size).ToListAsync(cancellationToken);

        var pagedList = new PagedList<ScheduleListResponse>(totalCount, models);
        return new ResponseWrapper<PagedList<ScheduleListResponse>>().Success(pagedList);
    }
    IQueryable<ScheduleListResponse> QuerySort(IQueryable<ScheduleListResponse> query, string sortField, DataGridQuerySortDirection sortDirection)
    {
        switch (sortField)
        {
            case nameof(ScheduleListResponse.ScheduleDate):
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.ScheduleDate)
                    : query.OrderByDescending(c => c.ScheduleDate);
            case nameof(ScheduleListResponse.Campus.Name):
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.Campus.Name)
                    : query.OrderByDescending(c => c.Campus.Name);
            default:
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.Id)
                    : query.OrderByDescending(c => c.Id);
        }
    }
}
