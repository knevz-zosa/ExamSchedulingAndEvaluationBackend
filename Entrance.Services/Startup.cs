using Entrance.Application.IRepositories;
using Entrance.Infrastructure.CurrentUser;
using Entrance.Infrastructure.Mapping;
using Entrance.Infrastructure.Repositories;
using Entrance.Infrastructure.Token;
using Entrance.Services.Services._ConnectServices;
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
using Microsoft.Extensions.DependencyInjection;

namespace Entrance.Services;
public static class Startup
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddHttpClient();
        return services
            .AddScoped(typeof(IReadRepositoryAsync<,>), typeof(ReadRepositoryAsync<,>))
            .AddScoped(typeof(IWriteRepositoryAsync<,>), typeof(WriteRepositoryAsync<,>))
            .AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
            .AddScoped<ICurrentRepository, CurrentRepository>()
            .AddScoped<ITokenService, TokenService>()
            .AddScoped<IConnectService, ConnectService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<ICampusService, CampusService>()
            .AddScoped<IDepartmentService, DepartmentService>()
            .AddScoped<ICourseService, CourseService>()
            .AddScoped<IScheduleService, ScheduleService>()
            .AddScoped<IUndergraduateRegistrationService, UndergraduateRegistrationService>()
            .AddScoped<IApplicantService, ApplicantService>()
            .AddScoped<IExamResultService, ExamResultService>()
            .AddScoped<IInterviewResultService, InterviewResultService>()
            .AddAutoMapper(cfg =>
             {
                 cfg.AddProfile<UserMapping>();
             }, typeof(UserMapping).Assembly);
    }
}
