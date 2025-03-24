using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace Entrance.Application.Feature.Management.Courses.Queries;
public class DetailsCourseQuery : BaseDetailsQuery<CourseDetailsResponse>
{
    public DetailsCourseQuery(int id) : base(id) { }    
}

public class DetailsCourseQueryHandler : BaseDetailsQueryHandler<DetailsCourseQuery, CourseDetailsResponse>
{
    public DetailsCourseQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

    public override async Task<ResponseWrapper<CourseDetailsResponse>> Handle(DetailsCourseQuery get, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Course>().Entities
                .AsNoTracking()
                .ProjectTo<CourseDetailsResponse>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.Id == get.Id, cancellationToken);

        if (resultInDb == null)
            return new ResponseWrapper<CourseDetailsResponse>().Failed(message: "Not found.");

        return new ResponseWrapper<CourseDetailsResponse>().Success(resultInDb);
    }
}
