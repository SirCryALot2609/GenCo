using GenCo.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Field
{
    public class FieldBaseDto : AuditableDto
    {
        public Guid EntityId { get; set; }
        public string ColumnName { get; set; } = default!;
        public string? Label { get; set; }
        public int Ordinal { get; set; }
    }
}
