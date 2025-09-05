namespace GenCo.Application.DTOs.EntityConstraintField.Responses;
public class EntityConstraintFieldDetailDto : EntityConstraintFieldBaseDto
{
    public string? ConstraintName { get; set; }
    public string? FieldName { get; set; }  // map từ Field.Name để client đọc
}