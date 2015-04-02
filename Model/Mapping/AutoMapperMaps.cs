﻿using ExamPreparation.Model.Common;

namespace ExamPreparation.Model.Mapping
{
    public static class AutoMapperMaps
    {
        public static void Initialize()
        {
            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.AnswerChoice, IAnswerChoice>().ReverseMap();
            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.AnswerChoice, ExamPreparation.DAL.Models.AnswerChoice>().ReverseMap();

            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.AnswerChoicePicture, IAnswerChoicePicture>().ReverseMap();
            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.AnswerChoicePicture, ExamPreparation.DAL.Models.AnswerChoicePicture>().ReverseMap();

            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.AnswerStep, IAnswerStep>().ReverseMap();
            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.AnswerStep, ExamPreparation.DAL.Models.AnswerStep>().ReverseMap();

            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.AnswerStepPicture, IAnswerStepPicture>().ReverseMap();
            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.AnswerStepPicture, ExamPreparation.DAL.Models.AnswerStepPicture>().ReverseMap();

            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.Exam, IExam>().ReverseMap();
            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.Exam, ExamPreparation.DAL.Models.Exam>().ReverseMap();

            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.ExamProblem, IExamProblem>().ReverseMap();
            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.ExamProblem, ExamPreparation.DAL.Models.ExamProblem>().ReverseMap();

            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.Problem, IProblem>().ReverseMap();
            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.Problem, ExamPreparation.DAL.Models.Problem>().ReverseMap();

            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.ProblemPicture, IProblemPicture>().ReverseMap();
            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.ProblemPicture, ExamPreparation.DAL.Models.ProblemPicture>().ReverseMap();

            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.ProblemType, IProblemType>().ReverseMap();
            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.ProblemType, ExamPreparation.DAL.Models.ProblemType>().ReverseMap();

            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.Role, IRole>().ReverseMap();
            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.Role, ExamPreparation.DAL.Models.Role>().ReverseMap();

            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.TestingArea, ITestingArea>().ReverseMap();
            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.TestingArea, ExamPreparation.DAL.Models.TestingArea>().ReverseMap();

            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.User, IUser>().ReverseMap();
            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.User, ExamPreparation.DAL.Models.User>().ReverseMap();

            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.UserAnswer, IUserAnswer>().ReverseMap();
            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.UserAnswer, ExamPreparation.DAL.Models.UserAnswer>().ReverseMap();

            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.UserRole, IUserRole>().ReverseMap();
            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.UserRole, ExamPreparation.DAL.Models.UserRole>().ReverseMap();
        }

    }
}
