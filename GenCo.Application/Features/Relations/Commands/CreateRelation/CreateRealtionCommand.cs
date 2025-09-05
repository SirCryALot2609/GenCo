using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Relation.Requests;
using GenCo.Application.DTOs.Relation.Responses;
using MediatR;

namespace GenCo.Application.Features.Relations.Commands.CreateRelation;
public record CreateRelationCommand(CreateRelationRequestDto Request)
    : IRequest<BaseResponseDto<RelationResponseDto>>;
