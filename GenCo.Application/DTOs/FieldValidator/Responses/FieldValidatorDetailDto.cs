using GenCo.Domain.Entities;

namespace GenCo.Application.DTOs.FieldValidator.Responses;

public class FieldValidatorDetailDto : FieldValidatorBaseDto
{
    public FieldValidatorConfig? ConfigObject { get; set; } // deserialize từ ConfigJson
    public string? FieldName { get; set; } // map từ Field.ColumnName hoặc Field.Name
}