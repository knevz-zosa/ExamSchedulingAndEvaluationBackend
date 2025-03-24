using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Entrance.Application.Feature.Registration.Undergraduate.Commands;
public class CreatePersonalInformationCommand : BaseCreateCommand<PersonalInformationRequest>
{
    public CreatePersonalInformationCommand(PersonalInformationRequest request)
    {
        Request = request;
    }
}
public class CreatePersonalInformationCommandHandler : BaseCreateCommandHandler<CreatePersonalInformationCommand, PersonalInformationRequest>
{
    public CreatePersonalInformationCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(CreatePersonalInformationCommand command, CancellationToken cancellationToken)
    {
        int applicantId = command.Request.ApplicantId;
        var applicant = await _unitOfWork.ReadRepositoryFor<Applicant>().Entities
             .Include(a => a.Schedule)
             .FirstOrDefaultAsync(a => a.Id == applicantId, cancellationToken);
        string schoolYear = applicant.Schedule.SchoolYear;

        var result = command.Request;
        var trimmedFirstName = result.FirstName.Trim().ToLower();
        var trimmedMiddleName = result.MiddleName?.Trim().ToLower();
        var trimmedLastName = result.LastName.Trim().ToLower();


        var isRegistered = await _unitOfWork.ReadRepositoryFor<Applicant>().Entities
            .Where(x => x.Registered == true)
            .AsNoTracking()
            .Include(x => x.PersonalInformation)
            .AnyAsync(a =>
                a.PersonalInformation.FirstName.Trim().ToLower() == trimmedFirstName &&
                (a.PersonalInformation.MiddleName == null
                    ? trimmedMiddleName == null
                    : a.PersonalInformation.MiddleName.Trim().ToLower() == trimmedMiddleName) &&
                a.PersonalInformation.LastName.Trim().ToLower() == trimmedLastName &&
                a.PersonalInformation.DateofBirth.Date == result.DateofBirth.Date &&
                a.Schedule.SchoolYear == schoolYear, cancellationToken);

        if (isRegistered)
            return new ResponseWrapper<int>().Failed(message: "Name already exists.");
        var model = result.Adapt<PersonalInformation>();

        model.FirstName = model.FirstName.Trim();
        model.MiddleName = model.MiddleName?.Trim();
        model.LastName = model.LastName.Trim();

        await _unitOfWork.WriteRepositoryFor<PersonalInformation>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id);
    }
}

