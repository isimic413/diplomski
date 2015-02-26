using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamPreparation.Model;
using ExamPreparation.Model.Common;
using ExamPreparation.Repository;
using ExamPreparation.Repository.Common;
using ExamPreparation.Service.Common;

namespace ExamPreparation.Service
{
    public class TestingAreaService : ITestingAreaService
    {
        protected ITestingAreaRepository Repository { get; set; }

        public TestingAreaService(ITestingAreaRepository repository)
        {
            Repository = repository;
        }

        public IQueryable<ITestingArea> GetAll()
        {
            return Repository.GetAll();
        }

        public ITestingArea GetById(Guid id)
        {
            return Repository.GetById(id);
        }

        public void Add(ITestingArea entity)
        {
            Repository.Add(entity);
        }

        public void Update(ITestingArea entity)
        {
            Repository.Update(entity);
        }

        public void Delete(ITestingArea entity)
        {
            Repository.Delete(entity);
        }

        public void Delete(Guid id)
        {
            Repository.Delete(id);
        }
    }
}
