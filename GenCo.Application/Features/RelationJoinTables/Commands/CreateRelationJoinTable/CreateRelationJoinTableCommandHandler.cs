using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.RelationJoinTable.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.RelationJoinTables.Commands.CreateRelationJoinTable;

public class CreateRelationJoinTableCommandHandler(
    IGenericRepository<RelationJoinTable> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<CreateRelationJoinTableCommand, BaseResponseDto<RelationJoinTableResponseDto>>
{
    public async Task<BaseResponseDto<RelationJoinTableResponseDto>> Handle(CreateRelationJoinTableCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<RelationJoinTable>(request.Request);
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = null;

        await repository.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = mapper.Map<RelationJoinTableResponseDto>(entity);
        return BaseResponseDto<RelationJoinTableResponseDto>.Ok(dto, "RelationJoinTable created successfully");
    }
}