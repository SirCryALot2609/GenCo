using GenCo.Application.DTOs.Field;
using GenCo.Domain.Entities;

namespace GenCo.Application.DTOs.FieldValidator.Responses
{
    public class FieldValidatorDetailDto
    {
        public FieldBaseDto Field { get; set; } = default!;
        public FieldValidatorConfig ConfigObject { get; set; } = new();
    }
}
