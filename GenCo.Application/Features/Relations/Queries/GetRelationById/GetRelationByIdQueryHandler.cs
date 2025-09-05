using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Relation.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.Relations;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.Relations.Queries.GetRelationById;
public class GetRelationByIdQueryHandler(
    IGenericRepository<Relation> repository,
    IMapper mapper)
    : IRequestHandler<GetRelationByIdQuery, BaseResponseDto<RelationDetailDto>>
{
    public async Task<BaseResponseDto<RelationDetailDto>> Handle(GetRelationByIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new RelationByIdSpec(request.Id, request.IncludeDetails);
        var relation = await repository.FirstOrDefaultAsync(spec,cancellationToken : cancellationToken);

        if (relation == null)
            return BaseResponseDto<RelationDetailDto>.Fail("Relation not found");

        var dto = mapper.Map<RelationDetailDto>(relation);
        return BaseResponseDto<RelationDetailDto>.Ok(dto, "Relation retrieved successfully");
    }
}
