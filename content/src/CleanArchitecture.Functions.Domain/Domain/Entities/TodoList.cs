using Platform.Domain.Common;

namespace CleanArchitecture.Functions.Domain.Entities;

public class TodoList : BaseAuditableEntity<int>
{
    public string? Title { get; set; }
    public string? Colour { get; set; } = "#3B82F6";
    private readonly List<TodoItem> _items = [];
    public IReadOnlyCollection<TodoItem> Items => _items.AsReadOnly();
}