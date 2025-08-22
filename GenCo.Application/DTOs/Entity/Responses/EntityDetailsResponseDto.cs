using GenCo.Application.DTOs.Field.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Entity.Responses
{
    public class EntityDetailsResponseDto : EntityResponseDto
    {
        public ICollection<FieldResponseDto>? Fields { get; set; }
    }
}
