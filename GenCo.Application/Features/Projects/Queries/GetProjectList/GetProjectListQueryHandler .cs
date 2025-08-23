using AutoMapper;
using GenCo.Application.DTOs.Project;
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

namespace GenCo.Application.Features.Projects.Queries.GetProjectList
{
    public class GetProjectListQueryHandler(IGenericRepository<Project> repository, IMapper mapper) : IRequestHandler<GetProjectListQuery, ProjectListResponseDto>
    {
        private readonly IGenericRepository<Project> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<ProjectListResponseDto> Handle(GetProjectListQuery request, CancellationToken cancellationToken)
        {
            // Tạo specification
            int skip = (request.PageNumber - 1) * request.PageSize;
            var spec = new ProjectByKeywordSpec(
                keyword: request.Keyword,
                skip: skip,
                take: request.PageSize,
                includeAllCollections: request.IncludeAllCollections
            );

            // Gọi repository
            var (projects, totalCount) = await _repository.GetPagedAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);

            // Ánh xạ sang DTO
            var items = _mapper.Map<IReadOnlyCollection<ProjectListItemDto>>(projects);

            return new ProjectListResponseDto
            {
                Items = items,
                TotalCount = totalCount,
                Success = true,
                Message = "Projects retrieved successfully"
            };
        }
    }
}
