using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Entrance.Application.Feature.Management.Departments.Commands;
public class UpdateDepartmentCommand : BaseUpdateCommand<DepartmentUpdate>
{
    public UpdateDepartmentCommand(DepartmentUpdate update)
    {
        Update = update;
    }
}
public class UpdateDepartmentCommandHandler : BaseUpdateCommandHandler<UpdateDepartmentCommand, DepartmentUpdate>
{
    public UpdateDepartmentCommandHandler(IUnitOfWork<int> unitOfWork)
        : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(UpdateDepartmentCommand command, CancellationToken cancellationToken)
    {
        var trimmedDepartmentName = command.Update.Name.Trim().ToLower();

        var resultExist = await _unitOfWork.ReadRepositoryFor<Department>()
            .Entities.FirstOrDefaultAsync(x => x.Id != command.Update.Id && x.Name.Trim().ToLower() == trimmedDepartmentName
            && x.CampusId == command.Update.CampusId);

        if (resultExist != null)
        {
            return new ResponseWrapper<int>().Failed("Name already exists.");
        }

        var resultInDb = await _unitOfWork.ReadRepositoryFor<Department>().GetAsync(command.Update.Id);

        if (resultInDb == null)
        {
            return new ResponseWrapper<int>().Failed("Not found.");
        }
        resultInDb.Update(command.Update.CampusId, command.Update.Name.Trim(),
            command.Update.UpdatedById);

        await _unitOfWork.WriteRepositoryFor<Department>().UpdateAsync(resultInDb);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(resultInDb.Id, "Update successful");
    }
}
