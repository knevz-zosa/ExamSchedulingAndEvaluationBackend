using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Mapster;
using MediatR;

namespace Entrance.Application.Feature.Management.AssessmentsResults.Exams.Commands;
public class CreateExamResultCommand : BaseCreateCommand<ExaminationResultRequest>
{
    public CreateExamResultCommand(ExaminationResultRequest request)
    {
        Request = request;
    }
}

public class CreateExamResultCommandHandler : BaseCreateCommandHandler<CreateExamResultCommand, ExaminationResultRequest>
{
    public CreateExamResultCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public async override Task<ResponseWrapper<int>> Handle(CreateExamResultCommand command, CancellationToken cancellationToken)
    {
        var result = command.Request;

        var model = result.Adapt<Examination>();
        await _unitOfWork.WriteRepositoryFor<Examination>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id, "Record successful.");
    }
}

