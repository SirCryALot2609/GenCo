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

namespace GenCo.Application.Features.Fields.Commands.DeleteField
{
    public class DeleteFieldCommandHandler(IFieldRepository repository, IMapper mapper)
        : IRequestHandler<DeleteFieldCommand, BaseUpdateResponseDto>
    {
        private readonly IFieldRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseUpdateResponseDto> Handle(DeleteFieldCommand request, CancellationToken cancellationToken)
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
            var deleted = await _repository.DeleteAsync(field);
            return new BaseUpdateResponseDto
            {
                Success = deleted,
                Message = deleted
                    ? "Field deleted successfully."
                    : "Failed to delete field.",
                UpdatedAt = field.UpdateAt,
                UpdatedBy = field.UpdateBy,
            };
        }
    }
}
