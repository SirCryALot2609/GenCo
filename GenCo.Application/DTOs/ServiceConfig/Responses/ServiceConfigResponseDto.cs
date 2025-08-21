using GenCo.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.ServiceConfig.Responses
{
    public class ServiceConfigResponseDto : BaseResponseDto
    {
        public int ProjectId { get; set; }
        public string Name { get; set; } = default!;
        public string? ConfigJson { get; set; }
    }
}
