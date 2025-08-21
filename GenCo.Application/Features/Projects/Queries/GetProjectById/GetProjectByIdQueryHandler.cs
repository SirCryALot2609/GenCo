using AutoMapper;
using GenCo.Application.DTOs.Project.Responses;
using GenCo.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Projects.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler(IProjectRepository repository, IMapper mapper)
        : IRequestHandler<GetProjectByIdQuery, ProjectResponseDto>
    {
        private readonly IProjectRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        public async Task<ProjectResponseDto> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<ProjectResponseDto>(project);
        }
    }
}
