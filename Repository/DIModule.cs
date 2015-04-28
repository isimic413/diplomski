using System;
using Ninject.Extensions.Factory;
using ExamPreparation.DAL.Models;
using ExamPreparation.Model;
using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;

namespace ExamPreparation.Repository
{
    public class DIModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IExamPreparationContext>().To<ExamPreparationContext>();
            Bind<IRepository>().To<Repository>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IUnitOfWorkFactory>().ToFactory();

            Bind<IAnswerChoiceRepository>().To<AnswerChoiceRepository>();
            Bind<IAnswerChoicePictureRepository>().To<AnswerChoicePictureRepository>();
            Bind<IAnswerStepRepository>().To<AnswerStepRepository>();
            Bind<IAnswerStepPictureRepository>().To<AnswerStepPictureRepository>();
            Bind<IExamRepository>().To<ExamRepository>();
            Bind<IExamQuestionRepository>().To<ExamQuestionRepository>();
            Bind<IQuestionRepository>().To<QuestionRepository>();
            Bind<IQuestionPictureRepository>().To<QuestionPictureRepository>();
            Bind<IQuestionTypeRepository>().To<QuestionTypeRepository>();
            Bind<IRoleRepository>().To<RoleRepository>();
            Bind<ITestingAreaRepository>().To<TestingAreaRepository>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IUserAnswerRepository>().To<UserAnswerRepository>();
            Bind<IUserRoleRepository>().To<UserRoleRepository>();
        }
    }
}
