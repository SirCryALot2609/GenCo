using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Entity.Responses;
using GenCo.Application.DTOs.Field.Responses;
using GenCo.Application.Persistence.Contracts;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.Entities;
using GenCo.Application.Specifications.Fields;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Fields.Queries.GetFieldById
{
    public class GetFieldByIdQueryHandler (
        IFieldRepository repository, 
        IUnitOfWork unitOfWork,
        IMapper mapper
        ) : IRequestHandler<GetFieldByIdQuery, BaseResponseDto<FieldResponseDto>>
    {
        private readonly IFieldRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponseDto<FieldResponseDto>> Handle(GetFieldByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new FieldByIdSpec(request.Id, request.IncludeDetails);
            var field = await _repository.FirstOrDefaultAsync(spec, cancellationToken: cancellationToken);
            if (field == null)
            {
                return BaseResponseDto<FieldResponseDto>.Fail("Field not found");
            }
            var dto = _mapper.Map<FieldResponseDto>(field);
            return BaseResponseDto<FieldResponseDto>.Ok(dto, "Field retrieved successfully");
        }
    }
}
