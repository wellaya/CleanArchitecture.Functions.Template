using Microsoft.Extensions.DependencyInjection;
using Platform.Application;

namespace CleanArchitecture.Functions.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddPlatformApplicationServices(typeof(DependencyInjection).Assembly);
        return services;
    }
}