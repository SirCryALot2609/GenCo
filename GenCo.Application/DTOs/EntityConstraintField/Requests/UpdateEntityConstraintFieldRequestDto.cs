namespace GenCo.GenCo.Application.DTOs.EntityConstraintField.Requests;

public class UpdateEntityConstraintFieldRequestDto : BaseRequestDto
{
    public Guid Id { get; set; }
    public Guid ConstraintId { get; set; }
    public Guid FieldId { get; set; }
}
