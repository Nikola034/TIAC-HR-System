using System.Diagnostics;
using System.Reflection;
using Common.Behaviors;
using Common.Exceptions.Handler;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
