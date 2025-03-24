using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Mapster;
using MediatR;

namespace Entrance.Application.Feature.Management.Applicants.Commands;
public class CreateTransferCommand : BaseCreateCommand<TransferRequest>
{
    public CreateTransferCommand(TransferRequest request)
    {
        Request = request;
    }
}
public class CreateTransferCommandHandler : BaseCreateCommandHandler<CreateTransferCommand, TransferRequest>
{
    public CreateTransferCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(CreateTransferCommand command, CancellationToken cancellationToken)
    {
        var result = command.Request;

        var model = result.Adapt<Transfer>();
        await _unitOfWork.WriteRepositoryFor<Transfer>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id);
    }
}
