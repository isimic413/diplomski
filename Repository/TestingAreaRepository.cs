using System;
using System.Collections.Generic;
using System.Linq;
using ExamPreparation.DAL.Models;
using ExamPreparation.Repository;
using ExamPreparation.Repository.Common;

namespace ExamPreparation.Repository
{
    public class TestingAreaRepository : ITestingAreaRepository
    {
        protected Repository Repository { get; set; }

        public TestingAreaRepository(Repository repository)
        {
            Repository = repository;
        }

        public virtual IQueryable<TestingArea> GetAll()
        {
            return Repository.GetAll<TestingArea>();
        }

        public virtual TestingArea GetById(Guid id)
        {
            return Repository.GetById<TestingArea>(id);
        }

        public virtual void Add(TestingArea entity)
        {
            Repository.Add<TestingArea>(entity);
        }

        public virtual void Update(TestingArea entity)
        {
            Repository.Update<TestingArea>(entity);
        }

        public virtual void Delete(TestingArea entity)
        {
            Repository.Delete<TestingArea>(entity);
        }

        public virtual void Delete(Guid id)
        {
            Repository.Delete<TestingArea>(id);
        }
    }
}
