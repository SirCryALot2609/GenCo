using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.FieldValidator.Requests
{
    public class CreateFieldValidatorRequestDto
    {
        public Guid FeildId { get; set; }
        public string ValidatorType { get; set; } = default!;
        public string? Config { get; set; }
    }
}
