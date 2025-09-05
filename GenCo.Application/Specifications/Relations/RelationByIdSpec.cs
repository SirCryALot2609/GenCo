using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;

namespace GenCo.Application.Specifications.Relations;
public class RelationByIdSpec : BaseSpecification<Relation>
{
    public RelationByIdSpec(Guid relationId, bool includeDetails = false)
        : base(r => r.Id == relationId)
    {
        // Always include Project, FromEntity, ToEntity
        AddInclude(r => r.Project);
        AddInclude(r => r.FromEntity);
        AddInclude(r => r.ToEntity);

        if (includeDetails)
        {
            // Include FieldMappings
            AddInclude(r => r.FieldMappings);

            // Include JoinTables
            AddInclude(r => r.JoinTables);
        }
    }
}