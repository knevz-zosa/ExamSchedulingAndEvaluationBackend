using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace Entrance.Application.Feature.Management.Departments.Queries;
public class GetDepartmentQuery : BaseGetQuery<DepartmentResponse>
{
    public GetDepartmentQuery(int id) : base(id) { }
}

public class GetDepartmentQueryHandler : BaseGetQueryHandler<GetDepartmentQuery, DepartmentResponse>
{
    public GetDepartmentQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
    : base(unitOfWork, mapper) { }

    public override async Task<ResponseWrapper<DepartmentResponse>> Handle(GetDepartmentQuery get, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Department>().Entities
               .AsNoTracking()
               .ProjectTo<DepartmentResponse>(_mapper.ConfigurationProvider)
               .SingleOrDefaultAsync(x => x.Id == get.Id, cancellationToken);

        if (resultInDb == null)
            return new ResponseWrapper<DepartmentResponse>().Failed(message: "Not found.");

        return new ResponseWrapper<DepartmentResponse>().Success(resultInDb);
    }
}
