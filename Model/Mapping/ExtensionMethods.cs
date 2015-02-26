using AutoMapper;
using DAL = ExamPreparation.DAL;
using ModelCommon = ExamPreparation.Model;
using System.Linq;

namespace ExamPreparation.Model.Mapping
{
    public static class ExtensionMethods
    {
            //Mapper.CreateMap<ModelCommon.AnswerChoice, IAnswerChoice>();
            //Mapper.CreateMap<AnswerChoicePicture, IAnswerChoicePicture>();
            //Mapper.CreateMap<AnswerStep, IAnswerStep>();
            //Mapper.CreateMap<AnswerStepPicture, IAnswerStepPicture>();
            //Mapper.CreateMap<Exam, IExam>();
            //Mapper.CreateMap<ExamProblem, IExamProblem>();
            //Mapper.CreateMap<Problem, IProblem>();
            //Mapper.CreateMap<ProblemPicture, IProblemPicture>();
            //Mapper.CreateMap<ProblemType, IProblemType>();
            //Mapper.CreateMap<Role, IRole>();
            //Mapper.CreateMap<TestingArea, ITestingArea>();
            //Mapper.CreateMap<TestingAreaProblem, ITestingAreaProblem>();
            //Mapper.CreateMap<User, IUser>();
            //Mapper.CreateMap<UserAnswer, IUserAnswer>();
            //Mapper.CreateMap<UserRole, IUserRole>();

        // AnswerChoice
        public static ModelCommon.AnswerChoice ConvertToModelCommon(this DAL.Models.AnswerChoice answerChoice)
        {
            return Mapper.Map<DAL.Models.AnswerChoice, ModelCommon.AnswerChoice>(answerChoice);
        }

        public static IQueryable<ModelCommon.AnswerChoice> ConvertToModelCommon(this IQueryable<DAL.Models.AnswerChoice> answerChoice)
        {
            return Mapper.Map<IQueryable<DAL.Models.AnswerChoice>, IQueryable<ModelCommon.AnswerChoice>>(answerChoice);
        }
        public static DAL.Models.AnswerChoice ConvertToDalModel(this ModelCommon.AnswerChoice answerChoice)
        {
            return Mapper.Map<ModelCommon.AnswerChoice, DAL.Models.AnswerChoice>(answerChoice);
        }


        // AnswerChoicePicture
        public static ModelCommon.AnswerChoicePicture ConvertToModelCommon(this DAL.Models.AnswerChoicePicture answerChoicePicture)
        {
            return Mapper.Map<DAL.Models.AnswerChoicePicture, ModelCommon.AnswerChoicePicture>(answerChoicePicture);
        }

        public static IQueryable<ModelCommon.AnswerChoicePicture> ConvertToModelCommon(this IQueryable<DAL.Models.AnswerChoicePicture> answerChoicePicture)
        {
            return Mapper.Map<IQueryable<DAL.Models.AnswerChoicePicture>, IQueryable<ModelCommon.AnswerChoicePicture>>(answerChoicePicture);
        }
        public static DAL.Models.AnswerChoicePicture ConvertToDalModel(this ModelCommon.AnswerChoicePicture answerChoicePicture)
        {
            return Mapper.Map<ModelCommon.AnswerChoicePicture, DAL.Models.AnswerChoicePicture>(answerChoicePicture);
        }


        // AnswerStep
        public static ModelCommon.AnswerStep ConvertToModelCommon(this DAL.Models.AnswerStep answerStep)
        {
            return Mapper.Map<DAL.Models.AnswerStep, ModelCommon.AnswerStep>(answerStep);
        }

        public static IQueryable<ModelCommon.AnswerStep> ConvertToModelCommon(this IQueryable<DAL.Models.AnswerStep> answerStep)
        {
            return Mapper.Map<IQueryable<DAL.Models.AnswerStep>, IQueryable<ModelCommon.AnswerStep>>(answerStep);
        }
        public static DAL.Models.AnswerStep ConvertToDalModel(this ModelCommon.AnswerStep answerStep)
        {
            return Mapper.Map<ModelCommon.AnswerStep, DAL.Models.AnswerStep>(answerStep);
        }


        // AnswerStepPicture
        public static ModelCommon.AnswerStepPicture ConvertToModelCommon(this DAL.Models.AnswerStepPicture answerStepPicture)
        {
            return Mapper.Map<DAL.Models.AnswerStepPicture, ModelCommon.AnswerStepPicture>(answerStepPicture);
        }

