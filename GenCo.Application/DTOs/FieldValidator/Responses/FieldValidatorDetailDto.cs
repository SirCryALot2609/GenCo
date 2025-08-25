using GenCo.Application.DTOs.Field.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.FieldValidator.Responses
{
    public class FieldValidatorDetailDto
    {
        public FieldResponseDto Field { get; set; } = default!;
    }
}
