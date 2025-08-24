using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Project.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Projects.Commands.RestoreProject
{
    public class RestoreProjectCommandHandler(
        IGenericRepository<Project> repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
        : IRequestHandler<RestoreProjectCommand, BaseResponseDto<ProjectResponseDto>>
    {
        private readonly IGenericRepository<Project> _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponseDto<ProjectResponseDto>> Handle(
            RestoreProjectCommand request,
            CancellationToken cancellationToken)
        {
            var project = await _repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);

            if (project == null)
                return BaseResponseDto<ProjectResponseDto>.Fail("Project not found");

            await _repository.RestoreAsync(project, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ProjectResponseDto>(project);

            return BaseResponseDto<ProjectResponseDto>.Ok(dto, "Project restored successfully");
        }
    }
}
