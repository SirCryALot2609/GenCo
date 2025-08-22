using GenCo.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Entity.Responses
{
    public class UpdateEntityResponseDto : BaseResponseDto
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = default!;
    }
}
