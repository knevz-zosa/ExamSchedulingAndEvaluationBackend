using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Entrance.Application.Feature.Management.AssessmentsResults.Interviews.Commands;
public class UpdateInterviewActiveCommand : BaseUpdateCommand<InterviewActiveUpdate>
{
    public UpdateInterviewActiveCommand(InterviewActiveUpdate update)
    {
        Update = update;
    }
}

public class UpdateInterviewActiveCommandHandler : BaseUpdateCommandHandler<UpdateInterviewActiveCommand, InterviewActiveUpdate>
{
    public UpdateInterviewActiveCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(UpdateInterviewActiveCommand command, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Interview>().GetAsync(command.Update.Id);

        if (resultInDb == null)
            return new ResponseWrapper<int>().Failed("Interview does not exists.");

        var result = resultInDb.UpdateIsActive(command.Update.IsUse, command.Update.UpdatedById);

        await _unitOfWork.WriteRepositoryFor<Interview>().UpdateAsync(result);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(result.Id, "Interview has been selected as active");
    }
}
