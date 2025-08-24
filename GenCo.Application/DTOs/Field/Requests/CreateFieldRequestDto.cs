using GenCo.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Field.Requests
{
    public class CreateFieldRequestDto : BaseRequestDto
    {
        public Guid EntityId { get; set; }
        public string Name { get; set; } = default!;
        public string Type { get; set; } = default!;
        public bool IsRequired { get; set; }
        public bool IsPrimaryKey { get; set; }
        public string? DefaultValue { get; set; }
    }
}
