using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.WorkflowStep.Responses
{
    public class GetWorkflowStepsByWorkflowIdResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime RetrievedAt { get; set; } = DateTime.UtcNow;
        public IReadOnlyCollection<WorkflowStepResponseDto> Steps { get; set; } = [];
    }
}
