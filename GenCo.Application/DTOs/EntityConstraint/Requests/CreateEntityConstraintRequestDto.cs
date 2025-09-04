using GenCo.Application.DTOs.Common;
using GenCo.Domain.Enum;

namespace GenCo.Application.DTOs.EntityConstraint.Requests;

public class CreateEntityConstraintRequestDto : BaseRequestDto
{
    public Guid EntityId { get; set; }
    public ConstraintType Type { get; set; }
    public string ConstraintName { get; set; } = null!;
    public string? Expression { get; set; }
}
