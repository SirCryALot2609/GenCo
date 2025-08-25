using GenCo.Domain.Entities.Common;
using GenCo.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Domain.Entities
{
    public class EntityConstraint : BaseEntity
    {
        public Guid EntityId { get; set; }
        public virtual Entity Entity { get; set; } = default!;

        public ConstraintType Type { get; set; }
        public string? ConstraintName { get; set; }
        public string? Expression { get; set; }

        public virtual ICollection<EntityConstraintField> Fields { get; set; } = [];
    }
}
