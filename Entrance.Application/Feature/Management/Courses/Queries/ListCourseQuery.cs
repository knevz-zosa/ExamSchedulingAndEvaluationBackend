using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace Entrance.Application.Feature.Management.Courses.Queries;
public class ListCourseQuery : BaseListQuery<CourseListResponse>
{
    public string Access { get; set; }
}

public class ListCourseQueryHandler : BaseListQueryHandler<ListCourseQuery, CourseListResponse>
{
    public ListCourseQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    public override async Task<ResponseWrapper<PagedList<CourseListResponse>>> Handle(ListCourseQuery list, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.ReadRepositoryFor<Course>().Entities;
        var data = repository;
        if (list.Access == "All")
        {
            data = repository
               .AsNoTracking();
        }
        else
        {
            data = repository
               .Where(x => x.Campus.Name == list.Access)
               .AsNoTracking();
        }

        var query = data.ProjectTo<CourseListResponse>(_mapper.ConfigurationProvider);

        if (!string.IsNullOrEmpty(list.GridQuery.Search))
        {
            query = query.Where(
                x => x.Name.Contains(list.GridQuery.Search));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var sortField = list.GridQuery.SortField ?? nameof(CourseListResponse.Name);
        query = QuerySort(query, sortField, list.GridQuery.SortDir);

        var page = list.GridQuery.Page ?? 0;
        var size = list.GridQuery.PageSize ?? 20;
        var models = await query.Skip(page * size).Take(size).ToListAsync(cancellationToken);

        var pagedList = new PagedList<CourseListResponse>(totalCount, models);
        return new ResponseWrapper<PagedList<CourseListResponse>>().Success(pagedList);
    }
    IQueryable<CourseListResponse> QuerySort(IQueryable<CourseListResponse> query, string sortField, DataGridQuerySortDirection sortDirection)
    {
        switch (sortField)
        {
            case nameof(CourseListResponse.Name):
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
