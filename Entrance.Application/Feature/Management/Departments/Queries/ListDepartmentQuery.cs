using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace Entrance.Application.Feature.Management.Departments.Queries;
public class ListDepartmentQuery : BaseListQuery<DepartmentListResponse> { }

public class ListDepartmentQueryHandler : BaseListQueryHandler<ListDepartmentQuery, DepartmentListResponse>
{
    public ListDepartmentQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper) { }
    public override async Task<ResponseWrapper<PagedList<DepartmentListResponse>>> Handle(ListDepartmentQuery list, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.ReadRepositoryFor<Department>().Entities
            .AsNoTracking().
            ProjectTo<DepartmentListResponse>(_mapper.ConfigurationProvider);

        var query = repository;

        if (!string.IsNullOrEmpty(list.GridQuery.Search))
        {
            query = query.Where(x => x.Name.Contains(list.GridQuery.Search));
        }

        var totalCount = await query.CountAsync();

        var sortField = list.GridQuery.SortField ?? nameof(DepartmentListResponse.Name);
        query = QuerySort(query, sortField, list.GridQuery.SortDir);

        var page = list.GridQuery.Page ?? 0;
        var size = list.GridQuery.PageSize ?? 20;
        var models = await query.Skip(page * size).Take(size).ToListAsync();

        var pagedList = new PagedList<DepartmentListResponse>(totalCount, models);
        return new ResponseWrapper<PagedList<DepartmentListResponse>>().Success(pagedList);
    }
    IQueryable<DepartmentListResponse> QuerySort(IQueryable<DepartmentListResponse> query, string sortField, DataGridQuerySortDirection sortDirection)
    {
        switch (sortField)
        {
            case nameof(DepartmentListResponse.Name):
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
