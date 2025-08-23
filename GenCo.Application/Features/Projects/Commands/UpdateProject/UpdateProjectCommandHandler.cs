using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Project.Responses;
using GenCo.Application.Persistence.Contracts;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, UpdateProjectResponseDto>
    {
        private readonly IGenericRepository<Project> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProjectCommandHandler(IGenericRepository<Project> repository,
                                           IUnitOfWork unitOfWork,
                                           IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UpdateProjectResponseDto> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetByIdAsync(request.Request.Id, cancellationToken: cancellationToken);
            if (project == null)
                throw new Exception("Project not found");

            project.Name = request.Request.Name;
            project.Description = request.Request.Description;
            project.UpdatedAt = DateTime.UtcNow;
            await _repository.UpdateAsync(project, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ProjectResponseDto>(project);
            return new UpdateProjectResponseDto { Project = dto, Success = true, UpdatedAt = DateTime.UtcNow };
        }
    }
}
