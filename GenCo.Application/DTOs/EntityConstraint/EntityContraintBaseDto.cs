using GenCo.Application.DTOs.Common;
using GenCo.Domain.Enum;

namespace GenCo.Application.DTOs.EntityConstraint;

public class EntityConstraintBaseDto : AuditableDto
{
    public Guid EntityId { get; set; }
    public string Type { get; set; } = string.Empty; // Enum -> string để client dễ đọc
    public string? ConstraintName { get; set; }
    public string? Expression { get; set; }
}
