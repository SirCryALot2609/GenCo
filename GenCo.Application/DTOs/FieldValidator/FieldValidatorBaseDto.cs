using GenCo.Application.DTOs.Common;

namespace GenCo.Application.DTOs.FieldValidator;

public class FieldValidatorBaseDto : AuditableDto
{
    public Guid FieldId { get; set; }
    public string Type { get; set; } = string.Empty; // Enum -> string để client dễ đọc
    public string? ConfigJson { get; set; }
}