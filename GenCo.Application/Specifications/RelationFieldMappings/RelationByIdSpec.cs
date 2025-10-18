using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;

namespace GenCo.Application.Specifications.RelationFieldMappings;

public class RelationByIdSpec : BaseSpecification<Relation>
{
    public RelationByIdSpec(Guid id, bool includeEntities = false)
        : base(r => r.Id == id)
    {
        if (!includeEntities) return;
        AddInclude(r => r.FromEntity);
        AddInclude(r => r.ToEntity);
        AddInclude(r => r.Project);
    }
}