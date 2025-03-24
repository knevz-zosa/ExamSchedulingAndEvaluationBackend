using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace Entrance.Application.Feature.Management.Departments.Queries;
public class DetailsDepartmentQuery : BaseDetailsQuery<DepartmentDetailsResponse>
{
	public DetailsDepartmentQuery(int id) : base(id) { }
}

public class DetailsDepartmentQueryHandler : BaseDetailsQueryHandler<DetailsDepartmentQuery, DepartmentDetailsResponse>
{
    public DetailsDepartmentQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) 
        : base(unitOfWork, mapper) { }

    public override async Task<ResponseWrapper<DepartmentDetailsResponse>> Handle(DetailsDepartmentQuery get, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Department>().Entities
              .AsNoTracking()
              .ProjectTo<DepartmentDetailsResponse>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(x => x.Id == get.Id, cancellationToken);

        if (resultInDb == null)
            return new ResponseWrapper<DepartmentDetailsResponse>().Failed(message: "Not found.");

        return new ResponseWrapper<DepartmentDetailsResponse>().Success(resultInDb);
    }
}
