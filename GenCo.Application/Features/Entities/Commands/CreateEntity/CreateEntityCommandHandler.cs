using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Entity.Responses;
using GenCo.Application.Persistence.Contracts;
using GenCo.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Entities.Commands.CreateEntity
{
    public class CreateEntityCommandHandler(IEntityRepository repository, IMapper mapper)
        : IRequestHandler<CreateEntityCommand, BaseCreateResponseDto>
    {
        private readonly IEntityRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseCreateResponseDto> Handle(CreateEntityCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Entity>(request.Request);
            entity.CreatedAt = DateTime.UtcNow;
            entity.CreatedBy = "system"; // TODO: lấy từ context user hiện tại
            var created = await _repository.AddAsync(entity);
            return new BaseCreateResponseDto
            {
                Success = true,
                Message = "Entity created successfully.",
                CreatedAt = created.CreatedAt,
                CreatedBy = created.CreatedBy,
            };
        }
    }
}