        public static IQueryable<ModelCommon.AnswerStepPicture> ConvertToModelCommon(this IQueryable<DAL.Models.AnswerStepPicture> answerStepPicture)
        {
            return Mapper.Map<IQueryable<DAL.Models.AnswerStepPicture>, IQueryable<ModelCommon.AnswerStepPicture>>(answerStepPicture);
        }
        public static DAL.Models.AnswerStepPicture ConvertToDalModel(this ModelCommon.AnswerStepPicture answerStepPicture)
        {
            return Mapper.Map<ModelCommon.AnswerStepPicture, DAL.Models.AnswerStepPicture>(answerStepPicture);
        }


        // Exam
        public static ModelCommon.Exam ConvertToModelCommon(this DAL.Models.Exam exam)
        {
            return Mapper.Map<DAL.Models.Exam, ModelCommon.Exam>(exam);
        }

        public static IQueryable<ModelCommon.Exam> ConvertToModelCommon(this IQueryable<DAL.Models.Exam> exam)
        {
            return Mapper.Map<IQueryable<DAL.Models.Exam>, IQueryable<ModelCommon.Exam>>(exam);
        }
        public static DAL.Models.Exam ConvertToDalModel(this ModelCommon.Exam exam)
        {
            return Mapper.Map<ModelCommon.Exam, DAL.Models.Exam>(exam);
        }


        // ExamProblem
        public static ModelCommon.ExamProblem ConvertToModelCommon(this DAL.Models.ExamProblem examProblem)
        {
            return Mapper.Map<DAL.Models.ExamProblem, ModelCommon.ExamProblem>(examProblem);
        }

        public static IQueryable<ModelCommon.ExamProblem> ConvertToModelCommon(this IQueryable<DAL.Models.ExamProblem> examProblem)
        {
            return Mapper.Map<IQueryable<DAL.Models.ExamProblem>, IQueryable<ModelCommon.ExamProblem>>(examProblem);
        }
        public static DAL.Models.ExamProblem ConvertToDalModel(this ModelCommon.ExamProblem examProblem)
        {
            return Mapper.Map<ModelCommon.ExamProblem, DAL.Models.ExamProblem>(examProblem);
        }


        // Problem
        public static ModelCommon.Problem ConvertToModelCommon(this DAL.Models.Problem problem)
        {
            return Mapper.Map<DAL.Models.Problem, ModelCommon.Problem>(problem);
        }

        public static IQueryable<ModelCommon.Problem> ConvertToModelCommon(this IQueryable<DAL.Models.Problem> problem)
        {
            return Mapper.Map<IQueryable<DAL.Models.Problem>, IQueryable<ModelCommon.Problem>>(problem);
        }
        public static DAL.Models.Problem ConvertToDalModel(this ModelCommon.Problem problem)
        {
            return Mapper.Map<ModelCommon.Problem, DAL.Models.Problem>(problem);
        }


        // ProblemPicture
        public static ModelCommon.ProblemPicture ConvertToModelCommon(this DAL.Models.ProblemPicture problemPicture)
        {
            return Mapper.Map<DAL.Models.ProblemPicture, ModelCommon.ProblemPicture>(problemPicture);
        }

        public static IQueryable<ModelCommon.ProblemPicture> ConvertToModelCommon(this IQueryable<DAL.Models.ProblemPicture> problemPicture)
        {
            return Mapper.Map<IQueryable<DAL.Models.ProblemPicture>, IQueryable<ModelCommon.ProblemPicture>>(problemPicture);
        }
        public static DAL.Models.ProblemPicture ConvertToDalModel(this ModelCommon.ProblemPicture problemPicture)
        {
            return Mapper.Map<ModelCommon.ProblemPicture, DAL.Models.ProblemPicture>(problemPicture);
        }


        // ProblemType
        public static ModelCommon.ProblemType ConvertToModelCommon(this DAL.Models.ProblemType problemType)
        {
            return Mapper.Map<DAL.Models.ProblemType, ModelCommon.ProblemType>(problemType);
        }

        public static IQueryable<ModelCommon.ProblemType> ConvertToModelCommon(this IQueryable<DAL.Models.ProblemType> problemType)
        {
            return Mapper.Map<IQueryable<DAL.Models.ProblemType>, IQueryable<ModelCommon.ProblemType>>(problemType);
        }
        public static DAL.Models.ProblemType ConvertToDalModel(this ModelCommon.ProblemType problemType)
        {
            return Mapper.Map<ModelCommon.ProblemType, DAL.Models.ProblemType>(problemType);
        }


        // Role
        public static ModelCommon.Role ConvertToModelCommon(this DAL.Models.Role role)
        {
            return Mapper.Map<DAL.Models.Role, ModelCommon.Role>(role);
        }

