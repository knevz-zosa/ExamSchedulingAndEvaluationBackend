using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace Entrance.Application.Feature.Management.Applicants.Queries;
public class ListInProgressQuery : BaseListQuery<ApplicantInprogressListResponse>
{
    public string Access { get; set; } = "All";
}
public class ListInProgressQueryHandler : BaseListQueryHandler<ListInProgressQuery, ApplicantInprogressListResponse>
{
    public ListInProgressQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    public override async Task<ResponseWrapper<PagedList<ApplicantInprogressListResponse>>> Handle(ListInProgressQuery list, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.ReadRepositoryFor<Applicant>().Entities;
        var data = repository;

        if (list.Access != "All")
        {
            data = data
            .AsNoTracking()
            .Where(x => x.Schedule.Campus.Name == list.Access);
        }
        else
        {
            data = data
            .AsNoTracking();
        }
        data = data.Where(a => a.Registered == false);
        var query = data.ProjectTo<ApplicantInprogressListResponse>(_mapper.ConfigurationProvider);

        if (!string.IsNullOrEmpty(list.GridQuery.Search))
        {            
            query = query.Where(c =>
                 c.FullName.Contains(list.GridQuery.Search) ||
                 c.CampusName.Contains(list.GridQuery.Search));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var sortField = list.GridQuery.SortField ?? nameof(Applicant.TransactionDate);
        query = QuerySort(query, sortField, list.GridQuery.SortDir);

        var page = list.GridQuery.Page ?? 0;
        var size = list.GridQuery.PageSize ?? 20;
        var models = await query.Skip(page * size).Take(size).ToListAsync(cancellationToken);

        var pagedList = new PagedList<ApplicantInprogressListResponse>(totalCount, models);

        return new ResponseWrapper<PagedList<ApplicantInprogressListResponse>>().Success(pagedList);
    }
    IQueryable<ApplicantInprogressListResponse> QuerySort(IQueryable<ApplicantInprogressListResponse> query, string sortField, DataGridQuerySortDirection sortDirection)
    {
        switch (sortField)
        {
            case "Name":
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.FullName)
                    : query.OrderByDescending(c => c.FullName);
            case "CampusName":
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.CampusName)
                    : query.OrderByDescending(c => c.CampusName);
            case "CourseName":
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.CampusName)
                    : query.OrderByDescending(c => c.CourseName);
            default:
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.Id)
                    : query.OrderByDescending(c => c.Id);
        }
    }
}

