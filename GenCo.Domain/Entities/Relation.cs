using GenCo.Domain.Entities.Common;
using GenCo.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Domain.Entities
{
    public class Relation : BaseEntity
    {
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; } = default!;

        public Guid FromEntityId { get; set; }
        public virtual Entity FromEntity { get; set; } = default!;

        public Guid ToEntityId { get; set; }
        public virtual Entity ToEntity { get; set; } = default!;

        public RelationType RelationType { get; set; }
        public DeleteBehavior OnDelete { get; set; }

        public string? RelationName { get; set; }

        public virtual ICollection<RelationFieldMapping> FieldMappings { get; set; } = [];
    }
}
