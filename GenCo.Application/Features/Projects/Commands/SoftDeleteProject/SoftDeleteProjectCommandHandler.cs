using GenCo.Application.DTOs.Project.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Projects.Commands.SoftDeleteProject
{
    public class SoftDeleteProjectCommandHandler : IRequestHandler<SoftDeleteProjectCommand, DeleteProjectResponseDto>
    {
        private readonly IGenericRepository<Project> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SoftDeleteProjectCommandHandler(IGenericRepository<Project> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteProjectResponseDto> Handle(SoftDeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
            if (project == null)
                return new DeleteProjectResponseDto { Success = false, Message = "Project not found" };

            await _repository.SoftDeleteAsync(project, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new DeleteProjectResponseDto { Success = true, Message = "Project soft-deleted successfully" };
        }
    }
}
