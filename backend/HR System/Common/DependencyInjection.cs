using System.Diagnostics;
using System.Reflection;
using Common.Behaviors;
using Common.Exceptions.Handler;
using Common.HttpCLients;
using Common.HttpClients.Implementation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Common.HttpCLients.Implementation;


namespace Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
        {
            services.AddExceptionHandler<CustomExceptionHandler>();
            services.AddProblemDetails();
            return services;
        }

        public static IServiceCollection AddMediator(this IServiceCollection services, Assembly assembly)
        {

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });
            return services;
        }

        public static IServiceCollection AddHttpServiceClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("AccountServiceClient", client =>
            {
                client.BaseAddress = new Uri(configuration["HttpClientsConfig:AccountServiceClientUrl"]);
            });
            
            services.AddHttpClient("EmployeeServiceClient", client =>
            {
                client.BaseAddress = new Uri(configuration["HttpClientsConfig:EmployeeServiceClientUrl"]);
            });

            services.AddHttpClient("ProjectServiceClient", client =>
            {
                client.BaseAddress = new Uri(configuration["HttpClientsConfig:ProjectServiceClientUrl"]);
            });

            services.AddScoped<IEmployeeHttpClient, EmployeeHttpClient>();
            services.AddScoped<IAccountServiceHttpClient, AccountServiceHttpClient>();
            services.AddScoped<IProjectHttpClient, ProjectHttpClient>();

            return services;
        }

    }
}
