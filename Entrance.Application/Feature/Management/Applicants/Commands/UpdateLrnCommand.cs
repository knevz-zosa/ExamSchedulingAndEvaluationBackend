using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Entrance.Application.Feature.Management.Applicants.Commands;
public class UpdateLrnCommand : BaseUpdateCommand<ApplicantUpdateLrn>
{
    public UpdateLrnCommand(ApplicantUpdateLrn update)
    {
        Update = update;
    }
}
public class UpdateLrnCommandHandler : BaseUpdateCommandHandler<UpdateLrnCommand, ApplicantUpdateLrn>
{
    public UpdateLrnCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }

    public override async Task<ResponseWrapper<int>> Handle(UpdateLrnCommand command, CancellationToken cancellationToken)
    {
        var result = command.Update;

        var resultExist = await _unitOfWork.ReadRepositoryFor<Applicant>().Entities
            .Where(x => x.Registered == true)
            .AsNoTracking()
            .AnyAsync(x => x.Id != x.Id && x.LRN == result.LRN, cancellationToken);

        if (resultExist)
            return new ResponseWrapper<int>().Failed(message: "LRN already exists.");

        var resultInDb = await _unitOfWork.ReadRepositoryFor<Applicant>().GetAsync(command.Update.Id);

        if (resultInDb == null)
            return new ResponseWrapper<int>().Failed("Not found.");

        resultInDb.UpdateLrn(result.LRN);

        await _unitOfWork.WriteRepositoryFor<Applicant>().UpdateAsync(resultInDb);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(result.Id, "Update successful.");
    }
}

