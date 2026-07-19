using CleanArchitecture.Functions.Application.Common.Interfaces;
using CleanArchitecture.Functions.Domain.Entities;
using FluentValidation;
using MediatR;

namespace CleanArchitecture.Functions.Application.TodoItems.Commands.CreateTodoItem;

public record CreateTodoItemCommand : IRequest<int>
{
    public int ListId { get; init; }
    public string? Title { get; init; }
}

public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
{
    public CreateTodoItemCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.ListId).GreaterThan(0);
    }
}

public class CreateTodoItemCommandHandler(IApplicationDbContext context)
    : IRequestHandler<CreateTodoItemCommand, int>
{
    public async Task<int> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoItem { ListId = request.ListId, Title = request.Title, Done = false };
        context.TodoItems.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}