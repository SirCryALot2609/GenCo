using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts;
using GenCo.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Features.Fields.Commands.UpdateField
{
    public class UpdateFieldCommandHandler(IFieldRepository repository, IMapper mapper)
        : IRequestHandler<UpdateFieldCommand, BaseUpdateResponseDto>
    {
        private readonly IFieldRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseUpdateResponseDto> Handle(UpdateFieldCommand request, CancellationToken cancellationToken)
        {
            var field = await _repository.GetByIdAsync(request.Request.Id);
            if (field == null)
            {
                return new BaseUpdateResponseDto
                {
                    Success = false,
                    Message = "Field not found.",
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = "system"
                };
            }
            _mapper.Map(request.Request, field);
            await _repository.UpdateAsync(field);
            return new BaseUpdateResponseDto
            {
                Success = true,
                Message = "Entity updated successfully.",
                UpdatedAt = field.UpdateAt,
                UpdatedBy = field.UpdateBy,
            };
        }
    }
}
