using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;

namespace GenCo.Application.Specifications.Entities;

public class EntityByNameAndProjectSpec : BaseSpecification<Entity>
{
    public EntityByNameAndProjectSpec(
        Guid projectId,
        string name,
        Guid? excludeEntityId = null,
        bool includeFieldsAndValidators = false)
        : base(e => 
            e.ProjectId == projectId && 
            e.Name == name &&
            (excludeEntityId == null || e.Id != excludeEntityId))
    {
        if (includeFieldsAndValidators)
        {
            AddInclude(e => e.Fields);
            AddInclude(e => e.Fields.Select(f => f.Validators));
        }

        AddInclude(e => e.Project);
    }
}

