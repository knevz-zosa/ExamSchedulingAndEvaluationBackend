using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Entrance.Application.Feature.Registration.Undergraduate.Commands;
public class CreateAcademicBackgroundCommand : BaseCreateCommand<AcademicBackgroundRequest>
{
    public CreateAcademicBackgroundCommand(AcademicBackgroundRequest request)
    {
        Request = request;
    }
}
public class CreateAcademicBackgroundCommandHandler : BaseCreateCommandHandler<CreateAcademicBackgroundCommand, AcademicBackgroundRequest>
{
    public CreateAcademicBackgroundCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }

    public override async Task<ResponseWrapper<int>> Handle(CreateAcademicBackgroundCommand command, CancellationToken cancellationToken)
    {
        var result = command.Request;
        var model = result.Adapt<AcademicBackground>();
        await _unitOfWork.WriteRepositoryFor<AcademicBackground>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id);
    }
}
