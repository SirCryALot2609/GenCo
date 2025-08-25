using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Entity.Responses;
using GenCo.Application.Persistence.Contracts;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Entities.Commands.CreateEntity
{
    public class CreateEntityCommandHandler(
    IGenericRepository<Entity> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<CreateEntityCommand, BaseResponseDto<EntityResponseDto>>
    {
        private readonly IGenericRepository<Entity> _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponseDto<EntityResponseDto>> Handle(
            CreateEntityCommand request,
            CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Entity>(request.Request);

            entity.Id = Guid.NewGuid();
            entity.CreatedAt = DateTime.UtcNow;
            entity.CreatedBy = "system"; // sau này lấy từ context user

            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<EntityResponseDto>(entity);

            return BaseResponseDto<EntityResponseDto>.Ok(dto, "Entity created successfully");
        }
    }

}
