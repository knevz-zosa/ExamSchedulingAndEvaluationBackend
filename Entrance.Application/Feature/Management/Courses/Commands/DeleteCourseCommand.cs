using Entrance.Application.IRepositories;
using Entrance.Application.Operations;
using Entrance.Domain.Entities;
using Entrance.Shared.Wrapper;

namespace Entrance.Application.Feature.Management.Courses.Commands;
public class DeleteCourseCommand : BaseDeleteCommand { }
public class DeleteCourseCommandHandler : BaseDeleteCommandHandler<DeleteCourseCommand>
{
    public DeleteCourseCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }

    public override async Task<ResponseWrapper<int>> Handle(DeleteCourseCommand command, CancellationToken cancellationToken)
    {
        var model = await _unitOfWork.ReadRepositoryFor<Course>().GetAsync(command.Id);

        if (model == null)
        {
            return new ResponseWrapper<int>().Failed("Not found.");
        }

        await _unitOfWork.WriteRepositoryFor<Course>().DeleteAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id, "Delete successful.");
    }
}
