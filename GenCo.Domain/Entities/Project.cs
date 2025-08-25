using GenCo.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public virtual ICollection<Entity> Entities { get; set; } = [];
        public virtual ICollection<Relation> Relations { get; set; } = [];
        public virtual ICollection<Workflow> Workflows { get; set; } = [];
        public virtual ICollection<UIConfig> UIConfigs { get; set; } = [];
        public virtual ICollection<ServiceConfig> ServiceConfigs { get; set; } = [];
    }
}
