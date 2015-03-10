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

            Bind<ITestingAreaRepository>().To<TestingAreaRepository>();
        }
    }
}
