using ExamPreparation.Model.Common;

namespace ExamPreparation.Model.Mapping
{
    public static class AutoMapperMaps
    {
        public static void Initialize()
        {
            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.AnswerChoice, ExamPreparation.DAL.Models.AnswerChoice>().ReverseMap();
            AutoMapper.Mapper.CreateMap<IAnswerChoice, ExamPreparation.DAL.Models.AnswerChoice>().ReverseMap();

            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.AnswerChoicePicture, ExamPreparation.DAL.Models.AnswerChoicePicture>().ReverseMap();
            AutoMapper.Mapper.CreateMap<IAnswerChoicePicture, ExamPreparation.DAL.Models.AnswerChoicePicture>().ReverseMap();

            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.AnswerStep, ExamPreparation.DAL.Models.AnswerStep>().ReverseMap();
            AutoMapper.Mapper.CreateMap<IAnswerStep, ExamPreparation.DAL.Models.AnswerStep>().ReverseMap();

            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.AnswerStepPicture, ExamPreparation.DAL.Models.AnswerStepPicture>().ReverseMap();
            AutoMapper.Mapper.CreateMap<IAnswerStepPicture, ExamPreparation.DAL.Models.AnswerStepPicture>().ReverseMap();

            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.Exam, ExamPreparation.DAL.Models.Exam>().ReverseMap();
            AutoMapper.Mapper.CreateMap<IExam, ExamPreparation.DAL.Models.Exam>().ReverseMap();

            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.ExamQuestion, ExamPreparation.DAL.Models.ExamQuestion>().ReverseMap();
            AutoMapper.Mapper.CreateMap<IExamQuestion, ExamPreparation.DAL.Models.ExamQuestion>().ReverseMap();

            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.Question, ExamPreparation.DAL.Models.Question>().ReverseMap();
            AutoMapper.Mapper.CreateMap<IQuestion, ExamPreparation.DAL.Models.Question>().ReverseMap();

            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.QuestionPicture, ExamPreparation.DAL.Models.QuestionPicture>().ReverseMap();
            AutoMapper.Mapper.CreateMap<IQuestionPicture, ExamPreparation.DAL.Models.QuestionPicture>().ReverseMap();

            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.QuestionType, ExamPreparation.DAL.Models.QuestionType>().ReverseMap();
            AutoMapper.Mapper.CreateMap<IQuestionType, ExamPreparation.DAL.Models.QuestionType>().ReverseMap();

            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.Role, ExamPreparation.DAL.Models.Role>().ReverseMap();
            AutoMapper.Mapper.CreateMap<IRole, ExamPreparation.DAL.Models.Role>().ReverseMap();

            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.TestingArea, ExamPreparation.DAL.Models.TestingArea>().ReverseMap();
            AutoMapper.Mapper.CreateMap<ITestingArea, ExamPreparation.DAL.Models.TestingArea>().ReverseMap();

            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.User, ExamPreparation.DAL.Models.User>().ReverseMap();
            AutoMapper.Mapper.CreateMap<IUser, ExamPreparation.DAL.Models.User>().ReverseMap();

            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.UserAnswer, ExamPreparation.DAL.Models.UserAnswer>().ReverseMap();
            AutoMapper.Mapper.CreateMap<IUserAnswer, ExamPreparation.DAL.Models.UserAnswer>().ReverseMap();

            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.UserRole, ExamPreparation.DAL.Models.UserRole>().ReverseMap();
            AutoMapper.Mapper.CreateMap<IUserRole, ExamPreparation.DAL.Models.UserRole>().ReverseMap();
        }

    }
}
