using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExamPreparation.DAL;
using ExamPreparation.DAL.Models;
using ExamPreparation.DAL.Common;
using ExamPreparation.Repository.Common;

namespace ExamPreparation.Repository
{
    public class Binding : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            //Bind<ExamPreparationContext>().To<ExamPreparationContext>();

            Bind<ITestingArea>().To<TestingArea>();

            Bind<IRepository>().To<Repository>();
            Bind<ITestingAreaRepository>().To<TestingAreaRepository>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}
