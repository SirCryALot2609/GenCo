using GenCo.Application.Exceptions;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.Fields;
using GenCo.Application.Specifications.RelationFieldMappings;
using GenCo.Application.Specifications.RelationJoinTables;
using GenCo.Domain.Entities;
using RelationByIdSpec = GenCo.Application.Specifications.Relations.RelationByIdSpec;

namespace GenCo.Application.BusinessRules.RelationFieldMappings;

public class RelationFieldMappingBusinessRules(
    IGenericRepository<Relation> relationRepository,
    IGenericRepository<Field> fieldRepository,
    IGenericRepository<RelationFieldMapping> mappingRepository,
    IGenericRepository<RelationJoinTable> joinTableRepository)
    : IRelationFieldMappingBusinessRules
{
    public async Task EnsureRelationExistsAsync(Guid relationId, CancellationToken cancellationToken)
    {
        var spec = new RelationByIdSpec(relationId);
        var exists = await relationRepository.ExistsAsync(spec, cancellationToken);

        if (!exists)
            throw new BusinessRuleValidationException(
                $"Relation with Id {relationId} does not exist.",
                "RELATION_NOT_FOUND");
    }

    public async Task EnsureFieldsExistAsync(Guid fromFieldId, Guid toFieldId, CancellationToken cancellationToken)
    {
        var fromSpec = new FieldByIdSpec(fromFieldId);
        var toSpec = new FieldByIdSpec(toFieldId);

        var fromExists = await fieldRepository.ExistsAsync(fromSpec, cancellationToken);
        var toExists = await fieldRepository.ExistsAsync(toSpec, cancellationToken);

        if (!fromExists)
            throw new BusinessRuleValidationException(
                $"FromField with Id {fromFieldId} does not exist.",
                "FROM_FIELD_NOT_FOUND");

        if (!toExists)
            throw new BusinessRuleValidationException(
                $"ToField with Id {toFieldId} does not exist.",
                "TO_FIELD_NOT_FOUND");
    }

    public async Task EnsureFieldsBelongToCorrectEntitiesAsync(Guid relationId, Guid fromFieldId, Guid toFieldId, CancellationToken cancellationToken)
    {
        var relationSpec = new RelationByIdSpec(relationId);
        var relation = await relationRepository.FirstOrDefaultAsync(relationSpec, cancellationToken: cancellationToken);

        if (relation is null)
            throw new BusinessRuleValidationException(
                $"Relation with Id {relationId} does not exist.",
                "RELATION_NOT_FOUND");

        var fromFieldSpec = new FieldByIdSpec(fromFieldId);
        var fromField = await fieldRepository.FirstOrDefaultAsync(fromFieldSpec, cancellationToken: cancellationToken);

        var toFieldSpec = new FieldByIdSpec(toFieldId);
        var toField = await fieldRepository.FirstOrDefaultAsync(toFieldSpec, cancellationToken: cancellationToken);

        if (fromField is null || toField is null)
            throw new BusinessRuleValidationException(
                "One or both fields do not exist.",
                "FIELD_NOT_FOUND");

        if (fromField.EntityId != relation.FromEntityId)
            throw new BusinessRuleValidationException(
                $"FromField does not belong to the source entity of Relation {relationId}.",
                "FROM_FIELD_ENTITY_MISMATCH");

        if (toField.EntityId != relation.ToEntityId)
            throw new BusinessRuleValidationException(
                $"ToField does not belong to the target entity of Relation {relationId}.",
                "TO_FIELD_ENTITY_MISMATCH");
    }

    public async Task EnsureNoDuplicateMappingAsync(Guid relationId, Guid fromFieldId, Guid toFieldId, CancellationToken cancellationToken)
    {
        var spec = new RelationFieldMappingByKeysSpec(relationId, fromFieldId, toFieldId);
        var exists = await mappingRepository.ExistsAsync(spec, cancellationToken);

        if (exists)
            throw new BusinessRuleValidationException(
                $"A mapping already exists for relation {relationId} between fields {fromFieldId} and {toFieldId}.",
                "MAPPING_DUPLICATED");
    }

    public async Task EnsureFieldTypesCompatibleAsync(Guid fromFieldId, Guid toFieldId, CancellationToken cancellationToken)
    {
        var fromSpec = new FieldByIdSpec(fromFieldId);
        var toSpec = new FieldByIdSpec(toFieldId);

        var fromField = await fieldRepository.FirstOrDefaultAsync(fromSpec, cancellationToken: cancellationToken);
        var toField = await fieldRepository.FirstOrDefaultAsync(toSpec, cancellationToken: cancellationToken);

        if (fromField is null || toField is null)
            throw new BusinessRuleValidationException("Fields must exist to compare types.", "FIELD_NOT_FOUND");

        if (fromField.Id == toField.Id)
            throw new BusinessRuleValidationException("Cannot map a field to itself.", "FIELD_SELF_MAPPING");

        if (!string.Equals(fromField.Type, toField.Type, StringComparison.OrdinalIgnoreCase))
            throw new BusinessRuleValidationException(
                $"Incompatible types: {fromField.Type} → {toField.Type}.",
                "FIELD_TYPE_INCOMPATIBLE");
    }
    
    public async Task EnsureMappingCanBeDeletedAsync(Guid mappingId, CancellationToken cancellationToken)
    {
        // ✅ Lấy đầy đủ thông tin Mapping
        var spec = new RelationFieldMappingByIdSpec(mappingId, includeFieldsAndValidators: true);
        var mapping = await mappingRepository.FirstOrDefaultAsync(spec, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (mapping is null)
            throw new BusinessRuleValidationException(
                $"RelationFieldMapping with Id {mappingId} does not exist.",
                "MAPPING_NOT_FOUND");

        // ✅ Kiểm tra Relation tồn tại
        if (mapping.Relation is null)
            throw new BusinessRuleValidationException(
                "Cannot delete mapping because its parent relation does not exist.",
                "RELATION_NOT_FOUND");

        // ✅ Kiểm tra Field tồn tại
        if (mapping.FromField is null || mapping.ToField is null)
            throw new BusinessRuleValidationException(
                "Cannot delete mapping because one of the fields no longer exists.",
                "FIELD_NOT_FOUND");

        // ✅ Kiểm tra xem mapping có đang được dùng trong RelationJoinTable không
        var joinTableSpec = new RelationJoinTableByRelationSpec(mapping.RelationId);
        var joinTable = await joinTableRepository.FirstOrDefaultAsync(joinTableSpec, cancellationToken : cancellationToken);

        if (joinTable is not null)
        {
            throw new BusinessRuleValidationException(
                "Cannot delete mapping because it is still referenced in a join table.",
                "MAPPING_REFERENCED_IN_JOIN_TABLE");
        }

        // ✅ (Optional) Nếu bạn muốn check thêm trạng thái Relation bị khóa
        // if (mapping.Relation.IsLocked)
        //     throw new BusinessRuleValidationException(
        //         "Cannot delete mapping because its parent relation is locked.",
        //         "RELATION_LOCKED");
    }
}

