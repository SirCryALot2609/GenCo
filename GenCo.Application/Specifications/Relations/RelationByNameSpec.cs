using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;

namespace GenCo.Application.Specifications.Relations;

public class RelationByNameSpec : BaseSpecification<Relation>
{
    public RelationByNameSpec(string name, Guid? projectId = null, bool includeDetails = false)
        : base(r => r.RelationName == name && (!projectId.HasValue || r.ProjectId == projectId.Value))
    {
        if (includeDetails)
        {
            AddInclude(r => r.Project);
            AddInclude(r => r.FromEntity);
            AddInclude(r => r.ToEntity);
            AddInclude(r => r.FieldMappings);
            AddInclude(r => r.JoinTables);
        }
    }
}
