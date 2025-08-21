using GenCo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Domain
{
    public class Project : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public ICollection<Entity> Entities { get; set; } = [];
        public ICollection<Relation> Relations { get; set; } = [];
        public ICollection<Workflow> Workflows { get; set; } = [];
        public ICollection<UIConfig> UIConfigs { get; set; } = [];
        public ICollection<ServiceConfig> ServiceConfigs { get; set; } = [];
    }
}
