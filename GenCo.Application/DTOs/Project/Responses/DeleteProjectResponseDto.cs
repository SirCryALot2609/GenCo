using GenCo.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Project.Responses
{
    public class DeleteProjectResponseDto : BaseResponseDto
    {
        public bool Success { get; set; }
    }
}
