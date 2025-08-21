using GenCo.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.WorkflowStep.Requests
{
    public class CreateWorkflowStepRequestDto : BaseRequestDto
    {
        public Guid WorkflowId { get; set; }
        public string StepType { get; set; } = default!;
        public string? Config { get; set; }
        public int Order { get; set; }
    }
}
