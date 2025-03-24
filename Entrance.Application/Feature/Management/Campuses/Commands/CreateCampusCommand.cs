using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Entrance.Application.Feature.Management.Campuses.Commands;
public class CreateCampusCommand : BaseCreateCommand<CampusRequest>
{
    public CreateCampusCommand(CampusRequest request)
    {
        Request = request;
    }
}
public class CreateCampusCommandHandler : BaseCreateCommandHandler<CreateCampusCommand, CampusRequest>
{
    public CreateCampusCommandHandler(IUnitOfWork<int> unitOfWork)
        : base(unitOfWork) { }

    public override async Task<ResponseWrapper<int>> Handle(CreateCampusCommand command, CancellationToken cancellationToken)
    {
        var trimmedName = command.Request.Name.Trim().ToLower();

        var existingResult = await _unitOfWork.ReadRepositoryFor<Campus>()
            .Entities.FirstOrDefaultAsync(x => x.Name.Trim().ToLower() == trimmedName);

        if (existingResult != null)
            return new ResponseWrapper<int>().Failed(message: "Name already exists.");

        var model = command.Request.Adapt<Campus>();

        model.Name = model.Name.Trim();
        model.DateCreated = DateTime.Now;

        await _unitOfWork.WriteRepositoryFor<Campus>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id, "Create successful.");
    }
}
