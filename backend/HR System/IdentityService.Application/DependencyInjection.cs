using Common;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {

        services.AddMediator(typeof(DependencyInjection).Assembly);
        return services;
    }
}
