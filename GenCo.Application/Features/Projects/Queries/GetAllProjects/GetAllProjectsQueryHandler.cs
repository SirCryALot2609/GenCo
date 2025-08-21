using AutoMapper;
using GenCo.Application.DTOs.Project.Responses;
using GenCo.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Projects.Queries.GetAllProjects
{
    public class GetAllEntitiesQueryHandler(IProjectRepository repository, IMapper mapper)
        : IRequestHandler<GetAllProjectsQuery, ProjectSummaryResponseDto>
    {
        private readonly IProjectRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        public async Task<ProjectSummaryResponseDto> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _repository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return _mapper.Map<ProjectSummaryResponseDto>(projects);
        }
    }
}
