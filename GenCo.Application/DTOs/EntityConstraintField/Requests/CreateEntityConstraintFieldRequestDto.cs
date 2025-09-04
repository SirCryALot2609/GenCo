namespace GenCo.GenCo.Application.DTOs.EntityConstraintField.Requests;

public class CreateEntityConstraintFieldRequestDto : BaseRequestDto
{
    public Guid ConstraintId { get; set; }
    public Guid FieldId { get; set; }
}
