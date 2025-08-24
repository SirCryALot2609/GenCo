using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Entity.Responses;
using GenCo.Application.DTOs.Field;
using GenCo.Application.DTOs.Field.Responses;
using GenCo.Application.Persistence.Contracts;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Fields.Commands.RestoreField
{
    public class RestoreFieldCommandHandler(
        IFieldRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper
        ) : IRequestHandler<RestoreFieldCommand, BaseResponseDto<FieldResponseDto>>
    {
        private readonly IFieldRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponseDto<FieldResponseDto>> Handle(RestoreFieldCommand request, CancellationToken cancellationToken)
        {
            var field = await _repository.GetByIdAsync( request.Id , cancellationToken: cancellationToken);
            if (field == null)
                return BaseResponseDto<FieldResponseDto>.Fail("Field not found");
            await _repository.RestoreAsync(field, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<FieldResponseDto>(field);

            return BaseResponseDto<FieldResponseDto>.Ok(dto, "Field restored successfully");
        }
    }
}
