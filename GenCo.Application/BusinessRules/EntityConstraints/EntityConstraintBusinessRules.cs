using GenCo.Application.Exceptions;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.Entities;
using GenCo.Application.Specifications.EntityConstraints;
using GenCo.Domain.Entities;
using GenCo.Domain.Enum;

namespace GenCo.Application.BusinessRules.EntityConstraints;

public class EntityConstraintBusinessRules(
    IGenericRepository<Entity> entityRepository,
    IGenericRepository<EntityConstraint> constraintRepository)
    : IEntityConstraintBusinessRules
{
    // ================== EXISTENCE ==================

    public async Task EnsureEntityExistsAsync(Guid entityId, CancellationToken cancellationToken)
    {
        var spec = new EntityByIdSpec(entityId);
        if (!await entityRepository.ExistsAsync(spec, cancellationToken))
            throw new BusinessRuleValidationException($"Entity with Id {entityId} does not exist.", "ENTITY_NOT_FOUND");
    }

    public async Task EnsureConstraintExistsAsync(Guid constraintId, CancellationToken cancellationToken)
    {
        var spec = new EntityConstraintByIdSpec(constraintId);
        if (!await constraintRepository.ExistsAsync(spec, cancellationToken))
            throw new BusinessRuleValidationException($"Constraint with Id {constraintId} does not exist.", "CONSTRAINT_NOT_FOUND");
    }

    public async Task EnsureConstraintBelongsToEntityAsync(Guid constraintId, Guid entityId, CancellationToken cancellationToken)
    {
        var spec = new EntityConstraintByIdSpec(constraintId);
        var constraint = await constraintRepository.FirstOrDefaultAsync(spec, cancellationToken: cancellationToken);

        if (constraint is null)
            throw new BusinessRuleValidationException($"Constraint with Id {constraintId} does not exist.", "CONSTRAINT_NOT_FOUND");

        if (constraint.EntityId != entityId)
            throw new BusinessRuleValidationException($"Constraint with Id {constraintId} does not belong to entity {entityId}.", "CONSTRAINT_ENTITY_MISMATCH");
    }

    // ================== NAME UNIQUENESS ==================

    public async Task EnsureConstraintNameUniqueOnCreateAsync(Guid entityId, string? constraintName, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(constraintName))
            return;

        var spec = new EntityConstraintByNameAndEntitySpec(entityId, constraintName);
        if (await constraintRepository.ExistsAsync(spec, cancellationToken))
            throw new BusinessRuleValidationException($"Constraint name '{constraintName}' already exists in entity {entityId}.", "CONSTRAINT_NAME_DUPLICATED");
    }

    public async Task EnsureConstraintNameUniqueOnUpdateAsync(Guid entityId, Guid constraintId, string? constraintName, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(constraintName))
            return;

        var spec = new EntityConstraintByNameAndEntitySpec(entityId, constraintName, excludeConstraintId: constraintId);
        if (await constraintRepository.ExistsAsync(spec, cancellationToken))
            throw new BusinessRuleValidationException($"Constraint name '{constraintName}' already exists in entity {entityId}.", "CONSTRAINT_NAME_DUPLICATED");
    }

    // ================== DELETE SAFETY ==================

    public async Task EnsureConstraintCanBeDeletedAsync(Guid constraintId, CancellationToken cancellationToken)
    {
        var spec = new EntityConstraintByIdSpec(constraintId, includeDetails: true);
        var constraint = await constraintRepository.FirstOrDefaultAsync(spec, cancellationToken: cancellationToken);

        if (constraint is null)
            throw new BusinessRuleValidationException($"Constraint with Id {constraintId} does not exist.", "CONSTRAINT_NOT_FOUND");

        if (constraint.Fields.Any())
            throw new BusinessRuleValidationException("Cannot delete constraint that still has assigned fields.", "CONSTRAINT_HAS_FIELDS");
    }

    // ================== MAIN VALIDATION ENTRY ==================

    public async Task EnsureConstraintValidAsync(EntityConstraint constraint, CancellationToken cancellationToken)
    {
        await EnsureEntityExistsAsync(constraint.EntityId, cancellationToken);

        switch (constraint.Type)
        {
            case ConstraintType.PrimaryKey:
                await EnsurePrimaryKeyValidAsync(constraint, cancellationToken);
                break;

            case ConstraintType.UniqueKey:
                await EnsureUniqueConstraintValidAsync(constraint, cancellationToken);
                break;

            case ConstraintType.Index:
                await EnsureIndexConstraintValidAsync(constraint, cancellationToken);
                break;

            case ConstraintType.Check:
                await EnsureCheckConstraintValidAsync(constraint, cancellationToken);
                break;

            default:
                throw new BusinessRuleValidationException($"Unsupported constraint type: {constraint.Type}", "CONSTRAINT_TYPE_INVALID");
        }
    }

    // ================== PRIMARY KEY ==================

    public async Task EnsurePrimaryKeyValidAsync(EntityConstraint constraint, CancellationToken cancellationToken)
    {
        var entitySpec = new EntityByIdSpec(constraint.EntityId, includeFieldsAndValidators: true);
        var entity = await entityRepository.FirstOrDefaultAsync(entitySpec, cancellationToken: cancellationToken);

        if (entity is null)
            throw new BusinessRuleValidationException("Entity not found.", "ENTITY_NOT_FOUND");

        if (!constraint.Fields.Any())
            throw new BusinessRuleValidationException("Primary key must have at least one field.", "PK_NO_FIELDS");

        var existingPkSpec = new EntityConstraintsByTypeSpec(constraint.EntityId, ConstraintType.PrimaryKey, excludeConstraintId: constraint.Id);
        if (await constraintRepository.ExistsAsync(existingPkSpec, cancellationToken))
            throw new BusinessRuleValidationException("Entity already has a primary key.", "PK_DUPLICATED");
    }

    // ================== UNIQUE KEY ==================

    public async Task EnsureUniqueConstraintValidAsync(EntityConstraint constraint, CancellationToken cancellationToken)
    {
        if (!constraint.Fields.Any())
            throw new BusinessRuleValidationException("Unique constraint must reference at least one field.", "UNIQUE_NO_FIELDS");

        var entitySpec = new EntityByIdSpec(constraint.EntityId, includeFieldsAndValidators: true);
        var entity = await entityRepository.FirstOrDefaultAsync(entitySpec, cancellationToken: cancellationToken);

        if (entity is null)
            throw new BusinessRuleValidationException("Entity not found.", "ENTITY_NOT_FOUND");

        // Kiểm tra trùng field set với PK hoặc Unique khác
        var pkSpec = new EntityConstraintsByTypeSpec(constraint.EntityId, ConstraintType.PrimaryKey);
        var pkConstraints = await constraintRepository.FindAsync(pkSpec, cancellationToken: cancellationToken);

        if (pkConstraints.Any(pk => AreFieldSetsEqual(pk.Fields, constraint.Fields)))
            throw new BusinessRuleValidationException("Unique constraint duplicates field set of primary key.", "UNIQUE_DUPLICATES_PK");

        var uniqueSpec = new EntityConstraintsByTypeSpec(constraint.EntityId, ConstraintType.UniqueKey, excludeConstraintId: constraint.Id);
        var existingUniques = await constraintRepository.FindAsync(uniqueSpec, cancellationToken: cancellationToken);

        if (existingUniques.Any(u => AreFieldSetsEqual(u.Fields, constraint.Fields)))
            throw new BusinessRuleValidationException("Duplicate unique constraint with same fields exists.", "UNIQUE_DUPLICATED");
    }

    // ================== INDEX ==================

    public async Task EnsureIndexConstraintValidAsync(EntityConstraint constraint, CancellationToken cancellationToken)
    {
        var entitySpec = new EntityByIdSpec(constraint.EntityId, includeFieldsAndValidators: true);
        var entity = await entityRepository.FirstOrDefaultAsync(entitySpec, cancellationToken: cancellationToken);

        if (entity is null)
            throw new BusinessRuleValidationException($"Entity with Id {constraint.EntityId} does not exist.", "ENTITY_NOT_FOUND");

        if (!constraint.Fields.Any())
            throw new BusinessRuleValidationException("Index constraint must include at least one field.", "INDEX_NO_FIELDS");

        var uniqueConstraints = await constraintRepository.FindAsync(
            new EntityConstraintsByTypeSpec(constraint.EntityId, ConstraintType.UniqueKey),
            cancellationToken: cancellationToken
        );

        var pkConstraints = await constraintRepository.FindAsync(
            new EntityConstraintsByTypeSpec(constraint.EntityId, ConstraintType.PrimaryKey),
            cancellationToken: cancellationToken
        );

        if (uniqueConstraints.Any(u => AreFieldSetsEqual(u.Fields, constraint.Fields)) ||
            pkConstraints.Any(pk => AreFieldSetsEqual(pk.Fields, constraint.Fields)))
        {
            throw new BusinessRuleValidationException(
                "Index constraint duplicates field set of an existing UniqueKey or PrimaryKey constraint.",
                "INDEX_DUPLICATES_EXISTING");
        }

        var indexSpec = new EntityConstraintsByTypeSpec(constraint.EntityId, ConstraintType.Index, excludeConstraintId: constraint.Id);
        var existingIndexes = await constraintRepository.FindAsync(indexSpec, cancellationToken: cancellationToken);

        if (existingIndexes.Any(idx => AreFieldSetsEqual(idx.Fields, constraint.Fields)))
            throw new BusinessRuleValidationException("Another index with the same field set already exists in this entity.", "INDEX_DUPLICATED");
    }

    // ================== CHECK CONSTRAINT ==================

    public Task EnsureCheckConstraintValidAsync(EntityConstraint constraint, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(constraint.Expression))
            throw new BusinessRuleValidationException("Check constraint must have an expression.", "CHECK_NO_EXPRESSION");

        // Optionally: parse or validate SQL-like syntax here
        return Task.CompletedTask;
    }

    // ================== FOREIGN KEY ==================

    public async Task EnsureForeignKeyValidAsync(EntityConstraint constraint, CancellationToken cancellationToken)
    {
        if (constraint.ReferencedEntityId is null)
            throw new BusinessRuleValidationException("Foreign key must reference another entity.", "FK_NO_REFERENCE");

        // Kiểm tra entity được tham chiếu có tồn tại
        var referencedEntity = await entityRepository.FirstOrDefaultAsync(
            new EntityByIdSpec(constraint.ReferencedEntityId.Value, includeFieldsAndValidators: true),
            cancellationToken: cancellationToken
        );

        if (referencedEntity is null)
            throw new BusinessRuleValidationException(
                $"Referenced entity {constraint.ReferencedEntityId} does not exist.",
                "FK_REFERENCED_ENTITY_NOT_FOUND");

        if (constraint.Fields.Count == 0)
            throw new BusinessRuleValidationException("Foreign key must include at least one field.", "FK_NO_FIELDS");

        if (constraint.ReferencedFields.Count == 0)
            throw new BusinessRuleValidationException("Foreign key must specify referenced fields.", "FK_NO_REFERENCED_FIELDS");

        if (constraint.Fields.Count != constraint.ReferencedFields.Count)
            throw new BusinessRuleValidationException("Foreign key field count must match referenced field count.", "FK_FIELD_COUNT_MISMATCH");

        // Có thể thêm kiểm tra type giữa các field tương ứng
    }


    // ================== HELPER ==================

    private static bool AreFieldSetsEqual(ICollection<EntityConstraintField> a, ICollection<EntityConstraintField> b)
    {
        if (a.Count != b.Count)
            return false;

        var aIds = a.Select(f => f.FieldId).OrderBy(id => id).ToList();
        var bIds = b.Select(f => f.FieldId).OrderBy(id => id).ToList();

        return aIds.SequenceEqual(bIds);
    }

    // ================== NAMING CONVENTION ==================

    public Task EnsureConstraintNameFollowsConventionAsync(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Task.CompletedTask;

        var validPrefixes = new[] { "PK_", "FK_", "UQ_", "CK_", "IX_" };
        if (!validPrefixes.Any(p => name.StartsWith(p, StringComparison.OrdinalIgnoreCase)))
            throw new BusinessRuleValidationException(
                $"Constraint name '{name}' must start with one of: {string.Join(", ", validPrefixes)}.",
                "CONSTRAINT_NAME_INVALID");

        return Task.CompletedTask;
    }
}
