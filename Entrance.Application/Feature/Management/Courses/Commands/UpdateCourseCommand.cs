using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Models;
using Entrance.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Entrance.Application.Feature.Management.Courses.Commands;
public class UpdateCourseCommand : BaseUpdateCommand<CourseUpdate>
{
    public UpdateCourseCommand(CourseUpdate update)
    {
        Update = update;
    }
}
public class UpdateCourseCommandHandler : BaseUpdateCommandHandler<UpdateCourseCommand, CourseUpdate>
{
    public UpdateCourseCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task<ResponseWrapper<int>> Handle(UpdateCourseCommand command, CancellationToken cancellationToken)
    {
        var trimmedDepartmentName = command.Update.Name.Trim().ToLower();

        var resultExist = await _unitOfWork.ReadRepositoryFor<Course>()
            .Entities.FirstOrDefaultAsync(x => x.Id != command.Update.Id && x.Name.Trim().ToLower() == trimmedDepartmentName
            && x.CampusId == command.Update.CampusId);

        if (resultExist != null)
        {
            return new ResponseWrapper<int>().Failed("Name already exists.");
        }

        var resultInDb = await _unitOfWork.ReadRepositoryFor<Course>().GetAsync(command.Update.Id);

        if (resultInDb == null)
        {
            return new ResponseWrapper<int>().Failed("Not found.");
        }
        resultInDb.Update(command.Update.CampusId, command.Update.DepartmentId, command.Update.Name.Trim(),
            command.Update.UpdatedById, command.Update.IsOpen);

        await _unitOfWork.WriteRepositoryFor<Course>().UpdateAsync(resultInDb);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(resultInDb.Id, "Update successful.");
    }
}
