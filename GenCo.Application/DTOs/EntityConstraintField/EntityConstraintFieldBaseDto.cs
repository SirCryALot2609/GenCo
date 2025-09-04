namespace GenCo.GenCo.Application.DTOs.EntityConstraintField.Responses;

public class EntityConstraintFieldBaseDto : AuditableDto
{
    public Guid ConstraintId { get; set; }
    public Guid FieldId { get; set; }
}
