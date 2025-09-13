using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;

namespace GenCo.Application.Specifications.RelationJoinTables;

public class RelationJoinTableByNameSpec : BaseSpecification<RelationJoinTable>
{
    public RelationJoinTableByNameSpec(Guid relationId, string joinTableName)
        : base(r => r.RelationId == relationId && r.JoinTableName == joinTableName)
    {
        AddInclude(r => r.Relation);
    }
}
