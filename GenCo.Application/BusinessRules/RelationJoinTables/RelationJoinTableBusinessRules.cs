using GenCo.Application.Exceptions;
using GenCo.Application.Persistence.Contracts;
using GenCo.Application.Specifications.RelationJoinTables;
using GenCo.Domain.Entities;
using GenCo.Domain.Enum;

namespace GenCo.Application.BusinessRules.RelationJoinTables;

public class RelationJoinTableBusinessRules(
    IRelationRepository relationRepository,
    IRelationJoinTableRepository joinTableRepository)
    : IRelationJoinTableBusinessRules
{
    // 1️⃣ Kiểm tra Relation có tồn tại không
    public async Task EnsureRelationExistsAsync(Guid relationId, CancellationToken cancellationToken)
    {
        var relation = await relationRepository.GetByIdAsync(relationId, cancellationToken: cancellationToken);
        if (relation is null)
            throw new BusinessRuleValidationException(
                $"Relation with id {relationId} does not exist.",
                "RELATION_NOT_FOUND");
    }

    // 2️⃣ Đảm bảo JoinTableName là duy nhất trong Relation
    public async Task EnsureJoinTableNameUniqueAsync(Guid relationId, string joinTableName, CancellationToken cancellationToken)
    {
        var spec = new RelationJoinTableByNameSpec(relationId, joinTableName);
        var existing = await joinTableRepository.FirstOrDefaultAsync(spec, cancellationToken : cancellationToken);

        if (existing is not null)
            throw new BusinessRuleValidationException(
                $"JoinTable '{joinTableName}' already exists for this relation.",
                "JOINTABLE_NAME_DUPLICATED");
    }

    // 3️⃣ Kiểm tra có thể xóa JoinTable hay không
    public async Task EnsureCanDeleteAsync(Guid joinTableId, CancellationToken cancellationToken)
    {
        var spec = new RelationJoinTableByIdSpec(joinTableId, includeRelation: true);
        var joinTable = await joinTableRepository.FirstOrDefaultAsync(spec, cancellationToken : cancellationToken);

        if (joinTable is null)
            throw new BusinessRuleValidationException(
                $"JoinTable with id {joinTableId} not found.",
                "JOINTABLE_NOT_FOUND");

        var relation = joinTable.Relation;
        if (relation is null)
            throw new BusinessRuleValidationException(
                "JoinTable is not associated with any relation.",
                "RELATION_NOT_FOUND");

        // Chỉ ManyToMany mới được xóa JoinTable
        if (relation.RelationType != RelationType.ManyToMany)
            throw new BusinessRuleValidationException(
                "Only Many-to-Many relations can have join tables deleted.",
                "INVALID_RELATION_TYPE");
    }

    // 4️⃣ Kiểm tra LeftKey / RightKey hợp lệ
    public void EnsureValidKeys(RelationJoinTable joinTable)
    {
        if (string.IsNullOrWhiteSpace(joinTable.LeftKey) || string.IsNullOrWhiteSpace(joinTable.RightKey))
            throw new BusinessRuleValidationException(
                "LeftKey and RightKey must not be empty.",
                "INVALID_JOIN_KEYS");

        if (string.Equals(joinTable.LeftKey, joinTable.RightKey, StringComparison.OrdinalIgnoreCase))
            throw new BusinessRuleValidationException(
                "LeftKey and RightKey cannot be the same.",
                "DUPLICATE_JOIN_KEYS");
    }

    // 5️⃣ Đảm bảo chỉ Many-to-Many mới được tạo JoinTable
    public async Task EnsureRelationTypeIsManyToManyAsync(Guid relationId, CancellationToken cancellationToken)
    {
        var relation = await relationRepository.GetByIdAsync(relationId, cancellationToken: cancellationToken);
        if (relation is null)
            throw new BusinessRuleValidationException(
                $"Relation with id {relationId} does not exist.",
                "RELATION_NOT_FOUND");

        if (relation.RelationType != RelationType.ManyToMany)
            throw new BusinessRuleValidationException(
                "JoinTable can only be created for Many-to-Many relations.",
                "INVALID_RELATION_TYPE");
    }
}
