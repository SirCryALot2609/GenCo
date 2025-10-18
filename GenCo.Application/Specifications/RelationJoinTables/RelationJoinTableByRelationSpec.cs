using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;

namespace GenCo.Application.Specifications.RelationJoinTables;

public class RelationJoinTableByRelationSpec : BaseSpecification<RelationJoinTable>
{
    public RelationJoinTableByRelationSpec(Guid relationId)
        : base(jt => jt.RelationId == relationId)
    {
        AddInclude(jt => jt.Relation);
    }
}
