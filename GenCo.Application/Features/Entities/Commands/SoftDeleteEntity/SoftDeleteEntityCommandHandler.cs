using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Entity.Responses;
using GenCo.Application.DTOs.Project.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Entities.Commands.SoftDeleteEntity
{
    public class SoftDeleteEntityCommandHandler(
    IGenericRepository<Entity> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<SoftDeleteEntityCommand, BaseResponseDto<EntityResponseDto>>
    {
        private readonly IGenericRepository<Entity> _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponseDto<EntityResponseDto>> Handle(SoftDeleteEntityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Request.Id, cancellationToken : cancellationToken);
            if (entity == null)
                return BaseResponseDto<EntityResponseDto>.Fail("Entity not found.");
            await _repository.SoftDeleteAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var dto = _mapper.Map<EntityResponseDto>(entity);
            return BaseResponseDto<EntityResponseDto>.Ok(dto, "Entity soft deleted successfully");
        }
    }
}
