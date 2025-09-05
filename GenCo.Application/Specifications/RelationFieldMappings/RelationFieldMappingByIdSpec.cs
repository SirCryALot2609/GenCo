using GenCo.Application.Specifications.Common;
using GenCo.Domain.Entities;

namespace GenCo.Application.Specifications.RelationFieldMappings;

public class RelationFieldMappingByIdSpec : BaseSpecification<RelationFieldMapping>
{
    public RelationFieldMappingByIdSpec(Guid id, bool includeFieldsAndValidators = false)
        : base(rfm => rfm.Id == id)
    {
        if (!includeFieldsAndValidators) return;
        AddInclude(rfm => rfm.Relation);
        AddInclude(rfm => rfm.FromField);
        AddInclude(rfm => rfm.ToField);
    }
}
