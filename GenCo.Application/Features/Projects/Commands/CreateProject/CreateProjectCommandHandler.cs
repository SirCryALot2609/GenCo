using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts;
using GenCo.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Projects.Commands.CreateProject
{
    public class CreateProjectCommandHandler(IProjectRepository repository, IMapper mapper)
        : IRequestHandler<CreateProjectCommand, BaseCreateResponseDto>
    {
        private readonly IProjectRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseCreateResponseDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = _mapper.Map<Project>(request.Request);
            project.CreatedAt = DateTime.UtcNow;
            project.CreatedBy = "system"; // TODO: lấy từ context user hiện tại
            var created = await _repository.AddAsync(project);
            return new BaseCreateResponseDto
            {
                Success = true,
                Message = "Project created successfully.",
                CreatedAt = created.CreatedAt,
                CreatedBy = created.CreatedBy,
            };
        }
    }
}
