using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;

namespace GenCo.Application.Specifications.Entities;

public class EntityByIdSpec : BaseSpecification<Entity>
{
    public EntityByIdSpec(
        Guid entityId,
        bool includeFieldsAndValidators = false,
        bool includeConstraints = false,
        bool includeRelations = false)
        : base(e => e.Id == entityId)
    {
        AddInclude(e => e.Project);

        if (includeFieldsAndValidators)
        {
            AddInclude(e => e.Fields);
            AddInclude(e => e.Fields.Select(f => f.Validators));
        }

        if (includeConstraints)
        {
            AddInclude(e => e.Constraints);
            AddInclude(e => e.Constraints.Select(c => c.Fields));
        }

        if (includeRelations)
        {
            AddInclude(e => e.FromRelations);
            AddInclude(e => e.ToRelations);
        }
    }
}

