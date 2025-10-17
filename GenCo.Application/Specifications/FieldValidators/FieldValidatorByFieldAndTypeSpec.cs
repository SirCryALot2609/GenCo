using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;
using GenCo.Domain.Enum;

namespace GenCo.Application.Specifications.FieldValidators;

public class FieldValidatorByFieldAndTypeSpec : BaseSpecification<FieldValidator>
{
    public FieldValidatorByFieldAndTypeSpec(Guid fieldId, ValidatorType type, Guid? excludeValidatorId = null)
        : base(v =>
            v.FieldId == fieldId &&
            v.Type == type &&
            (!excludeValidatorId.HasValue || v.Id != excludeValidatorId.Value))
    {
        AddInclude(v => v.Field);
    }
}