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
public class ConnectService : IConnectService
{
    public IAuthService Auth { get; }
    public IUserService User { get; }
    public ICampusService Campus { get; }
    public IDepartmentService Department { get; }
    public ICourseService Course { get; }
    public IScheduleService Schedule { get; }
    public IUndergraduateRegistrationService Undergraduate { get; }
    public IApplicantService Applicant { get; }
    public IExamResultService ExamResult { get; }
    public IInterviewResultService InterviewResult { get; }
    public ConnectService
    (
        IAuthService auth,
        IUserService user,
        ICampusService campus,
        IDepartmentService department,
        ICourseService course,
        IScheduleService schedule,
        IUndergraduateRegistrationService undergraduate,
        IApplicantService applicant,
        IExamResultService examResult,
        IInterviewResultService interviewResult 
    )
    {
        Auth = auth;
        User = user;
        Campus = campus;
        Department = department;
        Course = course;
        Schedule = schedule;
        Undergraduate = undergraduate;
        Applicant = applicant;
        ExamResult = examResult;
        InterviewResult = interviewResult;
    }
}
