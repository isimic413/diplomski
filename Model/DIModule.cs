using System;
using ExamPreparation.DAL;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Model
{
    public class DIModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IExamPreparationContext>().To<ExamPreparationContext>();

            Bind<IAnswerChoice>().To<AnswerChoice>();
            Bind<IAnswerChoicePicture>().To<AnswerChoicePicture>();
            Bind<IAnswerStep>().To<AnswerStep>();
            Bind<IAnswerStepPicture>().To<AnswerStepPicture>();
            Bind<IExam>().To<Exam>();
            Bind<IExamProblem>().To<ExamProblem>();
            Bind<IProblem>().To<Problem>();
            Bind<IProblemPicture>().To<ProblemPicture>();
            Bind<IProblemType>().To<ProblemType>();
            Bind<IRole>().To<Role>();
            Bind<ITestingArea>().To<TestingArea>();
            Bind<ITestingAreaProblem>().To<TestingAreaProblem>();
            Bind<IUser>().To<User>();
            Bind<IUserAnswer>().To<UserAnswer>();
            Bind<IUserRole>().To<UserRole>();
        }
    }
}
