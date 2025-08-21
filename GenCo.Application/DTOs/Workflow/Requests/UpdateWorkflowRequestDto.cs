using GenCo.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Workflow.Requests
{
    public class UpdateWorkflowRequestDto : BaseRequestDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Definition { get; set; }
    }
}
