namespace GenCo.GenCo.Application.DTOs.EntityConstraintField.Responses;

public class EntityConstraintFieldDetailDto : EntityConstraintFieldBaseDto
{
    public EntityConstraintBaseDto Constraint { get; set; } = default!;
    public FieldBaseDto Field { get; set; } = default!;
}
