using System;
using System.Collections.Generic;
using System.Linq;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface ITestingAreaService
    {
        IQueryable<ITestingArea> GetAll();
        ITestingArea GetById(Guid id);
        void Add(ITestingArea entity);
        void Update(ITestingArea entity);
        void Delete(ITestingArea entity);
        void Delete(Guid id);
    }
}