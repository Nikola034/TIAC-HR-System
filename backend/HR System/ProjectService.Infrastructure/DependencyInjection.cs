using Application.Common.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectServiceInfrastructure.Persistence;
using ProjectServiceInfrastructure.Persistence.Client;
using ProjectServiceInfrastructure.Persistence.EmployeeProject;
using ProjectServiceInfrastructure.Persistence.Project;

namespace ProjectServiceInfrastructure;

public static class DependencyInjection
{

    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //var assembly = typeof(DependencyInjection).Assembly;
        services.AddDbContext<ProjectDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IEmployeeProjectRepository, EmployeeProjectRepository>();
        return services;
    }
}