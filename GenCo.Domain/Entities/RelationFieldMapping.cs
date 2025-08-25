using GenCo.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Domain.Entities
{
    public class RelationFieldMapping : BaseEntity
    {
        public Guid RelationId { get; set; }
        public virtual Relation Relation { get; set; } = default!;

        public Guid FromFieldId { get; set; }
        public virtual Field FromField { get; set; } = default!;

        public Guid ToFieldId { get; set; }
        public virtual Field ToField { get; set; } = default!;
    }
}
