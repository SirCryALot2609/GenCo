using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Entity.Responses;
using GenCo.Application.DTOs.Project.Responses;
using GenCo.Application.Features.Projects.Commands.RestoreProject;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Entities.Commands.RestoreEntity
{
    public class RestoreEntityCommandHandler(
        IGenericRepository<Entity> repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
        : IRequestHandler<RestoreEntityCommand, BaseResponseDto<EntityResponseDto>>
    {
        private readonly IGenericRepository<Entity> _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponseDto<EntityResponseDto>> Handle(
            RestoreEntityCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);

            if (entity == null)
                return BaseResponseDto<EntityResponseDto>.Fail("Entity not found");

            await _repository.RestoreAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<EntityResponseDto>(entity);

            return BaseResponseDto<EntityResponseDto>.Ok(dto, "Entity restored successfully");
        }
    }
}
