using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.RelationJoinTable.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.RelationJoinTables;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.RelationJoinTables.Queries.GetRelationJoinTableById;


public class GetRelationJoinTableByIdQueryHandler(
    IGenericRepository<RelationJoinTable> repository,
    IMapper mapper)
    : IRequestHandler<GetRelationJoinTableByIdQuery, BaseResponseDto<RelationJoinTableDetailDto>>
{
    public async Task<BaseResponseDto<RelationJoinTableDetailDto>> Handle(GetRelationJoinTableByIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new RelationJoinTableByIdSpec(request.Id, request.IncludeDetails);
        var entity = await repository.FirstOrDefaultAsync(spec, cancellationToken: cancellationToken);

        if (entity == null)
            return BaseResponseDto<RelationJoinTableDetailDto>.Fail("RelationJoinTable not found");

        var dto = mapper.Map<RelationJoinTableDetailDto>(entity);
        return BaseResponseDto<RelationJoinTableDetailDto>.Ok(dto, "RelationJoinTable retrieved successfully");
    }
}