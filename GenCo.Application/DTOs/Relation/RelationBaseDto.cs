using GenCo.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.DTOs.Relation
{
    public class RelationBaseDto : AuditableDto
    {
        public Guid FromEntityId { get; set; }
        public Guid ToEntityId { get; set; }
        public string RelationType { get; set; } = default!;
    }
}
