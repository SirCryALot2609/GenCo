using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Project.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.Projects;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.Projects.Queries.GetProjectById;
public class GetProjectByIdQueryHandler(
    IGenericRepository<Project> repository,
    IMapper mapper)
    : IRequestHandler<GetProjectByIdQuery, BaseResponseDto<ProjectDetailDto>>
{
    public async Task<BaseResponseDto<ProjectDetailDto>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new ProjectByIdSpec(request.Id, request.IncludeDetails);
        var project = await repository.FirstOrDefaultAsync(spec, cancellationToken: cancellationToken);

        if (project == null)
            return BaseResponseDto<ProjectDetailDto>.Fail("Project not found");

        var dto = mapper.Map<ProjectDetailDto>(project);
        return BaseResponseDto<ProjectDetailDto>.Ok(dto, "Project retrieved successfully");
    }
}
