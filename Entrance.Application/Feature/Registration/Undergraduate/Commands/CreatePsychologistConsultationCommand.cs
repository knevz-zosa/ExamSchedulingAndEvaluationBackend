using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Mapster;
using MediatR;

namespace Entrance.Application.Feature.Registration.Undergraduate.Commands;
public class CreatePsychologistConsultationCommand : BaseCreateCommand<PsychologistConsultationRequest>
{
    public CreatePsychologistConsultationCommand(PsychologistConsultationRequest request)
    {
        Request = request;
    }
}
public class CreatePsychologistConsultationCommandHandler : BaseCreateCommandHandler<CreatePsychologistConsultationCommand, PsychologistConsultationRequest>
{
    public CreatePsychologistConsultationCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(CreatePsychologistConsultationCommand command, CancellationToken cancellationToken)
    {
        var result = command.Request;

        var model = result.Adapt<PsychologistConsultation>();
        await _unitOfWork.WriteRepositoryFor<PsychologistConsultation>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id);
    }
}
