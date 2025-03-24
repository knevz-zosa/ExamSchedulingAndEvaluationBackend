using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;

namespace Entrance.Application.Feature.Management.Applicants.Commands;
public class UpdateRegisteredCommand : BaseUpdateCommand<ApplicantUpdateRegistered>
{
	public UpdateRegisteredCommand(ApplicantUpdateRegistered update)
	{
		Update = update;
	}
}

public class UpdateRegisteredCommandHandler : BaseUpdateCommandHandler<UpdateRegisteredCommand, ApplicantUpdateRegistered>
{
    public UpdateRegisteredCommandHandler(IUnitOfWork<int> unitOfWork) 
        : base(unitOfWork) { }

    public override async Task<ResponseWrapper<int>> Handle(UpdateRegisteredCommand command, CancellationToken cancellationToken)
    {
        var result = command.Update;
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Applicant>().GetAsync(command.Update.Id);

        if (resultInDb == null)
            return new ResponseWrapper<int>().Failed("Not found.");

        resultInDb.UpdateRegistered(result.Registered);

        await _unitOfWork.WriteRepositoryFor<Applicant>().UpdateAsync(resultInDb);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(result.Id, "Update successful.");
    }
}
