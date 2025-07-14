using Microsoft.Extensions.DependencyInjection;

namespace Application.Mappings
{
    public static class AutoMapperModule
    {
        public static IServiceCollection AddAutoMapperModule(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<StudentProfile>();
                cfg.AddProfile<DepartmentProfile>();
                cfg.AddProfile<GradeProfile>();
                cfg.AddProfile<TeacherProfile>();
                cfg.AddProfile<CourseProfile>();
                cfg.AddProfile<LoggingProfile>();
            });

            return services;
        }
    }
}
