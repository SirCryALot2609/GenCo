using GenCo.Domain.Entities.Common;

namespace GenCo.Domain.Entities;

public class ServiceConfig : BaseEntity
{
    public Guid ProjectId { get; set; }
    public virtual Project Project { get; set; } = null!;
    public required string ServiceType { get; set; }
    public string? Config { get; set; }
}