using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace Entrance.Application.Feature.Management.Campuses.Queries;
public class ListCampusQuery : BaseListQuery<CampusListResponse> { }

public class ListCampusQueryHandler : BaseListQueryHandler<ListCampusQuery, CampusListResponse>
{
    public ListCampusQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
    : base(unitOfWork, mapper) { }

    public override async Task<ResponseWrapper<PagedList<CampusListResponse>>> Handle(ListCampusQuery list, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.ReadRepositoryFor<Campus>().Entities
            .AsNoTracking()
            .ProjectTo<CampusListResponse>(_mapper.ConfigurationProvider);

        var query = repository;

        if (!string.IsNullOrEmpty(list.GridQuery.Search))
        {
            query = query.Where(u => u.Name.Contains(list.GridQuery.Search));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var sortField = list.GridQuery.SortField ?? nameof(CampusListResponse.Name);
        query = QuerySort(query, sortField, list.GridQuery.SortDir);

        var page = list.GridQuery.Page ?? 0;
        var size = list.GridQuery.PageSize ?? 20;
        var models = await query.Skip(page * size).Take(size).ToListAsync(cancellationToken);

        var pagedList = new PagedList<CampusListResponse>(totalCount, models);
        return new ResponseWrapper<PagedList<CampusListResponse>>().Success(pagedList);
    }

    IQueryable<CampusListResponse> QuerySort(IQueryable<CampusListResponse> query, string sortField, DataGridQuerySortDirection sortDirection)
    {
        switch (sortField)
        {
            case nameof(CampusListResponse.Name):
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.Name)
                    : query.OrderByDescending(c => c.Name);
            default:
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.Id)
                    : query.OrderByDescending(c => c.Id);
        }
    }
}
