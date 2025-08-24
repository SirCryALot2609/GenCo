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

namespace GenCo.Application.Features.Projects.Commands.DeleteProject
{
    public class DeleteProjectCommandHandler(
        IGenericRepository<Project> repository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<DeleteProjectCommand, BaseResponseDto<BoolResultDto>>
    {
        private readonly IGenericRepository<Project> _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<BaseResponseDto<BoolResultDto>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetByIdAsync(request.Request.Id, cancellationToken: cancellationToken);
            if (project == null)
            {
                return BaseResponseDto<BoolResultDto>.Fail("Project not found");
            }

            await _repository.DeleteAsync(project, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return BaseResponseDto<BoolResultDto>.Ok(new BoolResultDto { Value = true }, "Project deleted permanently");
        }
    }

}
