using GenCo.Application.Exceptions;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.Entities;
using GenCo.Application.Specifications.Projects;
using GenCo.Domain.Entities;
using GenCo.Domain.Enum;

namespace GenCo.Application.BusinessRules.Entities;

public class EntityBusinessRules(
    IGenericRepository<Project> projectRepository,
    IGenericRepository<Entity> entityRepository)
    : IEntityBusinessRules
{
    // ================== Project & Entity existence ==================

    public async Task EnsureProjectExistsAsync(Guid projectId, CancellationToken cancellationToken)
    {
        var spec = new ProjectByIdSpec(projectId);
        var exists = await projectRepository.ExistsAsync(spec, cancellationToken);

        if (!exists)
            throw new BusinessRuleValidationException(
                $"Project with Id {projectId} does not exist.",
                "PROJECT_NOT_FOUND");
    }

    public async Task EnsureEntityExistsAsync(Guid entityId, CancellationToken cancellationToken)
    {
        var spec = new EntityByIdSpec(entityId);
        var exists = await entityRepository.ExistsAsync(spec, cancellationToken);

        if (!exists)
            throw new BusinessRuleValidationException(
                $"Entity with Id {entityId} does not exist.",
                "ENTITY_NOT_FOUND");
    }

    public async Task EnsureEntityBelongsToProjectAsync(Guid entityId, Guid projectId, CancellationToken cancellationToken)
    {
        var spec = new EntityByIdSpec(entityId);
        var entity = await entityRepository.FirstOrDefaultAsync(spec, cancellationToken: cancellationToken);

        if (entity is null)
            throw new BusinessRuleValidationException(
                $"Entity with Id {entityId} does not exist.",
                "ENTITY_NOT_FOUND");

        if (entity.ProjectId != projectId)
            throw new BusinessRuleValidationException(
                $"Entity with Id {entityId} does not belong to project {projectId}.",
                "ENTITY_PROJECT_MISMATCH");
    }

    // ================== Name uniqueness ==================

    public async Task EnsureEntityNameUniqueOnCreateAsync(Guid projectId, string name, CancellationToken cancellationToken)
    {
        var spec = new EntityByNameAndProjectSpec(projectId, name);
        var exists = await entityRepository.ExistsAsync(spec, cancellationToken);

        if (exists)
            throw new BusinessRuleValidationException(
                $"Entity with name '{name}' already exists in project {projectId}.",
                "ENTITY_NAME_DUPLICATED");
    }

    public async Task EnsureEntityNameUniqueOnUpdateAsync(Guid projectId, Guid entityId, string name, CancellationToken cancellationToken)
    {
        var spec = new EntityByNameAndProjectSpec(projectId, name, excludeEntityId: entityId);
        var exists = await entityRepository.ExistsAsync(spec, cancellationToken);

        if (exists)
            throw new BusinessRuleValidationException(
                $"Entity with name '{name}' already exists in project {projectId}.",
                "ENTITY_NAME_DUPLICATED");
    }

    // ================== Delete safety ==================

    public async Task EnsureEntityCanBeDeletedAsync(Guid entityId, CancellationToken cancellationToken)
    {
        var spec = new EntityByIdSpec(entityId, includeFieldsAndValidators: true);
        var entity = await entityRepository.FirstOrDefaultAsync(spec, cancellationToken: cancellationToken);

        if (entity is null)
            throw new BusinessRuleValidationException(
                $"Entity with Id {entityId} does not exist.",
                "ENTITY_NOT_FOUND");

        if (entity.Fields.Any())
            throw new BusinessRuleValidationException(
                "Cannot delete entity that still has fields.",
                "ENTITY_HAS_FIELDS");

        if (entity.FromRelations.Any() || entity.ToRelations.Any())
            throw new BusinessRuleValidationException(
                "Cannot delete entity that is referenced by relations.",
                "ENTITY_HAS_RELATIONS");
    }

    // ================== Field & Constraints ==================

    public async Task EnsureFieldNameUniqueAsync(Guid entityId, string fieldName, CancellationToken cancellationToken)
    {
        var spec = new EntityByIdSpec(entityId, includeFieldsAndValidators: true);
        var entity = await entityRepository.FirstOrDefaultAsync(spec, cancellationToken: cancellationToken);

        if (entity is null)
            throw new BusinessRuleValidationException(
                $"Entity with Id {entityId} does not exist.",
                "ENTITY_NOT_FOUND");

        if (entity.Fields.Any(f => f.ColumnName.Equals(fieldName, StringComparison.OrdinalIgnoreCase)))
            throw new BusinessRuleValidationException(
                $"Field with name '{fieldName}' already exists in entity '{entity.Name}'.",
                "FIELD_NAME_DUPLICATED");
    }

    public async Task EnsureConstraintsValidAsync(Guid entityId, CancellationToken cancellationToken)
    {
        var spec = new EntityByIdSpec(entityId, includeFieldsAndValidators: true, includeConstraints: true);
        var entity = await entityRepository.FirstOrDefaultAsync(spec, cancellationToken: cancellationToken);

        if (entity is null)
            throw new BusinessRuleValidationException(
                $"Entity with Id {entityId} does not exist.",
                "ENTITY_NOT_FOUND");

        // ⚙️ Validate Primary Key
        var primaryKeys = entity.Constraints.Where(c => c.Type == ConstraintType.PrimaryKey).ToList();
        if (primaryKeys.Count > 1)
            throw new BusinessRuleValidationException(
                "Entity cannot have more than one primary key.",
                "PK_DUPLICATED");

        if (primaryKeys.Count == 1 && !primaryKeys.First().Fields.Any())
            throw new BusinessRuleValidationException(
                "Primary key must have at least one field.",
                "PK_NO_FIELDS");

        // ⚙️ Validate Unique Constraints
        var uniqueConstraints = entity.Constraints.Where(c => c.Type == ConstraintType.UniqueKey).ToList();
        foreach (var constraint in uniqueConstraints)
        {
            if (!constraint.Fields.Any())
                throw new BusinessRuleValidationException(
                    $"Unique constraint '{constraint.ConstraintName}' must have at least one field.",
                    "UNIQUE_NO_FIELDS");
        }

        // ⚙️ Validate Foreign Keys
        var fkConstraints = entity.Constraints.Where(c => c.Type == ConstraintType.ForeignKey).ToList();
        foreach (var fk in fkConstraints)
        {
            if (!fk.Fields.Any())
                throw new BusinessRuleValidationException(
                    $"Foreign key '{fk.ConstraintName}' must have at least one local field.",
                    "FK_NO_FIELDS");

            if (fk.ReferencedEntityId is null)
                throw new BusinessRuleValidationException(
                    $"Foreign key '{fk.ConstraintName}' must reference another entity.",
                    "FK_NO_REFERENCE");
        }

        // ⚙️ Validate Check Constraints
        var checkConstraints = entity.Constraints.Where(c => c.Type == ConstraintType.Check).ToList();
        foreach (var check in checkConstraints)
        {
            if (string.IsNullOrWhiteSpace(check.Expression))
                throw new BusinessRuleValidationException(
                    $"Check constraint '{check.ConstraintName}' must have a valid expression.",
                    "CHECK_NO_EXPRESSION");
        }
    }

    // ================== Naming convention ==================

    public Task EnsureEntityNameFollowsConventionAsync(string name)
    {
        // Example rule: PascalCase (no spaces, starts uppercase)
        if (string.IsNullOrWhiteSpace(name))
            throw new BusinessRuleValidationException("Entity name cannot be empty.", "ENTITY_NAME_EMPTY");

        if (!char.IsUpper(name[0]))
            throw new BusinessRuleValidationException(
                $"Entity name '{name}' must start with an uppercase letter.",
                "ENTITY_NAME_INVALID");

        if (name.Any(ch => ch == ' '))
            throw new BusinessRuleValidationException(
                $"Entity name '{name}' cannot contain spaces.",
                "ENTITY_NAME_INVALID");

        return Task.CompletedTask;
    }
}
