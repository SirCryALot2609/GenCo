using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Project;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.Projects;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.Projects.Queries.GetProjectList;

public class GetProjectListQueryHandler
    : IRequestHandler<GetProjectListQuery, PagedResponseDto<ProjectBaseDto>>
{
    private readonly IGenericRepository<Project> _repository;
    private readonly IMapper _mapper;

    public GetProjectListQueryHandler(
        IGenericRepository<Project> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedResponseDto<ProjectBaseDto>> Handle(
        GetProjectListQuery request,
        CancellationToken cancellationToken)
    {
        int pageNumber = Math.Max(request.PageNumber, 1);
        int pageSize = Math.Max(request.PageSize, 1);

        var spec = new ProjectByKeywordSpec(
            keyword: request.Keyword,
            includeAllCollections: request.IncludeAllCollections
        );

        var (projects, totalCount) = await _repository.GetPagedAsync(
            spec,
            pageNumber,
            pageSize,
            cancellationToken: cancellationToken
        );

        var items = _mapper.Map<IReadOnlyCollection<ProjectBaseDto>>(
            projects ?? new List<Project>()
        );

        return PagedResponseDto<ProjectBaseDto>.Ok(
            items,
            totalCount,
            pageNumber,
            pageSize,
            "Projects retrieved successfully"
        );
    }
}