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

namespace GenCo.Application.Features.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommandHandler(IProjectRepository repository, IMapper mapper)
        : IRequestHandler<UpdateProjectCommand, BaseUpdateResponseDto>
    {
        private readonly IProjectRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        public async Task<BaseUpdateResponseDto> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
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
            _mapper.Map(request.Request, project);
            var updated = await _repository.UpdateAsync(project);
            return new BaseUpdateResponseDto
            {
                Success = true,
                Message = "Project updated successfully.",
                UpdatedAt = updated.UpdateAt,
                UpdatedBy = updated.UpdateBy,
            };
        }
    }
}
