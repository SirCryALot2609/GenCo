using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Entity.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.Entities;
using GenCo.Domain.Entities;
using MediatR;
namespace GenCo.Application.Features.Entities.Queries.GetEntityById;
public class GetEntityByIdQueryHandler(
    IGenericRepository<Entity> repository,
    IMapper mapper)
    : IRequestHandler<GetEntityByIdQuery, BaseResponseDto<EntityDetailDto>>
{
    public async Task<BaseResponseDto<EntityDetailDto>> Handle(GetEntityByIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new EntityByIdSpec(request.Id, request.IncludeDetails);
        var entity = await repository.FirstOrDefaultAsync(spec, cancellationToken: cancellationToken);

        if (entity == null)
            return BaseResponseDto<EntityDetailDto>.Fail("Entity not found");

        var dto = mapper.Map<EntityDetailDto>(entity);
        return BaseResponseDto<EntityDetailDto>.Ok(dto, "Entity retrieved successfully");
    }
}