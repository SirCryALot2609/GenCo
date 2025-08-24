using GenCo.Application.Specifications.Common;
using GenCo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Specifications.Fields
{
    public class FieldByIdSpec : BaseSpecification<Field>
    {
        public FieldByIdSpec(Guid fieldId, bool includeValidators = false)
        : base(f => f.Id == fieldId)
        {
            if (includeValidators)
            {
                AddInclude(f => f.Validators);
            }
        }
    }
}
