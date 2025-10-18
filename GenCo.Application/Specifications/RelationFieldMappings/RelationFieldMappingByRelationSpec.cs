using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;

namespace GenCo.Application.Specifications.RelationFieldMappings;

public class RelationFieldMappingByRelationSpec : BaseSpecification<RelationFieldMapping>
{
    public RelationFieldMappingByRelationSpec(Guid relationId, bool includeFields = false)
        : base(rfm => rfm.RelationId == relationId)
    {
        if (!includeFields) return;
        AddInclude(rfm => rfm.FromField);
        AddInclude(rfm => rfm.ToField);
    }
}
