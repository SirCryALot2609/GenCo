using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Entity.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.Entities;
using GenCo.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Entities.Queries.GetChildEntities
{
    public class GetChildEntitiesQueryHandler(
    IGenericRepository<Entity> repository,
    IMapper mapper)
    : IRequestHandler<GetChildEntitiesQuery, PagedResponseDto<EntityDetailsResponseDto>>
    {
        private readonly IGenericRepository<Entity> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<PagedResponseDto<EntityDetailsResponseDto>> Handle(
            GetChildEntitiesQuery request,
            CancellationToken cancellationToken)
        {
            var dto = request.Request;

            int skip = (dto.PageNumber - 1) * dto.PageSize;
            int take = dto.PageSize;

            // Specification: load từ entity cha + paging level + include fields/validators
            var spec = new EntityByPagingLevelSpec(
                rootEntityId: dto.ParentEntityId,
                pagingLevel: dto.PagingLevel,
                skip: skip,
                take: take
            );

            // Gọi repository
            var (entities, totalCount) = await _repository.GetPagedAsync(
                spec,
                dto.PageNumber,
                dto.PageSize,
                cancellationToken: cancellationToken
            );

            // Map sang DTO
            var items = _mapper.Map<IReadOnlyCollection<EntityDetailsResponseDto>>(entities);

            return PagedResponseDto<EntityDetailsResponseDto>.Ok(
                items,
                totalCount,
                dto.PageNumber,
                dto.PageSize,
                "Child entities retrieved successfully"
            );
        }
    }
}
