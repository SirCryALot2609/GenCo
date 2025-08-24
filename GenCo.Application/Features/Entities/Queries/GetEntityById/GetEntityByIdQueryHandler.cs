using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Entity.Responses;
using GenCo.Application.Persistence.Contracts;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.Entities;
using GenCo.Domain;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Entities.Queries.GetEntityById
{
    public class GetEntityByIdQueryHandler(
    IGenericRepository<Entity> repository,
    IMapper mapper)
    : IRequestHandler<GetEntityByIdQuery, BaseResponseDto<EntityDetailsResponseDto>>
    {
        private readonly IGenericRepository<Entity> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponseDto<EntityDetailsResponseDto>> Handle(GetEntityByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new EntityByIdSpec(request.Id, request.IncludeDetails);
            var entity = await _repository.FirstOrDefaultAsync(spec, cancellationToken : cancellationToken);
            if (entity == null)
            {
                return BaseResponseDto<EntityDetailsResponseDto>.Fail("Entity not found");
            }
            var dto = _mapper.Map<EntityDetailsResponseDto>(entity);
            return BaseResponseDto<EntityDetailsResponseDto>.Ok(dto, "Entity retrieved successfully");
        }
    }
}
