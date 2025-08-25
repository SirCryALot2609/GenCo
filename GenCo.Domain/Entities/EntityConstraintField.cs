using GenCo.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Domain.Entities
{
    public class EntityConstraintField : BaseEntity
    {
        public Guid ConstraintId { get; set; }
        public virtual EntityConstraint Constraint { get; set; } = default!;

        public Guid FieldId { get; set; }
        public virtual Field Field { get; set; } = default!;
    }
}
