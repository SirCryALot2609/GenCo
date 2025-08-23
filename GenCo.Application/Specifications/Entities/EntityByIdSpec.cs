using GenCo.Application.Specifications.Common;
using GenCo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Specifications.Entities
{
    public class EntityByIdSpec : BaseSpecification<Entity>
    {
        public EntityByIdSpec(Guid entityId, bool includeFieldsAndValidators = false)
            : base(e => e.Id == entityId)
        {
            if (includeFieldsAndValidators)
            {
                AddInclude(e => e.Fields); // load Fields
                AddInclude(e => e.Fields.Select(f => f.Validators)); // load Validators
            }

            // Optional: include Project
            AddInclude(e => e.Project);
        }
    }
}
