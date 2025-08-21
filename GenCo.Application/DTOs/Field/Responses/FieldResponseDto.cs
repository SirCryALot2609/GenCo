using GenCo.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Field.Responses
{
    public class FieldResponseDto : BaseResponseDto
    {
        public Guid EntityId { get; set; }
        public string Name { get; set; } = default!;
        public string DataType { get; set; } = default!;
        public bool IsNullable { get; set; }
        public bool IsPrimaryKey { get; set; }
    }
}
