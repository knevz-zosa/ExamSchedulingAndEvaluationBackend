﻿using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Mapster;
using MediatR;

namespace Entrance.Application.Feature.Registration.Undergraduate.Commands;
public class CreatePhysicalHealthCommand : BaseCreateCommand<PhysicalHealthRequest>
{
    public CreatePhysicalHealthCommand(PhysicalHealthRequest request)
    {
        Request = request;
    }
}
public class CreatePhysicalHealthCommandHandler : BaseCreateCommandHandler<CreatePhysicalHealthCommand, PhysicalHealthRequest>
{
    public CreatePhysicalHealthCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(CreatePhysicalHealthCommand command, CancellationToken cancellationToken)
    {
        var result = command.Request;

        var model = result.Adapt<PhysicalHealth>();
        await _unitOfWork.WriteRepositoryFor<PhysicalHealth>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id);
    }
}
