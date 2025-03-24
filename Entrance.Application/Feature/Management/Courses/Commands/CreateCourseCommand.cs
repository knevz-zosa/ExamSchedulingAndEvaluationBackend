using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Entrance.Application.Feature.Management.Courses.Commands;
public class CreateCourseCommand : BaseCreateCommand<CourseRequest>
{
    public CreateCourseCommand(CourseRequest request)
    {
        Request = request;
    }
}

public class CreateCourseCommandHandler : BaseCreateCommandHandler<CreateCourseCommand, CourseRequest>
{
    public CreateCourseCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
    {
        var trimmedName = command.Request.Name.Trim().ToLower();

        var existingResult = await _unitOfWork.ReadRepositoryFor<Course>()
            .Entities.FirstOrDefaultAsync(x => x.Name.Trim().ToLower() == trimmedName
            && x.CampusId == command.Request.CampusId);

        if (existingResult != null)
            return new ResponseWrapper<int>().Failed(message: "Name already exists.");

        var model = command.Request.Adapt<Course>();
        model.Name = model.Name.Trim();

        model.DateCreated = DateTime.Now;


        var campus = await _unitOfWork.ReadRepositoryFor<Campus>()
           .Entities.FirstOrDefaultAsync(x => x.Id == command.Request.CampusId);

        if (campus != null && campus.HasDepartment && model.DepartmentId == null)
            return new ResponseWrapper<int>().Failed(message: "Department is required.");

        await _unitOfWork.WriteRepositoryFor<Course>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id, "Create successful.");
    }
}
