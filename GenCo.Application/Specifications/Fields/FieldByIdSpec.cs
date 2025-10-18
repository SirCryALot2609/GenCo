using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;

namespace GenCo.Application.Specifications.Fields;

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