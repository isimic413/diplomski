using System;
using ExamPreparation.Repository.Common;

namespace ExamPreparation.Repository
{
    public class DIModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository>().To<Repository>();

            Bind<ITestingAreaRepository>().To<TestingAreaRepository>();
        }
    }
}
