using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Relation.Responses
{
    public class RelationsByProjectIdResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime RetrievedAt { get; set; } = DateTime.UtcNow;
        public List<RelationResponseDto> Relations { get; set; } = [];
    }
}
