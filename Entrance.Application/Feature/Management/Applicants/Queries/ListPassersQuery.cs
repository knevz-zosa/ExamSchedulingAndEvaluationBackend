using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Tool;
using Entrance.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace Entrance.Application.Feature.Management.Applicants.Queries;
public class ListPassersQuery : BaseListQuery<ApplicantPassersListResponse>
{
    public string Access { get; set; }
}
public class ListPassersQueryHandler : BaseListQueryHandler<ListPassersQuery, ApplicantPassersListResponse>
{
    public ListPassersQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    public override async Task<ResponseWrapper<PagedList<ApplicantPassersListResponse>>> Handle(ListPassersQuery list, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.ReadRepositoryFor<Applicant>().Entities
           .Where(x => x.Registered == true)
           .AsNoTracking()
           .ProjectTo<ApplicantPassersListResponse>(_mapper.ConfigurationProvider);

        if (list.Access != "All")
        {
            query = query.Where(x => x.Campus.Name == list.Access);
        }

        if (!string.IsNullOrEmpty(list.GridQuery.Search))
        {
            var searchTerm = list.GridQuery.Search.ToLower();

            query = query.Where(c =>
                c.PersonalInformation.LastName.ToLower().Contains(searchTerm) ||
                c.PersonalInformation.FirstName.ToLower().Contains(searchTerm) ||
                c.Campus.Courses.Any(course =>
                    course.Id == c.CourseId &&
                    (course.Name.ToLower().Contains(searchTerm) || course.Name.ToLower() == searchTerm)));
        }

        if (!string.IsNullOrEmpty(list.GridQuery.SchoolYear))
        {
            query = query.Where(c => c.SchoolYear.Equals(list.GridQuery.SchoolYear));
        }

        var sortField = list.GridQuery.SortField ?? nameof(ApplicantPassersListResponse.PersonalInformation.LastName);
        query = QuerySort(query.AsQueryable(), sortField, list.GridQuery.SortDir);

        var totalCount = query.Count();

        var page = list.GridQuery.Page ?? 0;
        var size = list.GridQuery.PageSize ?? 20;

        var models = await query.Skip(page * size).Take(size).ToListAsync(cancellationToken);

        models = models.Where(passers => Utility.Remarks(null, passers) == "Passed").ToList();

        foreach (var model in models)
        {
            model.Remarks = Utility.Remarks(null, model);  
        }

        var pagedList = new PagedList<ApplicantPassersListResponse>(totalCount, models);

        return new ResponseWrapper<PagedList<ApplicantPassersListResponse>>().Success(pagedList);
    }

    IQueryable<ApplicantPassersListResponse> QuerySort(IQueryable<ApplicantPassersListResponse> query, string sortField, DataGridQuerySortDirection sortDirection)
    {
        switch (sortField)
        {
            case "LastName":
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.PersonalInformation.LastName)
                    : query.OrderByDescending(c => c.PersonalInformation.LastName);
            case "PersonalInformation.LastName":
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.PersonalInformation.LastName)
                    : query.OrderByDescending(c => c.PersonalInformation.LastName);
            case "OverallTotalRating":
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => double.Parse(Utility.OverallTotalRating(null, c)))
                    : query.OrderByDescending(c => double.Parse(Utility.OverallTotalRating(null, c)));
            case "ExaminationResult":
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => double.Parse(Utility.ExaminationResult(null, c)))
                    : query.OrderByDescending(c => double.Parse(Utility.ExaminationResult(null, c)));
            case "InterviewResult":
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => double.Parse(Utility.InterviewResult(null, c)))
                    : query.OrderByDescending(c => double.Parse(Utility.InterviewResult(null, c)));
            default:
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.PersonalInformation.LastName)
                    : query.OrderByDescending(c => c.PersonalInformation.LastName);
        }
    }

}

