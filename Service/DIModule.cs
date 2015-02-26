using System;
using ExamPreparation.Service.Common;

namespace ExamPreparation.Service
{
    public class DIModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<ITestingAreaService>().To<TestingAreaService>();
        }
    }
}
