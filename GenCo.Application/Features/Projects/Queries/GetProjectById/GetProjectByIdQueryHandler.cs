using AutoMapper;
using GenCo.Application.DTOs.Common;
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
    public class GetProjectByIdQueryHandler(
        IGenericRepository<Project> repository,
        IMapper mapper)
        : IRequestHandler<GetProjectByIdQuery, BaseResponseDto<ProjectDetailDto>>
    {
        private readonly IGenericRepository<Project> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponseDto<ProjectDetailDto>> Handle(
            GetProjectByIdQuery request,
            CancellationToken cancellationToken)
        {
            var spec = new ProjectByIdSpec(request.ProjectId, request.IncludeAllCollections);
            var project = await _repository.FirstOrDefaultAsync(spec, cancellationToken: cancellationToken);

            if (project is null)
            {
                return new BaseResponseDto<ProjectDetailDto>
                {
                    Success = false,
                    Message = "Project not found"
                };
            }

            var dto = _mapper.Map<ProjectDetailDto>(project);
            return BaseResponseDto<ProjectDetailDto>.Ok(dto);
        }
    }

}
