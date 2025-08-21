using GenCo.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Workflow.Requests
{
    public class CreateWorkflowRequestDto : BaseRequestDto
    {
        public int ProjectId { get; set; }
        public string Name { get; set; } = default!;
        public string? Definition { get; set; } // JSON hoặc XML
    }

}
