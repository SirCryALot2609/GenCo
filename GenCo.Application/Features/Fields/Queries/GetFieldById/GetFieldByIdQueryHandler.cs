using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Field.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.Fields;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.Fields.Queries.GetFieldById;
public class GetFieldByIdQueryHandler(
    IGenericRepository<Field> repository,
    IMapper mapper)
    : IRequestHandler<GetFieldByIdQuery, BaseResponseDto<FieldDetailDto>>
{
    public async Task<BaseResponseDto<FieldDetailDto>> Handle(GetFieldByIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new FieldByIdSpec(request.Id, request.IncludeValidators);
        var field = await repository.FirstOrDefaultAsync(spec, cancellationToken: cancellationToken);

        if (field == null)
            return BaseResponseDto<FieldDetailDto>.Fail("Field not found");

        var dto = mapper.Map<FieldDetailDto>(field);
        return BaseResponseDto<FieldDetailDto>.Ok(dto, "Field retrieved successfully");
    }
}
