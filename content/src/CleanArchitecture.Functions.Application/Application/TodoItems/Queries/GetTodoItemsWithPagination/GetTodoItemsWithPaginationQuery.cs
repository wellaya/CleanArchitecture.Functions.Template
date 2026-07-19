using CleanArchitecture.Functions.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common.Models;

namespace CleanArchitecture.Functions.Application.TodoItems.Queries.GetTodoItemsWithPagination;

public record TodoItemBriefDto
{
    public int Id { get; init; }
    public int ListId { get; init; }
    public string? Title { get; init; }
    public bool Done { get; init; }
}

public record GetTodoItemsWithPaginationQuery : IRequest<PaginatedList<TodoItemBriefDto>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetTodoItemsWithPaginationQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetTodoItemsWithPaginationQuery, PaginatedList<TodoItemBriefDto>>
{
    public async Task<PaginatedList<TodoItemBriefDto>> Handle(
        GetTodoItemsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = context.TodoItems
            .Where(x => x.ListId == request.ListId)
            .OrderBy(x => x.Title)
            .Select(x => new TodoItemBriefDto { Id = x.Id, ListId = x.ListId, Title = x.Title, Done = x.Done });

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedList<TodoItemBriefDto>(items, totalCount, request.PageNumber, request.PageSize);
    }
}