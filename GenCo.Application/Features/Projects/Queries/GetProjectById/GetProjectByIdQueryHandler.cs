using AutoMapper;
using GenCo.Application.DTOs.Project.Responses;
using GenCo.Application.Persistence.Contracts;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.Projects;
using GenCo.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Projects.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDetailResponseDto>
    {
        private readonly IGenericRepository<Project> _repository;
        private readonly IMapper _mapper;

        public GetProjectByIdQueryHandler(IGenericRepository<Project> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProjectDetailResponseDto> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            // Sử dụng Specification
            var spec = new ProjectByIdSpec(request.ProjectId, request.IncludeAllCollections);
            var project = await _repository.FirstOrDefaultAsync(spec, cancellationToken: cancellationToken);

            if (project == null)
                throw new Exception("Project not found");

            // Ánh xạ sang DTO
            var dto = _mapper.Map<ProjectDetailDto>(project);

            return new ProjectDetailResponseDto
            {
                Project = dto,
                Success = true,
                UpdatedAt = project.UpdatedAt,
                CreatedAt = project.CreatedAt,
                CreatedBy = project.CreatedBy,
                UpdatedBy = project.UpdatedBy,
                DeletedAt = project.DeletedAt,
                DeletedBy = project.DeletedBy
            };
        }
    }
}
