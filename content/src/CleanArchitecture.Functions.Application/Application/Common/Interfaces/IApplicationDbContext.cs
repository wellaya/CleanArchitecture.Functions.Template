using CleanArchitecture.Functions.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Functions.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }
    DbSet<TodoItem> TodoItems { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}