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

namespace GenCo.Application.Features.Projects.Commands.DeleteProject
{
    public class DeleteProjectCommandHandler(IProjectRepository repository, IMapper mapper)
                : IRequestHandler<DeleteProjectCommand, BaseUpdateResponseDto>
    {
        private readonly IProjectRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseUpdateResponseDto> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetByIdAsync(request.Request.Id);
            if (project == null)
            {
                return new BaseUpdateResponseDto
                {
                    Success = false,
                    Message = "Project not found.",
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = "system"
                };
            }
            var deleted = await _repository.DeleteAsync(project);
            return new BaseUpdateResponseDto
            {
                Success = deleted,
                Message = deleted
                    ? "Project deleted successfully."
                    : "Failed to delete project.",
                UpdatedAt = project.CreatedAt,
                UpdatedBy = project.UpdateBy,
            };
        }
    }
}
