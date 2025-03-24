using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Entrance.Application.Feature.Management.Applicants.Commands;
public class UpdateGWAStatusTrackCommand : BaseUpdateCommand<ApplicantUpdateGwaStatusTrack>
{
    public UpdateGWAStatusTrackCommand(ApplicantUpdateGwaStatusTrack update)
    {
        Update = update;
    }
}
public class UpdateGWAStatusTrackCommandHandler : BaseUpdateCommandHandler<UpdateGWAStatusTrackCommand, ApplicantUpdateGwaStatusTrack>
{
    public UpdateGWAStatusTrackCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }

    public override async Task<ResponseWrapper<int>> Handle(UpdateGWAStatusTrackCommand command, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Applicant>().GetAsync(command.Update.Id);

        if (resultInDb == null)
            return new ResponseWrapper<int>().Failed("Not found.");

        resultInDb.UpdateGwaStatusTrack(command.Update.GWA, command.Update.ApplicantStatus,
           command.Update.Track);

        await _unitOfWork.WriteRepositoryFor<Applicant>().UpdateAsync(resultInDb);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(resultInDb.Id, "Update successful.");
    }
}