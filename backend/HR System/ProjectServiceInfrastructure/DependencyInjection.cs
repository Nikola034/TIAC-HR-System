using Application.Common.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectServiceInfrastructure.Persistence;
using ProjectServiceInfrastructure.Persistence.Client;

namespace ProjectServiceInfrastructure;

public static class DependencyInjection
{

    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //var assembly = typeof(DependencyInjection).Assembly;
        services.AddDbContext<ClientDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IClientRepository, ClientRepository>();
        return services;
    }
}