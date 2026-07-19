using Platform.Domain.Common;

namespace CleanArchitecture.Functions.Domain.Entities;

public class TodoItem : BaseAuditableEntity<int>
{
    public int ListId { get; set; }
    public string? Title { get; set; }
    public string? Note { get; set; }
    public bool Done { get; set; }
}