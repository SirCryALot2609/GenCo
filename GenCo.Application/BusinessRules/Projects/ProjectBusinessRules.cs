using GenCo.Application.Exceptions;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.Projects;
using GenCo.Domain.Entities;

namespace GenCo.Application.BusinessRules.Projects;

public class ProjectBusinessRules(
    IGenericRepository<Project> projectRepository
) : IProjectBusinessRules
{
    public async Task EnsureProjectExistsAsync(Guid projectId, CancellationToken cancellationToken)
    {
        var spec = new ProjectByIdSpec(projectId);
        var exists = await projectRepository.ExistsAsync(spec, cancellationToken).ConfigureAwait(false);
        if (!exists)
            throw new BusinessRuleValidationException($"Project with Id {projectId} does not exist.", "PROJECT_NOT_FOUND");
    }

    public async Task EnsureProjectNameUniqueOnCreateAsync(string name, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        var spec = new ProjectByNameSpec(name);
        var exists = await projectRepository.ExistsAsync(spec, cancellationToken).ConfigureAwait(false);
        if (exists)
            throw new BusinessRuleValidationException($"Project name '{name}' already exists.", "PROJECT_NAME_DUPLICATED");
    }

    public async Task EnsureProjectNameUniqueOnUpdateAsync(Guid projectId, string name, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        var spec = new ProjectByNameSpec(name, excludeProjectId: projectId);
        var exists = await projectRepository.ExistsAsync(spec, cancellationToken).ConfigureAwait(false);
        if (exists)
            throw new BusinessRuleValidationException($"Project name '{name}' already exists.", "PROJECT_NAME_DUPLICATED");
    }

    public async Task EnsureCanDeleteAsync(Guid projectId, CancellationToken cancellationToken)
    {
        var spec = new ProjectByIdSpec(projectId, includeRelations: true, includeEntities: true);
        var project = await projectRepository.FirstOrDefaultAsync(spec, cancellationToken: cancellationToken).ConfigureAwait(false);
        if (project is null)
            throw new BusinessRuleValidationException($"Project with Id {projectId} does not exist.", "PROJECT_NOT_FOUND");
        if (project.Entities.Count > 0 || project.Relations.Count > 0)
            throw new BusinessRuleValidationException("Cannot delete project containing entities or relations.", "PROJECT_HAS_DEPENDENCIES");
    }

    public Task EnsureCanAddConnectionAsync(Project project, Connection connection)
    {
        ArgumentNullException.ThrowIfNull(project);
        ArgumentNullException.ThrowIfNull(connection);

        var duplicate = project.Connections.Any(c => 
            c.Name.Equals(connection.Name, StringComparison.OrdinalIgnoreCase));

        return duplicate ? throw new BusinessRuleValidationException($"Connection '{connection.Name}' already exists in project '{project.Name}'.", "PROJECT_CONNECTION_DUPLICATED") : Task.CompletedTask;
    }

    public Task EnsureCanAddWorkflowAsync(Project project, Workflow workflow)
    {
        ArgumentNullException.ThrowIfNull(project);
        ArgumentNullException.ThrowIfNull(workflow);

        var duplicate = project.Workflows.Any(w => 
            w.Name.Equals(workflow.Name, StringComparison.OrdinalIgnoreCase));

        return duplicate ? throw new BusinessRuleValidationException($"Workflow '{workflow.Name}' already exists in project '{project.Name}'.", "PROJECT_WORKFLOW_DUPLICATED") : Task.CompletedTask;
    }

    public Task EnsureNameValidAsync(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        if (name.Length > 100)
            throw new BusinessRuleValidationException("Project name cannot exceed 100 characters.", "PROJECT_NAME_TOO_LONG");
        return Task.CompletedTask;
    }
}