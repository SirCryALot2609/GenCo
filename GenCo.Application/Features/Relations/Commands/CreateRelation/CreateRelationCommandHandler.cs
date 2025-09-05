using AutoMapper;
using GenCo.Application.DTOs.Common;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Domain.Entities;
using MediatR;
using GenCo.Application.DTOs.Relation.Responses;

namespace GenCo.Application.Features.Relations.Commands.CreateRelation;
public class CreateRelationCommandHandler(
    IGenericRepository<Relation> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<CreateRelationCommand, BaseResponseDto<RelationResponseDto>>
{
    public async Task<BaseResponseDto<RelationResponseDto>> Handle(
        CreateRelationCommand request,
        CancellationToken cancellationToken)
    {
        var relation = mapper.Map<Relation>(request.Request);
        relation.Id = Guid.NewGuid();
        relation.CreatedAt = DateTime.UtcNow;
        relation.UpdatedAt = null;

        await repository.AddAsync(relation, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = mapper.Map<RelationResponseDto>(relation);
        return BaseResponseDto<RelationResponseDto>.Ok(dto, "Relation created successfully");
    }
}
