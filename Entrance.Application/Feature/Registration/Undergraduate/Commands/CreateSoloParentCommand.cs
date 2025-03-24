using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Mapster;
using MediatR;

namespace Entrance.Application.Feature.Registration.Undergraduate.Commands;
public class CreateSoloParentCommand : BaseCreateCommand<SoloParentRequest>
{
    public CreateSoloParentCommand(SoloParentRequest request)
    {
        Request = request;
    }
}
public class CreateSoloParentCommandHandler : BaseCreateCommandHandler<CreateSoloParentCommand, SoloParentRequest>
{
    public CreateSoloParentCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(CreateSoloParentCommand command, CancellationToken cancellationToken)
    {
        var result = command.Request;

        var model = result.Adapt<SoloParent>();
        await _unitOfWork.WriteRepositoryFor<SoloParent>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id);
    }
}
