using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.EntityConstraintField.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.EntityConstraintFields;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.EntityConstraintFields.Queries.GetEntityConstraintFieldById;


public class GetEntityConstraintFieldByIdQueryHandler(
    IGenericRepository<EntityConstraintField> repository,
    IMapper mapper)
    : IRequestHandler<GetEntityConstraintFieldByIdQuery, BaseResponseDto<EntityConstraintFieldDetailDto>>
{
    public async Task<BaseResponseDto<EntityConstraintFieldDetailDto>> Handle(GetEntityConstraintFieldByIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new EntityConstraintFieldByIdSpec(request.Id, request.IncludeDetails);
        var entity = await repository.FirstOrDefaultAsync(spec, cancellationToken: cancellationToken);

        if (entity == null)
            return BaseResponseDto<EntityConstraintFieldDetailDto>.Fail("EntityConstraintField not found");

        var dto = mapper.Map<EntityConstraintFieldDetailDto>(entity);
        return BaseResponseDto<EntityConstraintFieldDetailDto>.Ok(dto, "EntityConstraintField retrieved successfully");
    }
}