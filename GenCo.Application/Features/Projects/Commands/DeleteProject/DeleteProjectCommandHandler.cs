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
                : IRequestHandler<DeleteProjectCommand, BaseDeleteResponseDto>
    {
        private readonly IProjectRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseDeleteResponseDto> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetByIdAsync(request.Request.Id);
            if (project == null)
            {
                return new BaseDeleteResponseDto
                {
                    Success = false,
                    Message = "Project not found.",
                    DeleteAt = DateTime.UtcNow,
                    DeleteBy = "system"
                };
            }
            await _repository.DeleteAsync(project);
            var deleted = await _repository.GetByIdAsync(project.Id);
            return new BaseDeleteResponseDto
            {
                Success = deleted.IsDelete,
                Message = deleted.IsDelete
                    ? "Project deleted successfully."
                    : "Failed to delete project.",
                DeleteAt = deleted.DeleteAt,
                DeleteBy = deleted.DeleteBy,
            };
        }
    }
}
