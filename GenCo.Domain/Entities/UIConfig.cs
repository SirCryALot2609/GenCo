using GenCo.Domain.Entities.Common;

namespace GenCo.Domain.Entities;

public class UIConfig : BaseEntity
{
    public Guid ProjectId { get; set; }
    public virtual Project Project { get; set; } = null!;
    public required string PageType { get; set; }
    public string? Config { get; set; }
}