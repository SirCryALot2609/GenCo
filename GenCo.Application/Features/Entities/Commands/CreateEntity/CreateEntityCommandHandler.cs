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
        : IRequestHandler<CreateEntityCommand, CreateEntityResponseDto>
    {
        private readonly IEntityRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<CreateEntityResponseDto> Handle(CreateEntityCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Entity>(request.Request);
            entity.Id = Guid.NewGuid();
            await _repository.AddAsync(entity);
            return _mapper.Map<CreateEntityResponseDto>(entity);
        }
    }
}
