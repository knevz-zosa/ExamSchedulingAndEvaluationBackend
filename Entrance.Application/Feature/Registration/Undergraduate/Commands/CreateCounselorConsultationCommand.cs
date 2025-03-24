using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Mapster;
using MediatR;

namespace Entrance.Application.Feature.Registration.Undergraduate.Commands;
public class CreateCounselorConsultationCommand : BaseCreateCommand<CounselorConsultationRequest>
{
    public CreateCounselorConsultationCommand(CounselorConsultationRequest request)
    {
        Request = request;
    }
}
public class CreateCounselorConsultationCommandHandler : BaseCreateCommandHandler<CreateCounselorConsultationCommand, CounselorConsultationRequest>
{
    public CreateCounselorConsultationCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(CreateCounselorConsultationCommand command, CancellationToken cancellationToken)
    {
        var result = command.Request;

        var model = result.Adapt<CounselorConsultation>();
        await _unitOfWork.WriteRepositoryFor<CounselorConsultation>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id);
    }
}

