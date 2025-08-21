using AutoMapper;
using GenCo.Application.DTOs.Field.Responses;
using GenCo.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Fields.Queries.GetFieldsByEntityId
{
    public class GetFieldsByEntityIdQueryHandler(IFieldRepository repository, IMapper mapper)
        : IRequestHandler<GetFieldsByEntityIdQuery, FieldsByEntityIdResponseDto>
    {
        private readonly IFieldRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<FieldsByEntityIdResponseDto> Handle(GetFieldsByEntityIdQuery request, CancellationToken cancellationToken)
        {
            var fields = await _repository.GetFieldsByEntityIdAsync(request.EntityId);
            if (fields == null || fields.Count == 0)
            {
                return new FieldsByEntityIdResponseDto
                {
                    Success = false,
                    Message = "No fields found for the given entity.",
                    RetrievedAt = DateTime.UtcNow
                };
            }
            var fieldDtos = _mapper.Map<List<FieldResponseDto>>(fields);
            return new FieldsByEntityIdResponseDto
            {
                Success = true,
                Message = "Fields retrieved successfully.",
                RetrievedAt = DateTime.UtcNow,
                Fields = fieldDtos
            };
        }
    }
}
