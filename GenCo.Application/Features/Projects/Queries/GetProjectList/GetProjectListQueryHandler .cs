using AutoMapper;
using GenCo.Application.DTOs.Common;
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
    public class GetProjectListQueryHandler
    : IRequestHandler<GetProjectListQuery, PagedResponseDto<ProjectListItemDto>>
    {
        private readonly IGenericRepository<Project> _repository;
        private readonly IMapper _mapper;

        public GetProjectListQueryHandler(IGenericRepository<Project> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResponseDto<ProjectListItemDto>> Handle(
            GetProjectListQuery request,
            CancellationToken cancellationToken)
        {
            var spec = new ProjectByKeywordSpec(
                keyword: request.Keyword,
                includeAllCollections: request.IncludeAllCollections
            );

            var (projects, totalCount) = await _repository.GetPagedAsync(
                spec,
                request.PageNumber,
                request.PageSize,
                cancellationToken: cancellationToken
            );

            var items = _mapper.Map<IReadOnlyCollection<ProjectListItemDto>>(projects);

            return PagedResponseDto<ProjectListItemDto>.Ok(
                items,
                totalCount,
                request.PageNumber,
                request.PageSize,
                "Projects retrieved successfully"
            );
        }
    }
}
