using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;

namespace GenCo.Application.Specifications.RelationFieldMappings;

public class RelationFieldMappingByKeysSpec : BaseSpecification<RelationFieldMapping>
{
    public RelationFieldMappingByKeysSpec(Guid relationId, Guid fromFieldId, Guid toFieldId)
        : base(r => r.RelationId == relationId 
                    && r.FromFieldId == fromFieldId 
                    && r.ToFieldId == toFieldId)
    {
        AddInclude(r => r.Relation);
        AddInclude(r => r.FromField);
        AddInclude(r => r.ToField);
    }
}
