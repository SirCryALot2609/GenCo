using GenCo.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Entity
{
    public class EntityBaseDto : AuditableDto
    {
        public string Name { get; set; } = default!;
        public string? Label { get; set; }
    }
}
