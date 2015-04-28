using System;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Model
{
    public class DIModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IAnswerChoice>().To<AnswerChoice>();
            Bind<IAnswerChoicePicture>().To<AnswerChoicePicture>();
            Bind<IAnswerStep>().To<AnswerStep>();
            Bind<IAnswerStepPicture>().To<AnswerStepPicture>();
            Bind<IExam>().To<Exam>();
            Bind<IExamQuestion>().To<ExamQuestion>();
            Bind<IQuestion>().To<Question>();
            Bind<IQuestionPicture>().To<QuestionPicture>();
            Bind<IQuestionType>().To<QuestionType>();
            Bind<IRole>().To<Role>();
            Bind<ITestingArea>().To<TestingArea>();
            Bind<IUser>().To<User>();
            Bind<IUserAnswer>().To<UserAnswer>();
            Bind<IUserRole>().To<UserRole>();
        }
    }
}
