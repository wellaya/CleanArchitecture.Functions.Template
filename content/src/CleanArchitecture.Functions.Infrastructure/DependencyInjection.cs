using CleanArchitecture.Functions.Application.Common.Interfaces;
using CleanArchitecture.Functions.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Platform.Infrastructure;
using Platform.Infrastructure.Persistence;

namespace CleanArchitecture.Functions.Infrastructure;

public static class DependencyInjection
{
    private const string VerticalName = "CleanArchitectureFunctions";

    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPlatformInfrastructureServices(configuration);

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var resolver = sp.GetRequiredService<IVerticalConnectionStringResolver>();
            var connectionString = resolver.GetConnectionString(VerticalName);
            options.UseSqlServer(connectionString,
                sql => sql.MigrationsHistoryTable("__EFMigrationsHistory", "todo"));
        });

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}