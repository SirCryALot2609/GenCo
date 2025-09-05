using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Project.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.Projects.Commands.RestoreProject;

public class RestoreProjectCommandHandler(
    IGenericRepository<Project> repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<RestoreProjectCommand, BaseResponseDto<bool>>
{
    public async Task<BaseResponseDto<bool>> Handle(RestoreProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (project == null)
            return BaseResponseDto<bool>.Fail("Project not found");

        await repository.RestoreAsync(project, cancellationToken);
        project.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponseDto<bool>.Ok(true, "Project restored successfully");
    }
}