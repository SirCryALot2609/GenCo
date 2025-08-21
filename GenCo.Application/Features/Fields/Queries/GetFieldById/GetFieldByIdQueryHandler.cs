using AutoMapper;
using GenCo.Application.DTOs.Field.Responses;
using GenCo.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Fields.Queries.GetFieldById
{
    public class GetFieldByIdQueryHandler (IFieldRepository repository, IMapper mapper)
        : IRequestHandler<GetFieldByIdQuery, FieldResponseDto>
    {
        private readonly IFieldRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<FieldResponseDto> Handle(GetFieldByIdQuery request, CancellationToken cancellationToken)
        {
            var field = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<FieldResponseDto>(field);
        }
    }
}
