using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Entrance.Application.Feature.Management.AssessmentsResults.Interviews.Commands;
public class CreateInterviewResultCommand : BaseCreateCommand<InterviewResultRequest>
{
    public CreateInterviewResultCommand(InterviewResultRequest request)
    {
        Request = request;
    }
}

public class CreateInterviewResultCommandHandler : BaseCreateCommandHandler<CreateInterviewResultCommand, InterviewResultRequest>
{
    public CreateInterviewResultCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(CreateInterviewResultCommand command, CancellationToken cancellationToken)
    {
        var result = command.Request;

        var existingResult = await _unitOfWork.ReadRepositoryFor<Interview>()
            .Entities.FirstOrDefaultAsync(x => x.CourseId == command.Request.CourseId && x.ApplicantId == command.Request.ApplicantId);

        if (existingResult != null)
            return new ResponseWrapper<int>().Failed(message: "Applicant has already been interviewed in this program.");

        var model = result.Adapt<Interview>();
        await _unitOfWork.WriteRepositoryFor<Interview>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id, "Record successful.");
    }
}

