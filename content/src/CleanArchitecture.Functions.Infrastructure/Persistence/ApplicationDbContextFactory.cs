using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Functions.Infrastructure.Persistence;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // Reads local.settings.json manually since EF design-time tooling doesn't know about it.
        var config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "CleanArchitecture.Functions.Functions"))
            .AddJsonFile("local.settings.json", optional: true)
            .Build();

        var connectionString = config["Values:ConnectionStrings:SharedPlatformDb"]
            ?? config["ConnectionStrings:SharedPlatformDb"]
            ?? "Server=(localdb)\\mssqllocaldb;Database=PlatformShared;Trusted_Connection=true;TrustServerCertificate=true";

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(connectionString,
            sql => sql.MigrationsHistoryTable("__EFMigrationsHistory", "todo"));

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}