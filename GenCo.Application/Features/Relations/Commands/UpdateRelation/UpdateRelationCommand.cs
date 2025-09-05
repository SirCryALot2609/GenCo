using GenCo.Application.DTOs.Common;
using GenCo.Application.DTOs.Relation.Requests;
using MediatR;
using GenCo.Application.DTOs.Relation.Responses;

namespace GenCo.Application.Features.Relations.Commands.UpdateRelation;
public record UpdateRelationCommand(UpdateRelationRequestDto Request)
    : IRequest<BaseResponseDto<RelationResponseDto>>;
