using GenCo.Domain.Entities;

namespace GenCo.Application.BusinessRules.RelationJoinTables;

public interface IRelationJoinTableBusinessRules
{
    Task EnsureRelationExistsAsync(Guid relationId, CancellationToken cancellationToken);
    Task EnsureJoinTableNameUniqueAsync(Guid relationId, string joinTableName, CancellationToken cancellationToken);
    Task EnsureCanDeleteAsync(Guid joinTableId, CancellationToken cancellationToken);
    void EnsureValidKeys(RelationJoinTable joinTable);
    Task EnsureRelationTypeIsManyToManyAsync(Guid relationId, CancellationToken cancellationToken);
}