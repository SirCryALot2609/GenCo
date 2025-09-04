using GenCo.Application.DTOs.EntityConstraint;
using GenCo.Application.DTOs.Field;

namespace GenCo.Application.DTOs.EntityConstraintField.Responses;

public class EntityConstraintFieldDetailDto : EntityConstraintFieldBaseDto
{
    public EntityConstraintBaseDto Constraint { get; set; } = null!;
    public FieldBaseDto Field { get; set; } = null!;
}
