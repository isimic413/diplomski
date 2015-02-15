using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamPreparation.Repository;
using ExamPreparation.Repository.Common;
using Ninject;

namespace ExamPreparation.Service
{
    public class TestingAreaService // !
    {
        protected UnitOfWork UnitOfWork;
        protected ITestingAreaRepository Repository;

        Ninject.IKernel kernel = new StandardKernel(new Binding());

        public TestingAreaService(ITestingAreaRepository repository)
        {
            Repository = repository;
        }

        public void GetByAbrv(string abrv) // !
        {
           // Repository.GetAll().Where(c => c.Abrv.Equals(abrv));
        }

        public void GetByTitle(string title) // !
        {
        }

    }
}
