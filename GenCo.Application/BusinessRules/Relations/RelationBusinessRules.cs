using GenCo.Application.Exceptions;
using GenCo.Application.Persistence.Contracts;
using GenCo.Application.Specifications.Entities;
using GenCo.Application.Specifications.Relations;
using GenCo.Domain.Entities;
using GenCo.Domain.Enum;

namespace GenCo.Application.BusinessRules.Relations;

public class RelationBusinessRules(
    IRelationRepository relationRepository,
    IEntityRepository entityRepository)
    : IRelationBusinessRules
{
    public Task EnsureRelationTypeValidAsync(string type)
    {
        if (string.IsNullOrWhiteSpace(type))
            throw new BusinessRuleValidationException("Relation type cannot be null or empty.", "RELATION_TYPE_REQUIRED");

        if (!Enum.TryParse(typeof(RelationType), type, true, out var parsed)
            || !Enum.IsDefined(typeof(RelationType), parsed))
            throw new BusinessRuleValidationException($"Relation type '{type}' is not valid.", "RELATION_TYPE_INVALID");

        return Task.CompletedTask;
    }

    public Task EnsureDeleteBehaviorValidAsync(string behavior)
    {
        if (string.IsNullOrWhiteSpace(behavior))
            throw new BusinessRuleValidationException("Delete behavior cannot be null or empty.", "RELATION_DELETE_BEHAVIOR_REQUIRED");

        if (!Enum.TryParse(typeof(DeleteBehavior), behavior, true, out var parsed)
            || !Enum.IsDefined(typeof(DeleteBehavior), parsed))
            throw new BusinessRuleValidationException($"Delete behavior '{behavior}' is not valid.", "RELATION_DELETE_BEHAVIOR_INVALID");

        return Task.CompletedTask;
    }

    public async Task EnsureEntitiesExistAsync(Guid fromEntityId, Guid toEntityId, CancellationToken cancellationToken)
    {
        var fromSpec = new EntityByIdSpec(fromEntityId);
        var toSpec = new EntityByIdSpec(toEntityId);

        var fromExists = await entityRepository.ExistsAsync(fromSpec, cancellationToken);
        var toExists = await entityRepository.ExistsAsync(toSpec, cancellationToken);

        if (!fromExists)
            throw new BusinessRuleValidationException(
                $"FromEntity '{fromEntityId}' does not exist.",
                "RELATION_FROM_ENTITY_NOT_FOUND");

        if (!toExists)
            throw new BusinessRuleValidationException(
                $"ToEntity '{toEntityId}' does not exist.",
                "RELATION_TO_ENTITY_NOT_FOUND");
    }


    public Task EnsureNoCircularRelationAsync(Guid fromEntityId, Guid toEntityId)
    {
        return fromEntityId == toEntityId ? throw new BusinessRuleValidationException("A relation cannot reference the same entity.", "RELATION_CIRCULAR_NOT_ALLOWED") : Task.CompletedTask;
    }

    public async Task EnsureRelationUniqueOnCreateAsync(Guid projectId, Guid fromEntityId, Guid toEntityId, string type, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<RelationType>(type, true, out var parsedType))
            throw new BusinessRuleValidationException($"Relation type '{type}' is invalid.", "RELATION_TYPE_INVALID");

        var spec = new RelationByEntitiesAndTypeSpec(projectId, fromEntityId, toEntityId, parsedType);

        if (await relationRepository.ExistsAsync(spec, cancellationToken))
            throw new BusinessRuleValidationException(
                $"A relation of type '{parsedType}' between these entities already exists.",
                "RELATION_DUPLICATE_ON_CREATE");
    }

    public async Task EnsureRelationUniqueOnUpdateAsync(Guid projectId, Guid fromEntityId, Guid toEntityId, Guid relationId, string type, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<RelationType>(type, true, out var parsedType))
            throw new BusinessRuleValidationException($"Relation type '{type}' is invalid.", "RELATION_TYPE_INVALID");

        var spec = new RelationByEntitiesAndTypeSpec(projectId, fromEntityId, toEntityId, parsedType, excludeRelationId: relationId);

        if (await relationRepository.ExistsAsync(spec, cancellationToken))
            throw new BusinessRuleValidationException(
                $"Another relation of type '{parsedType}' between these entities already exists.",
                "RELATION_DUPLICATE_ON_UPDATE");
    }

    public Task EnsureJoinTableConsistencyAsync(Relation relation)
    {
        if (relation.RelationType == RelationType.ManyToMany)
        {
            if (relation.JoinTables == null || relation.JoinTables.Count == 0)
                throw new BusinessRuleValidationException(
                    "Many-to-Many relation must define at least one JoinTable.",
                    "RELATION_JOIN_TABLE_REQUIRED");

            foreach (var join in relation.JoinTables)
            {
                if (string.IsNullOrWhiteSpace(join.JoinTableName))
                    throw new BusinessRuleValidationException("JoinTable name cannot be empty.", "RELATION_JOIN_TABLE_NAME_REQUIRED");

                if (string.IsNullOrWhiteSpace(join.LeftKey) || string.IsNullOrWhiteSpace(join.RightKey))
                    throw new BusinessRuleValidationException("JoinTable must define both LeftKey and RightKey.", "RELATION_JOIN_KEYS_REQUIRED");
            }
        }
        else
        {
            if (relation.JoinTables != null && relation.JoinTables.Any())
                throw new BusinessRuleValidationException(
                    $"{relation.RelationType} relation cannot contain JoinTables.",
                    "RELATION_JOIN_TABLE_NOT_ALLOWED");
        }

        return Task.CompletedTask;
    }

    public Task EnsureFieldMappingConsistencyAsync(Relation relation)
    {
        if (relation.RelationType is not (RelationType.OneToOne or RelationType.OneToMany)) return Task.CompletedTask;
        if (relation.FieldMappings == null || relation.FieldMappings.Count == 0)
            throw new BusinessRuleValidationException(
                $"{relation.RelationType} relation must define at least one FieldMapping.",
                "RELATION_FIELD_MAPPING_REQUIRED");

        return Task.CompletedTask;
    }

    public Task EnsureDeleteBehaviorCompatibilityAsync(Relation relation)
    {
        return relation.OnDelete switch
        {
            DeleteBehavior.Cascade when relation.RelationType == RelationType.ManyToMany => throw
                new BusinessRuleValidationException("Cascade delete is not allowed for Many-to-Many relations.",
                    "RELATION_DELETE_BEHAVIOR_INVALID"),
            DeleteBehavior.Restrict when relation.RelationType == RelationType.OneToMany => Task.CompletedTask // valid
            ,
            _ => Task.CompletedTask
        };
    }

    public Task EnsureRelationNameValidAsync(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Task.CompletedTask; // optional

        if (name.Length > 128)
            throw new BusinessRuleValidationException("Relation name is too long (max 128 chars).", "RELATION_NAME_TOO_LONG");

        return !char.IsLetter(name[0]) ? throw new BusinessRuleValidationException("Relation name must start with a letter.", "RELATION_NAME_INVALID") : Task.CompletedTask;
    }
}