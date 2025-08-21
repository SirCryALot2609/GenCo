using GenCo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Domain
{
    public class Relation : BaseEntity
    {
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = default!;
        public Guid FromEntityId { get; set; }
        public Entity FromEntity { get; set; } = default!;
        public Guid ToEntityId { get; set; }
        public Entity ToEntity { get; set; } = default!;
        public string RelationType { get; set; } = default!;
    }
}
