using EmployeeService.Application.Common.Repositories;
using EmployeeService.Infrastructure.Persistance;
using EmployeeService.Infrastructure.Persistance.Employee;
using EmployeeService.Infrastructure.Persistence.HolidayRequest;
using EmployeeService.Infrastructure.Persistence.HolidayRequestApprover;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeService.Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddDbContext<EmployeeDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IHolidayRequestRepository, HolidayRequestRepository>();
            services.AddScoped<IHolidayRequestApproverRepository, HolidayRequestApproverRepository>();

            return services;
        }
    }
}
