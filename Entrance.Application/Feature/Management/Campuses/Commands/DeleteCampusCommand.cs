using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Wrapper;

namespace Entrance.Application.Feature.Management.Campuses.Commands;
public class DeleteCampusCommand : BaseDeleteCommand { }
public class DeleteCampusCommandHandler : BaseDeleteCommandHandler<DeleteCampusCommand>
{
    public DeleteCampusCommandHandler(IUnitOfWork<int> unitOfWork)
        : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(DeleteCampusCommand command, CancellationToken cancellationToken)
    {
        var model = await _unitOfWork.ReadRepositoryFor<Campus>().GetAsync(command.Id);

        if (model == null)
        {
            return new ResponseWrapper<int>().Failed("Not found.");
        }

        await _unitOfWork.WriteRepositoryFor<Campus>().DeleteAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id, "Delete successful.");
    }
}
