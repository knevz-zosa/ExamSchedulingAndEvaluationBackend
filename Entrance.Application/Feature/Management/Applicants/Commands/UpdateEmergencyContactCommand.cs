using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Entrance.Application.Feature.Management.Applicants.Commands;
public class UpdateEmergencyContactCommand : BaseUpdateCommand<EmergencyContactUpdate>
{
    public UpdateEmergencyContactCommand(EmergencyContactUpdate update)
    {
        Update = update;
    }
}
public class UpdateEmergencyContactCommandHandler : BaseUpdateCommandHandler<UpdateEmergencyContactCommand, EmergencyContactUpdate>
{
    public UpdateEmergencyContactCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }

    public override async Task<ResponseWrapper<int>> Handle(UpdateEmergencyContactCommand command, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<EmergencyContact>().GetAsync(command.Update.Id);

        if (resultInDb == null)
            return new ResponseWrapper<int>().Failed("Not found.");

        var result = resultInDb.Update(command.Update.Name.Trim(), command.Update.ContactNo,
            command.Update.Address, command.Update.Relationship);

        await _unitOfWork.WriteRepositoryFor<EmergencyContact>().UpdateAsync(result);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(result.Id, "Update successful.");
    }
}
