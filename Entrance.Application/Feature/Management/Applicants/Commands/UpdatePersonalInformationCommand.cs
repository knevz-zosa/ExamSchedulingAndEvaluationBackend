using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace Entrance.Application.Feature.Management.Applicants.Commands;
public class UpdatePersonalInformationCommand : BaseUpdateCommand<PersonalInformationUpdate>
{
    public UpdatePersonalInformationCommand(PersonalInformationUpdate update)
    {
        Update = update;
    }
}
public class UpdatePersonalInformationCommandHandler : BaseUpdateCommandHandler<UpdatePersonalInformationCommand, PersonalInformationUpdate>
{
    public UpdatePersonalInformationCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(UpdatePersonalInformationCommand command, CancellationToken cancellationToken)
    {
        var trimmedFirstName = command.Update.FirstName.Trim().ToLower();
        var trimmedMiddleName = command.Update.MiddleName?.Trim().ToLower();
        var trimmedLastName = command.Update.LastName.Trim().ToLower();

        var existingResult = await _unitOfWork.ReadRepositoryFor<Applicant>().Entities
            .AsNoTracking()
            .AnyAsync(a => a.Registered != null &&
                a.PersonalInformation.FirstName.Trim().ToLower() == trimmedFirstName &&
                (a.PersonalInformation.MiddleName == null
                    ? trimmedMiddleName == null
                    : a.PersonalInformation.MiddleName.Trim().ToLower() == trimmedMiddleName) &&
                a.PersonalInformation.LastName.Trim().ToLower() == trimmedLastName &&
                a.PersonalInformation.DateofBirth.Date == command.Update.DateofBirth.Date &&
            a.Id != command.Update.ApplicantId, cancellationToken);

        if (existingResult)
            return new ResponseWrapper<int>().Failed(message: "Name already exists.");

        var resultInDb = await _unitOfWork.ReadRepositoryFor<PersonalInformation>().GetAsync(command.Update.Id);

        if (resultInDb == null)
            return new ResponseWrapper<int>().Failed("Not found.");

        resultInDb.Update(
                command.Update.ApplicantId,
                command.Update.FirstName.Trim(),
                command.Update.MiddleName?.Trim() ?? string.Empty,
                command.Update.LastName.Trim(),
                command.Update.NameExtension?.Trim() ?? string.Empty,
                command.Update.NickName?.Trim() ?? string.Empty,
                command.Update.Sex,
                command.Update.DateofBirth,
                command.Update.PlaceOfBirth?.Trim() ?? string.Empty,
                command.Update.Citizenship?.Trim() ?? string.Empty,
                command.Update.Email?.Trim() ?? string.Empty,
                command.Update.ContactNumber?.Trim() ?? string.Empty,
                command.Update.HouseNo?.Trim() ?? string.Empty,
                command.Update.Street?.Trim() ?? string.Empty,
                command.Update.Barangay?.Trim() ?? string.Empty,
                command.Update.Purok?.Trim() ?? string.Empty,
        command.Update.Municipality?.Trim() ?? string.Empty,
        command.Update.Province?.Trim() ?? string.Empty,
                command.Update.ZipCode?.Trim() ?? string.Empty
            );

        await _unitOfWork.WriteRepositoryFor<PersonalInformation>().UpdateAsync(resultInDb);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(resultInDb.Id, "Update successful.");
    }
}

