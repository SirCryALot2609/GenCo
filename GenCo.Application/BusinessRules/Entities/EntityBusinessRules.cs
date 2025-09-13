using GenCo.Application.Exceptions;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.Entities;
using GenCo.Application.Specifications.Projects;
using GenCo.Domain.Entities;

namespace GenCo.Application.BusinessRules.Entities;

public class EntityBusinessRules : IEntityBusinessRules
{
    private readonly IGenericRepository<Project> _projectRepository;
    private readonly IGenericRepository<Entity> _entityRepository;

    public EntityBusinessRules(
        IGenericRepository<Project> projectRepository,
        IGenericRepository<Entity> entityRepository)
    {
        _projectRepository = projectRepository;
        _entityRepository = entityRepository;
    }

    // ================== Project & Entity existence ==================

    public async Task EnsureProjectExistsAsync(Guid projectId, CancellationToken cancellationToken)
    {
        var spec = new ProjectByIdSpec(projectId);
        var exists = await _projectRepository.ExistsAsync(spec, cancellationToken);

        if (!exists)
            throw new BusinessRuleValidationException(
                $"Project with Id {projectId} does not exist.",
                "PROJECT_NOT_FOUND");
    }

    public async Task EnsureEntityExistsAsync(Guid entityId, CancellationToken cancellationToken)
    {
        var spec = new EntityByIdSpec(entityId);
        var exists = await _entityRepository.ExistsAsync(spec, cancellationToken);

        if (!exists)
            throw new BusinessRuleValidationException(
                $"Entity with Id {entityId} does not exist.",
                "ENTITY_NOT_FOUND");
    }

    public async Task EnsureEntityBelongsToProjectAsync(Guid entityId, Guid projectId, CancellationToken cancellationToken)
    {
        var spec = new EntityByIdSpec(entityId);
        var entity = await _entityRepository.FirstOrDefaultAsync(
            spec,
            asNoTracking: false, // cần tracking để load fields & relations
            cancellationToken: cancellationToken);

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
        var exists = await _entityRepository.ExistsAsync(spec, cancellationToken);

        if (exists)
            throw new BusinessRuleValidationException(
                $"Entity with name '{name}' already exists in project {projectId}.",
                "ENTITY_NAME_DUPLICATED");
    }

    public async Task EnsureEntityNameUniqueOnUpdateAsync(Guid projectId, Guid entityId, string name, CancellationToken cancellationToken)
    {
        var spec = new EntityByNameAndProjectSpec(projectId, name, excludeEntityId: entityId);
        var exists = await _entityRepository.ExistsAsync(spec, cancellationToken);

        if (exists)
            throw new BusinessRuleValidationException(
                $"Entity with name '{name}' already exists in project {projectId}.",
                "ENTITY_NAME_DUPLICATED");
    }

    // ================== Delete safety ==================

    public async Task EnsureEntityCanBeDeletedAsync(Guid entityId, CancellationToken cancellationToken)
    {
        var spec = new EntityByIdSpec(entityId, includeFieldsAndValidators: true);
        var entity = await _entityRepository.FirstOrDefaultAsync(spec, cancellationToken: cancellationToken);

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
        var entity = await _entityRepository.FirstOrDefaultAsync(
            spec,
            asNoTracking: false, // cần tracking để load fields & relations
            cancellationToken: cancellationToken);

        if (entity is null)
            throw new BusinessRuleValidationException(
                $"Entity with Id {entityId} does not exist.",
                "ENTITY_NOT_FOUND");

        if (entity.Fields.Any(f => f.ColumnName.Equals(fieldName, StringComparison.OrdinalIgnoreCase)))
            throw new BusinessRuleValidationException(
                $"Field with name '{fieldName}' already exists in entity {entityId}.",
                "FIELD_NAME_DUPLICATED");
    }

    public async Task EnsureConstraintsValidAsync(Guid entityId, CancellationToken cancellationToken)
    {
        var spec = new EntityByIdSpec(entityId, includeFieldsAndValidators: true);
        var entity = await _entityRepository.FirstOrDefaultAsync(
            spec,
            asNoTracking: false, // cần tracking để load fields & relations
            cancellationToken: cancellationToken);

        if (entity is null)
            throw new BusinessRuleValidationException(
                $"Entity with Id {entityId} does not exist.",
                "ENTITY_NOT_FOUND");

        // TODO: add specific constraint validation logic
        // Ví dụ: check khóa chính phải tồn tại, không có 2 khóa chính, unique field hợp lệ, v.v.
    }

    // ================== Naming convention ==================

    public Task EnsureEntityNameFollowsConventionAsync(string name)
    {
        // Example convention: PascalCase + chữ cái đầu viết hoa
        if (!char.IsUpper(name[0]))
        {
            throw new BusinessRuleValidationException(
                $"Entity name '{name}' must start with uppercase.",
                "ENTITY_NAME_INVALID");
        }

        return Task.CompletedTask;
    }
}


