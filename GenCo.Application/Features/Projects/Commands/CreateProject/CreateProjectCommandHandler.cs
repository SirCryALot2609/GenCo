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

namespace GenCo.Application.Features.Projects.Commands.CreateProject
{
    public class CreateProjectCommandHandler(
    IGenericRepository<Project> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<CreateProjectCommand, BaseResponseDto<ProjectResponseDto>>
    {
        private readonly IGenericRepository<Project> _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponseDto<ProjectResponseDto>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = _mapper.Map<Project>(request.Request);
            project.CreatedAt = DateTime.UtcNow;
            project.CreatedBy = "system"; // sau này lấy từ context user
            await _repository.AddAsync(project, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var dto = _mapper.Map<ProjectResponseDto>(project);
            return BaseResponseDto<ProjectResponseDto>.Ok(dto, "Project created successfully");
        }
    }
}
