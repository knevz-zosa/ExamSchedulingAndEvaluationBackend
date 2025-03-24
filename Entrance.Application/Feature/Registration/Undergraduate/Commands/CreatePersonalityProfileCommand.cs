using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Mapster;
using MediatR;

namespace Entrance.Application.Feature.Registration.Undergraduate.Commands;
public class CreatePersonalityProfileCommand : BaseCreateCommand<PersonalityProfileRequest>
{
    public CreatePersonalityProfileCommand(PersonalityProfileRequest request)
    {
        Request = request;
    }
}

public class CreatePersonalityProfileCommandHandler : BaseCreateCommandHandler<CreatePersonalityProfileCommand, PersonalityProfileRequest>
{
    public CreatePersonalityProfileCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(CreatePersonalityProfileCommand command, CancellationToken cancellationToken)
    {
        var result = command.Request;

        var model = result.Adapt<PersonalityProfile>();
        await _unitOfWork.WriteRepositoryFor<PersonalityProfile>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id);
    }
}

