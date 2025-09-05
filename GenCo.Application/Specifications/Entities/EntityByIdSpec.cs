using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;

namespace GenCo.Application.Specifications.Entities;

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
        AddInclude(e => e.Project);
    }
}