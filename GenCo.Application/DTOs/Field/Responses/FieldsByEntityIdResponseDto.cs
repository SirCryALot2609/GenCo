using GenCo.Application.DTOs.Entity.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Field.Responses
{
    public class FieldsByEntityIdResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime RetrievedAt { get; set; } = DateTime.UtcNow;
        public List<FieldResponseDto> Fields { get; set; } = [];
    }
}
