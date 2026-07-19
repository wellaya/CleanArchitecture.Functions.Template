using CleanArchitecture.Functions.Application.Common.Interfaces;
using CleanArchitecture.Functions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Platform.Domain.Common;
using Platform.Infrastructure.Persistence;

namespace CleanArchitecture.Functions.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : BasePlatformDbContext(options), IApplicationDbContext
{
    public DbSet<TodoList> TodoLists => Set<TodoList>();
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("todo");
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(builder);   
    }
}
