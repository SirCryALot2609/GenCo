using AutoMapper;
using GenCo.Application.DTOs.Entity.Request;
using GenCo.Application.DTOs.Entity.Requests;
using GenCo.Application.DTOs.Entity.Responses;
using GenCo.Application.DTOs.Field.Requests;
using GenCo.Application.DTOs.Field.Responses;
using GenCo.Application.DTOs.Project.Requests;
using GenCo.Application.DTOs.Project.Responses;
using GenCo.Application.DTOs.Relation.Requests;
using GenCo.Application.DTOs.Relation.Responses;
using GenCo.Application.DTOs.ServiceConfig.Requests;
using GenCo.Application.DTOs.ServiceConfig.Responses;
using GenCo.Application.DTOs.UIConfig.Requests;
using GenCo.Application.DTOs.UIConfig.Responses;
using GenCo.Application.DTOs.Workflow.Requests;
using GenCo.Application.DTOs.Workflow.Responses;
using GenCo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCo.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // -------- Project --------
            CreateMap<CreateProjectRequestDto, Project>();
            CreateMap<UpdateProjectRequestDto, Project>();
            CreateMap<Project, ProjectResponseDto>().ReverseMap();
            CreateMap<Project, ProjectListResponseDto>().ReverseMap();

            // -------- Entity --------
            CreateMap<CreateEntityRequestDto, Entity>();
            CreateMap<UpdateEntityRequestDto, Entity>();
            CreateMap<Entity, EntityResponseDto>();
            CreateMap<Entity, CreateEntityResponseDto>();
            CreateMap<Entity, UpdateEntityResponseDto>();

            // -------- Field --------
            CreateMap<CreateFieldRequestDto, Field>();
            CreateMap<UpdateFieldRequestDto, Field>();
            CreateMap<Field, FieldResponseDto>().ReverseMap();

            // -------- Relation --------
            CreateMap<CreateRelationRequestDto, Relation>();
            CreateMap<UpdateRelationRequestDto, Relation>();
            CreateMap<Relation, RelationResponseDto>().ReverseMap();
            CreateMap<Relation, RelationSummaryResponseDto>().ReverseMap();

            // -------- Workflow --------
            CreateMap<CreateWorkflowRequestDto, Workflow>();
            CreateMap<UpdateWorkflowRequestDto, Workflow>();
            CreateMap<Workflow, WorkflowResponseDto>().ReverseMap();
            CreateMap<Workflow, WorkflowSummaryResponseDto>().ReverseMap();

            // -------- UIConfig --------
            CreateMap<CreateUIConfigRequestDto, UIConfig>();
            CreateMap<UpdateUIConfigRequestDto, UIConfig>();
            CreateMap<UIConfig, UIConfigResponseDto>().ReverseMap();
            CreateMap<UIConfig, UIConfigSummaryResponseDto>().ReverseMap();

            // -------- ServiceConfig --------
            CreateMap<CreateServiceConfigRequestDto, ServiceConfig>();
            CreateMap<UpdateServiceConfigRequestDto, ServiceConfig>();
            CreateMap<ServiceConfig, ServiceConfigResponseDto>().ReverseMap();
            CreateMap<ServiceConfig, ServiceConfigSummaryResponseDto>().ReverseMap();

            // -------- Log --------
            //CreateMap<CreateLogRequestDto, Log>();
            //CreateMap<UpdateLogRequestDto, Log>();
            //CreateMap<Log, LogResponseDto>().ReverseMap();
            //CreateMap<Log, LogSummaryResponseDto>().ReverseMap();
        }
    }
}
