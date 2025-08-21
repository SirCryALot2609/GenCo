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

namespace GenCo.Application.Features.Relations.Commands.DeleteRelation
{
    public class DeleteRelationCommandHandler(IRelationRepository repository, IMapper mapper)
        : IRequestHandler<DeleteRelationCommand, BaseDeleteResponseDto>
    {
        private readonly IRelationRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseDeleteResponseDto> Handle(DeleteRelationCommand request, CancellationToken cancellationToken)
        {
            var realation = await _repository.GetByIdAsync(request.Request.Id);
            if (realation == null)
            {
                return new BaseDeleteResponseDto
                {
                    Success = false,
                    Message = "Relation not found.",
                    DeleteAt = DateTime.UtcNow,
                    DeleteBy = "system"
                };
            }
            await _repository.DeleteAsync(realation);
            var deleted = await _repository.GetByIdAsync(realation.Id);
            return new BaseDeleteResponseDto
            {
                Success = deleted.IsDelete,
                Message = deleted.IsDelete
                    ? "Project deleted successfully."
                    : "Failed to delete project.",
                DeleteAt = deleted.DeleteAt,
                DeleteBy = deleted.DeleteBy,
            };
        }
    }
}