        public static IQueryable<ModelCommon.Role> ConvertToModelCommon(this IQueryable<DAL.Models.Role> role)
        {
            return Mapper.Map<IQueryable<DAL.Models.Role>, IQueryable<ModelCommon.Role>>(role);
        }
        public static DAL.Models.Role ConvertToDalModel(this ModelCommon.Role role)
        {
            return Mapper.Map<ModelCommon.Role, DAL.Models.Role>(role);
        }


        // TestingArea
        public static ModelCommon.TestingArea ConvertToModelCommon(this DAL.Models.TestingArea testingArea)
        {
            return Mapper.Map<DAL.Models.TestingArea, ModelCommon.TestingArea>(testingArea);
        }

        public static IQueryable<ModelCommon.TestingArea> ConvertToModelCommon(this IQueryable<DAL.Models.TestingArea> testingArea)
        {
            return Mapper.Map<IQueryable<DAL.Models.TestingArea>, IQueryable<ModelCommon.TestingArea>>(testingArea);
        }

        public static DAL.Models.TestingArea ConvertToDalModel(this ModelCommon.TestingArea testingArea)
        {
            return Mapper.Map<ModelCommon.TestingArea, DAL.Models.TestingArea>(testingArea);
        }


        // TestingAreaProblem
        public static ModelCommon.TestingAreaProblem ConvertToModelCommon(this DAL.Models.TestingAreaProblem testingAreaProblem)
        {
            return Mapper.Map<DAL.Models.TestingAreaProblem, ModelCommon.TestingAreaProblem>(testingAreaProblem);
        }

        public static IQueryable<ModelCommon.TestingAreaProblem> ConvertToModelCommon(this IQueryable<DAL.Models.TestingAreaProblem> testingAreaProblem)
        {
            return Mapper.Map<IQueryable<DAL.Models.TestingAreaProblem>, IQueryable<ModelCommon.TestingAreaProblem>>(testingAreaProblem);
        }
        public static DAL.Models.TestingAreaProblem ConvertToDalModel(this ModelCommon.TestingAreaProblem testingAreaProblem)
        {
            return Mapper.Map<ModelCommon.TestingAreaProblem, DAL.Models.TestingAreaProblem>(testingAreaProblem);
        }

        // User
        public static ModelCommon.User ConvertToModelCommon(this DAL.Models.User user)
        {
            return Mapper.Map<DAL.Models.User, ModelCommon.User>(user);
        }

        public static IQueryable<ModelCommon.User> ConvertToModelCommon(this IQueryable<DAL.Models.User> user)
        {
            return Mapper.Map<IQueryable<DAL.Models.User>, IQueryable<ModelCommon.User>>(user);
        }
        public static DAL.Models.User ConvertToDalModel(this ModelCommon.User user)
        {
            return Mapper.Map<ModelCommon.User, DAL.Models.User>(user);
        }


        // UserAnswer
        public static ModelCommon.UserAnswer ConvertToModelCommon(this DAL.Models.UserAnswer userAnswer)
        {
            return Mapper.Map<DAL.Models.UserAnswer, ModelCommon.UserAnswer>(userAnswer);
        }

        public static IQueryable<ModelCommon.UserAnswer> ConvertToModelCommon(this IQueryable<DAL.Models.UserAnswer> userAnswer)
        {
            return Mapper.Map<IQueryable<DAL.Models.UserAnswer>, IQueryable<ModelCommon.UserAnswer>>(userAnswer);
        }
        public static DAL.Models.UserAnswer ConvertToDalModel(this ModelCommon.UserAnswer userAnswer)
        {
            return Mapper.Map<ModelCommon.UserAnswer, DAL.Models.UserAnswer>(userAnswer);
        }


        // UserRole
        public static ModelCommon.UserRole ConvertToModelCommon(this DAL.Models.UserRole userRole)
        {
            return Mapper.Map<DAL.Models.UserRole, ModelCommon.UserRole>(userRole);
        }

        public static IQueryable<ModelCommon.UserRole> ConvertToModelCommon(this IQueryable<DAL.Models.UserRole> userRole)
        {
            return Mapper.Map<IQueryable<DAL.Models.UserRole>, IQueryable<ModelCommon.UserRole>>(userRole);
        }
        public static DAL.Models.UserRole ConvertToDalModel(this ModelCommon.UserRole userRole)
        {
            return Mapper.Map<ModelCommon.UserRole, DAL.Models.UserRole>(userRole);
        }
    }
}
