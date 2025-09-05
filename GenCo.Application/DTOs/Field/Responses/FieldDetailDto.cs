using GenCo.Application.DTOs.FieldValidator;

namespace GenCo.Application.DTOs.Field.Responses;

public class FieldDetailDto : FieldBaseDto
{
    public ICollection<FieldValidatorBaseDto> Validators { get; set; } = [];
}