using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Project.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.Projects.Commands.CreateProject;


public class CreateProjectCommandHandler(
    IGenericRepository<Project> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<CreateProjectCommand, BaseResponseDto<ProjectResponseDto>>
{
    public async Task<BaseResponseDto<ProjectResponseDto>> Handle(
        CreateProjectCommand request,
        CancellationToken cancellationToken)
    {
        var project = mapper.Map<Project>(request.Request);
        project.Id = Guid.NewGuid();
        project.CreatedAt = DateTime.UtcNow;
        project.UpdatedAt = null;

        await repository.AddAsync(project, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = mapper.Map<ProjectResponseDto>(project);
        return BaseResponseDto<ProjectResponseDto>.Ok(dto, "Project created successfully");
    }
}