using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Project.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Projects.Commands.SoftDeleteProject
{
    public class SoftDeleteProjectCommandHandler(
        IGenericRepository<Project> repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
        : IRequestHandler<SoftDeleteProjectCommand, BaseResponseDto<ProjectResponseDto>>
    {
        private readonly IGenericRepository<Project> _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponseDto<ProjectResponseDto>> Handle(
            SoftDeleteProjectCommand request,
            CancellationToken cancellationToken)
        {
            var project = await _repository.GetByIdAsync(request.Request.Id, cancellationToken: cancellationToken);
            if (project == null)
                return BaseResponseDto<ProjectResponseDto>.Fail("Project not found");
            await _repository.SoftDeleteAsync(project, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var dto = _mapper.Map<ProjectResponseDto>(project);
            return BaseResponseDto<ProjectResponseDto>.Ok(dto, "Project soft deleted successfully");
        }
    }
}
