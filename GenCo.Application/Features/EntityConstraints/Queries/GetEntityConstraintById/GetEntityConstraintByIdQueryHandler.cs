using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.EntityConstraint.Response;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.EntityConstraints;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.EntityConstraints.Queries.GetEntityConstraintById;

public class GetEntityConstraintByIdQueryHandler(
    IGenericRepository<EntityConstraint> repository,
    IMapper mapper)
    : IRequestHandler<GetEntityConstraintByIdQuery, BaseResponseDto<EntityConstraintDetailDto>>
{
    public async Task<BaseResponseDto<EntityConstraintDetailDto>> Handle(GetEntityConstraintByIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new EntityConstraintByIdSpec(request.Id, request.IncludeDetails);
        var constraint = await repository.FirstOrDefaultAsync(spec, cancellationToken: cancellationToken);

        if (constraint == null)
            return BaseResponseDto<EntityConstraintDetailDto>.Fail("EntityConstraint not found");

        var dto = mapper.Map<EntityConstraintDetailDto>(constraint);
        return BaseResponseDto<EntityConstraintDetailDto>.Ok(dto, "EntityConstraint retrieved successfully");
    }
}