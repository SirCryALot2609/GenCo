using GenCo.Application.DTOs.Common;
using GenCo.Domain.Enum;

namespace GenCo.Application.DTOs.EntityConstraint;

public class EntityConstraintBaseDto : AuditableDto
{
    public Guid EntityId { get; set; }
    public ConstraintType Type { get; set; }
    public string? ConstraintName { get; set; }
    public string? Expression { get; set; }
}
