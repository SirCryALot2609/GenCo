using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;

namespace GenCo.Application.Specifications.RelationJoinTables;

public class RelationJoinTableByIdSpec : BaseSpecification<RelationJoinTable>
{
    public RelationJoinTableByIdSpec(Guid relationJoinTableId, bool includeRelation = false)
        : base(rjt => rjt.Id == relationJoinTableId)
    {
        if (includeRelation)
        {
            AddInclude(rjt => rjt.Relation);
        }
    }
}