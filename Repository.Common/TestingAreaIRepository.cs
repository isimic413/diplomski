using System;
using System.Collections.Generic;
using System.Linq;
using ExamPreparation.DAL.Models;

namespace ExamPreparation.Repository.Common
{
    public interface TestingAreaIRepository
    {
        IQueryable<TestingArea> GetAll();
        TestingArea GetById(Guid id);
        void Add(TestingArea entity);
        void Update(TestingArea entity);
        void Delete(TestingArea entity);
        void Delete(Guid id);
    }
}
