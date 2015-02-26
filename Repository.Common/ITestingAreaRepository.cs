using System;
using System.Collections.Generic;
using System.Linq;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface ITestingAreaRepository
    {
        IQueryable<ITestingArea> GetAll();
        ITestingArea GetById(Guid id);
        void Add(ITestingArea entity);
        void Update(ITestingArea entity);
        void Delete(ITestingArea entity);
        void Delete(Guid id);
    }
}
