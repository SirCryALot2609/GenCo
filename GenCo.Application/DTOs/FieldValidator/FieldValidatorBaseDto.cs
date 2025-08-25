using GenCo.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.FieldValidator
{
    public class FieldValidatorBaseDto : AuditableDto
    {
        public string ValidatorType { get; set; } = default!;
        public string? Config { get; set; }
    }
}
