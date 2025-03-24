using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Tool;
using Entrance.Shared.Wrapper;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Entrance.Application.Feature.Registration.Undergraduate.Commands;
public class CreateApplicantCommand : BaseCreateCommand<ApplicantRequest>
{
    public CreateApplicantCommand(ApplicantRequest request)
    {
        Request = request;
    }
}
public class CreateApplicantCommandHandler : BaseCreateCommandHandler<CreateApplicantCommand, ApplicantRequest>
{
    public CreateApplicantCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(CreateApplicantCommand command, CancellationToken cancellationToken)
    {
        var result = command.Request;

        var isLrnExist = await _unitOfWork.ReadRepositoryFor<Applicant>().Entities
            .Where(x => x.Registered ==  true)
            .AsNoTracking()
            .AnyAsync(r => r.LRN == result.LRN, cancellationToken);

        if (isLrnExist)
            return new ResponseWrapper<int>().Failed(message: "LRN already exists.");

        var model = result.Adapt<Applicant>();
        model.ControlNo = Utility.GenerateControlNumber();
        await _unitOfWork.WriteRepositoryFor<Applicant>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id);
    }
}
