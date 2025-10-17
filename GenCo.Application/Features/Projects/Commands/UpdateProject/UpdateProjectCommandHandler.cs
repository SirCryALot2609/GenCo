using AutoMapper;
using GenCo.Application.BusinessRules.Projects;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Project.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.Projects.Commands.UpdateProject;

public class UpdateProjectCommandHandler(
    IGenericRepository<Project> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IProjectBusinessRules projectBusinessRules)
    : IRequestHandler<UpdateProjectCommand, BaseResponseDto<ProjectResponseDto>>
{
    public async Task<BaseResponseDto<ProjectResponseDto>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        await projectBusinessRules.EnsureProjectExistsAsync(request.Request.Id, cancellationToken);
        await projectBusinessRules.EnsureNameValidAsync(request.Request.Name);
        await projectBusinessRules.EnsureProjectNameUniqueOnUpdateAsync(request.Request.Id, request.Request.Name, cancellationToken);
        var project = await repository.GetByIdAsync(request.Request.Id, cancellationToken: cancellationToken);
        mapper.Map(request.Request, project);
        project!.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(project, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = mapper.Map<ProjectResponseDto>(project);
        return BaseResponseDto<ProjectResponseDto>.Ok(dto, "Project updated successfully");
    }
}
