using GenCo.Application.DTOs.Common;
using GenCo.Domain.Entities;

namespace GenCo.Application.DTOs.FieldValidator.Requests;

public class CreateFieldValidatorRequestDto : BaseRequestDto
{
    public Guid FieldId { get; set; }
    public string Type { get; set; } = string.Empty;
    public FieldValidatorConfig? ConfigObject { get; set; }
}