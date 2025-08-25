using GenCo.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Domain.Entities
{
    public class ServiceConfig : BaseEntity
    {
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = default!;
        public string ServiceType { get; set; } = default!;
        public string? Config { get; set; }
    }
}
