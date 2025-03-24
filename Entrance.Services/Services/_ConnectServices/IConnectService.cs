using Entrance.Services.Services.ApplicantServices;
using Entrance.Services.Services.AuthServices;
using Entrance.Services.Services.CampusServices;
using Entrance.Services.Services.CourseServices;
using Entrance.Services.Services.DepartmentServices;
using Entrance.Services.Services.ExamResultServices;
using Entrance.Services.Services.InterviewResultServices;
using Entrance.Services.Services.RegistrationServices.Undergraduates;
using Entrance.Services.Services.ScheduleServices;
using Entrance.Services.Services.UserServices;

namespace Entrance.Services.Services._ConnectServices;
public interface IConnectService
{
    public IAuthService Auth { get; }
    public IUserService User { get; }
    public ICampusService Campus { get; }
    public IDepartmentService Department { get; }
    public ICourseService Course { get; }
    public IScheduleService Schedule { get; }
    public IUndergraduateRegistrationService Undergraduate { get; }
    public IApplicantService Applicant { get; }
    public IExamResultService ExamResult { get;}
    public IInterviewResultService InterviewResult { get;}
}
