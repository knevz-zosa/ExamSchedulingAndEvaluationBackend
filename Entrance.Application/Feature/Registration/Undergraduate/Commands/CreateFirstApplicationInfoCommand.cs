﻿using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Mapster;
using MediatR;

namespace Entrance.Application.Feature.Registration.Undergraduate.Commands;
public class CreateFirstApplicationInfoCommand : BaseCreateCommand<FirstApplicationInfoRequest>
{
    public CreateFirstApplicationInfoCommand(FirstApplicationInfoRequest request)
    {
        Request = request;
    }
}
public class CreateFirstApplicationInfoCommandHandler : BaseCreateCommandHandler<CreateFirstApplicationInfoCommand, FirstApplicationInfoRequest>
{
    public CreateFirstApplicationInfoCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(CreateFirstApplicationInfoCommand command, CancellationToken cancellationToken)
    {
        var result = command.Request;

        var model = result.Adapt<FirstApplicationInfo>();
        await _unitOfWork.WriteRepositoryFor<FirstApplicationInfo>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id);
    }
}
