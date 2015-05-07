using ExamPreparation.Model;
using ExamPreparation.Model.Common;
using ExamPreparation.WebApi.Controllers;

namespace ExamPreparation.WebApi.App_Start
{
    public static class AutoMapperConfig
    {
        public static void Initialize()
        {
            // Model
            ExamPreparation.Model.Mapping.AutoMapperMaps.Initialize();

            // AnswerChoiceContorller
            AutoMapper.Mapper.CreateMap<AnswerChoiceController.AnswerChoiceModel, AnswerChoice>().ReverseMap();
            AutoMapper.Mapper.CreateMap<AnswerChoiceController.AnswerChoiceModel, IAnswerChoice>().ReverseMap();
            AutoMapper.Mapper.CreateMap<AnswerChoiceController.AnswerChoicePictureModel, AnswerChoicePicture>().ReverseMap();
            AutoMapper.Mapper.CreateMap<AnswerChoiceController.AnswerChoicePictureModel, IAnswerChoicePicture>().ReverseMap();

            // AnswerStepController
            AutoMapper.Mapper.CreateMap<AnswerStepController.AnswerStepModel, AnswerStep>().ReverseMap();
            AutoMapper.Mapper.CreateMap<AnswerStepController.AnswerStepModel, IAnswerStep>().ReverseMap();
            AutoMapper.Mapper.CreateMap<AnswerStepController.AnswerStepPictureModel, AnswerStepPicture>().ReverseMap();
            AutoMapper.Mapper.CreateMap<AnswerStepController.AnswerStepPictureModel, IAnswerStepPicture>().ReverseMap();

            // ExamController
            AutoMapper.Mapper.CreateMap<ExamController.ExamModel, Exam>().ReverseMap();
            AutoMapper.Mapper.CreateMap<ExamController.ExamModel, IExam>().ReverseMap();

            // ExamQuestionController
            AutoMapper.Mapper.CreateMap<ExamQuestionController.ExamQuestionModel, ExamQuestion>().ReverseMap();
            AutoMapper.Mapper.CreateMap<ExamQuestionController.ExamQuestionModel, IExamQuestion>().ReverseMap();
           
            // PictureController

            // QuestionController
            AutoMapper.Mapper.CreateMap<QuestionController.QuestionModel, Question>().ReverseMap();
            AutoMapper.Mapper.CreateMap<QuestionController.QuestionModel, IQuestion>().ReverseMap();
            AutoMapper.Mapper.CreateMap<QuestionController.QuestionPictureModel, QuestionPicture>().ReverseMap();
            AutoMapper.Mapper.CreateMap<QuestionController.QuestionPictureModel, IQuestionPicture>().ReverseMap();

            // QuestionTypeController
            AutoMapper.Mapper.CreateMap<QuestionTypeController.QuestionTypeModel, QuestionType>().ReverseMap();
            AutoMapper.Mapper.CreateMap<QuestionTypeController.QuestionTypeModel, IQuestionType>().ReverseMap();

            // RoleController
            AutoMapper.Mapper.CreateMap<RoleController.RoleModel, Role>().ReverseMap();
            AutoMapper.Mapper.CreateMap<RoleController.RoleModel, IRole>().ReverseMap();

            // TestingAreaController
            AutoMapper.Mapper.CreateMap<TestingAreaController.TestingAreaModel, TestingArea>().ReverseMap();
            AutoMapper.Mapper.CreateMap<TestingAreaController.TestingAreaModel, ITestingArea>().ReverseMap();

            // UserAnswerController

            // UserController



        }
    }
}