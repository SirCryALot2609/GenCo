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
        : IRequestHandler<DeleteFieldCommand, BaseDeleteResponseDto>
    {
        private readonly IFieldRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseDeleteResponseDto> Handle(DeleteFieldCommand request, CancellationToken cancellationToken)
        {
            var field = await _repository.GetByIdAsync(request.Request.Id);
            if (field == null)
            {
                return new BaseDeleteResponseDto
                {
                    Success = false,
                    Message = "Field not found.",
                    DeleteAt = DateTime.UtcNow,
                    DeleteBy = "system"
                };
            }
            await _repository.DeleteAsync(field);
            var deleted = await _repository.GetByIdAsync(field.Id);
            return new BaseDeleteResponseDto
            {
                Success = deleted.IsDelete,
                Message = deleted.IsDelete
                    ? "Field deleted successfully."
                    : "Failed to delete field.",
                DeleteAt = deleted.DeleteAt,
                DeleteBy = deleted.DeleteBy,
            };
        }
    }
}
