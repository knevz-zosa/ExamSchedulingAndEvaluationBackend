using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace Entrance.Application.Feature.Management.Applicants.Queries;
public class ListApplicantQuery : BaseListQuery<ApplicantListResponse>
{
    public string Access { get; set; }
}
public class ListApplicantQueryHandler : BaseListQueryHandler<ListApplicantQuery, ApplicantListResponse>
{
    public ListApplicantQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    public override async Task<ResponseWrapper<PagedList<ApplicantListResponse>>> Handle(ListApplicantQuery list, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.ReadRepositoryFor<Applicant>().Entities
       .Where(x => x.Registered ==  true)
       .AsNoTracking()
       .ProjectTo<ApplicantListResponse>(_mapper.ConfigurationProvider);

        if (list.Access != "All")
        {
            query = query.Where(x => x.CampusName == list.Access);
        }        

        if (!string.IsNullOrEmpty(list.GridQuery.Search))
        {
            var searchTerm = list.GridQuery.Search.ToLower();
            query = query.Where(c =>
                c.PersonalInformation.LastName.Contains(searchTerm) ||
                c.PersonalInformation.FirstName.Contains(searchTerm) ||
                c.CampusName.Contains(searchTerm) ||
                c.CourseName.Contains(searchTerm)
            );
        }

        if (!string.IsNullOrEmpty(list.GridQuery.SchoolYear))
        {
            query = query.Where(c => c.SchoolYear.Equals(list.GridQuery.SchoolYear));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var sortField = list.GridQuery.SortField ?? nameof(ApplicantListResponse.PersonalInformation.LastName);
        query = QuerySort(query, sortField, list.GridQuery.SortDir);

        var page = list.GridQuery.Page ?? 0;
        var size = list.GridQuery.PageSize ?? 20;
        var models = await query.Skip(page * size).Take(size).ToListAsync(cancellationToken);

        var pagedList = new PagedList<ApplicantListResponse>(totalCount, models);

        return new ResponseWrapper<PagedList<ApplicantListResponse>>().Success(pagedList);
    }

    IQueryable<ApplicantListResponse> QuerySort(IQueryable<ApplicantListResponse> query, string sortField, DataGridQuerySortDirection sortDirection)
    {
        switch (sortField)
        {
            case "LastName":
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.PersonalInformation.LastName)
                    : query.OrderByDescending(c => c.PersonalInformation.LastName);
            case nameof(ApplicantListResponse.CampusName):
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.CampusName)
                    : query.OrderByDescending(c => c.CampusName);
            case nameof(ApplicantListResponse.CourseName):
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.CourseName)
                    : query.OrderByDescending(c => c.CourseName);
            default:
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.Id)
                    : query.OrderByDescending(c => c.Id);
        }
    }
}
