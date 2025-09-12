using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.RelationJoinTable.Responses;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;

namespace GenCo.Application.Features.RelationJoinTables.Commands.UpdateRelationJoinTable;


public class UpdateRelationJoinTableCommandHandler(
    IGenericRepository<RelationJoinTable> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<UpdateRelationJoinTableCommand, BaseResponseDto<RelationJoinTableResponseDto>>
{
    public async Task<BaseResponseDto<RelationJoinTableResponseDto>> Handle(UpdateRelationJoinTableCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Request.Id, cancellationToken: cancellationToken);
        if (entity == null)
            return BaseResponseDto<RelationJoinTableResponseDto>.Fail("RelationJoinTable not found");

        mapper.Map(request.Request, entity);
        entity.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = mapper.Map<RelationJoinTableResponseDto>(entity);
        return BaseResponseDto<RelationJoinTableResponseDto>.Ok(dto, "RelationJoinTable updated successfully");
    }
}