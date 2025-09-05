using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.Projects.Commands.SoftDeleteProject;
public class SoftDeleteProjectCommandHandler(
    IGenericRepository<Project> repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<SoftDeleteProjectCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(SoftDeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (project == null)
            return BaseResponseDto<bool>.Fail("Project not found");

        await repository.SoftDeleteAsync(project, cancellationToken);
        project.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "Project soft deleted successfully");
    }
}
