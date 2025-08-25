using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Specifications.Entities
{
    public class EntityByNameSpec : BaseSpecification<Entity>
    {
        public EntityByNameSpec(string name, Guid? projectId = null, bool includeFieldsAndValidators = false)
            : base(e => e.Name == name && (!projectId.HasValue || e.ProjectId == projectId.Value))
        {
            if (includeFieldsAndValidators)
            {
                AddInclude(e => e.Fields);
                AddInclude(e => e.Fields.Select(f => f.Validators));
            }

            AddInclude(e => e.Project);
        }
    }
}
