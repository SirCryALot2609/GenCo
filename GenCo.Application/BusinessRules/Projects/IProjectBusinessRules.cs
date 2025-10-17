using GenCo.Domain.Entities;

namespace GenCo.Application.BusinessRules.Projects;

public interface IProjectBusinessRules
{
    Task EnsureProjectExistsAsync(Guid projectId, CancellationToken cancellationToken);
    Task EnsureProjectNameUniqueOnCreateAsync(string name, CancellationToken cancellationToken);
    Task EnsureProjectNameUniqueOnUpdateAsync(Guid projectId, string name, CancellationToken cancellationToken);
    Task EnsureCanDeleteAsync(Guid projectId, CancellationToken cancellationToken);
    Task EnsureCanAddConnectionAsync(Project project, Connection connection);
    Task EnsureCanAddWorkflowAsync(Project project, Workflow workflow);
    Task EnsureNameValidAsync(string name);
}