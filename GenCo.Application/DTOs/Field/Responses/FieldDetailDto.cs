using GenCo.Application.DTOs.Entity;
using GenCo.Application.DTOs.FieldValidator;
using GenCo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Field.Responses
{
    public class FieldDetailDto : FieldBaseDto
    {
        public EntityBaseDto Entity { get; set; } = default!;
        public ICollection<FieldValidatorBaseDto> Validators { get; set; } = [];
    }
}
