using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ServiceModule
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IGradeService, GradeService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ILoggingService, LoggingService>();
            return services;
        }
    }
}
