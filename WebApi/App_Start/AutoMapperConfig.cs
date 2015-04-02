using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ExamPreparation.Model;
using ExamPreparation.WebApi.Controllers;

namespace ExamPreparation.WebApi.App_Start
{
    public static class AutoMapperConfig
    {
        public static void Initialize()
        {
            ExamPreparation.Model.Mapping.AutoMapperMaps.Initialize();

            AutoMapper.Mapper.CreateMap<AnswerChoiceModel, AnswerChoice>().ReverseMap();
            AutoMapper.Mapper.CreateMap<AnswerStepModel, AnswerStep>().ReverseMap();
            AutoMapper.Mapper.CreateMap<ExamModel, Exam>().ReverseMap();
            AutoMapper.Mapper.CreateMap<ExamProblemModel, ExamProblem>().ReverseMap();
            AutoMapper.Mapper.CreateMap<ProblemModel, Problem>().ReverseMap();
            AutoMapper.Mapper.CreateMap<ProblemTypeModel, ProblemType>().ReverseMap();
            AutoMapper.Mapper.CreateMap<RoleModel, Role>().ReverseMap();
            AutoMapper.Mapper.CreateMap<TestingAreaModel, TestingArea>().ReverseMap();
        }
    }
}