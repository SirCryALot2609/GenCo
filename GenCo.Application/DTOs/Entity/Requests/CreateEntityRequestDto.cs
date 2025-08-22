using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Field.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Entity.Requests
{
    public class CreateEntityRequestDto : BaseRequestDto
    {
        public Guid ProjectId { get; set; }  
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public ICollection<CreateFieldRequestDto>? Fields { get; set; }
    }
}
