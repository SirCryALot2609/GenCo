using GenCo.Application.DTOs.Common;

namespace GenCo.Application.DTOs.EntityConstraintField;

public class EntityConstraintFieldBaseDto : AuditableDto
{
    public Guid ConstraintId { get; set; }
    public Guid FieldId { get; set; }
}
