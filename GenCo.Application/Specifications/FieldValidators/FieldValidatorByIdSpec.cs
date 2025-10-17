using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;

namespace GenCo.Application.Specifications.FieldValidators;

public class FieldValidatorByIdSpec : BaseSpecification<FieldValidator>
{
    public FieldValidatorByIdSpec(Guid validatorId, bool includeField = false)
        : base(v => v.Id == validatorId)
    {
        if (includeField)
            AddInclude(v => v.Field);
    }
}
