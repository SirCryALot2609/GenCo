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
        public string ColumnName { get; set; } = default!;
        public string? Label { get; set; }
        public string DataType { get; set; } = default!;
        public bool IsNullable { get; set; }
        public int? MaxLength { get; set; }
        public int Ordinal { get; set; }
    }
}
