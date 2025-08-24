using AutoMapper;
using GenCo.Application.DTOs.Entity.Requests;
using GenCo.Application.DTOs.Entity.Responses;
using GenCo.Application.DTOs.Field.Requests;
using GenCo.Application.DTOs.Field.Responses;
using GenCo.Application.DTOs.Project;
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
            CreateMap<Project, ProjectResponseDto>();
            CreateMap<Project, ProjectDetailDto>()
                .ForMember(dest => dest.Entities, opt => opt.MapFrom(src => src.Entities))
                .ForMember(dest => dest.Relations, opt => opt.MapFrom(src => src.Relations))
                .ForMember(dest => dest.Workflows, opt => opt.MapFrom(src => src.Workflows))
                .ForMember(dest => dest.UIConfigs, opt => opt.MapFrom(src => src.UIConfigs))
                .ForMember(dest => dest.ServiceConfigs, opt => opt.MapFrom(src => src.ServiceConfigs));
            CreateMap<Project, ProjectListItemDto>();
            CreateMap<ProjectResponseDto, Project>();
            CreateMap<ProjectDetailDto, Project>();
            CreateMap<ProjectListItemDto, Project>();

            // -------- Entity --------
            CreateMap<CreateEntityRequestDto, Entity>();
            CreateMap<UpdateEntityRequestDto, Entity>();
            CreateMap<Entity, DTOs.Entity.Responses.EntityResponseDto>();
            CreateMap<Entity, CreateEntityResponseDto>();
            CreateMap<Entity, UpdateEntityResponseDto>();

            // -------- Field --------
            CreateMap<CreateFieldRequestDto, Field>();
            CreateMap<UpdateFieldRequestDto, Field>();
            CreateMap<Field, DTOs.Field.Responses.FieldResponseDto>().ReverseMap();

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
