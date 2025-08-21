using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Field.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Entity.Responses
{
    public class EntityResponseDto : BaseResponseDto
    {
        public int ProjectId { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public ICollection<FieldResponseDto> Fields { get; set; } = new List<FieldResponseDto>();
    }
}
