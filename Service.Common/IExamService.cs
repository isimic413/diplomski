using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface IExamService
    {
        Task<List<IExam>> GetAsync(string sortOrder = "yearAsc", int pageNumber = 0, int pageSize = 50);
        Task<IExam> GetAsync(Guid id);
        Task<int> AddAsync(IExam entity);
        Task<int> UpdateAsync(IExam entity);
        Task<int> DeleteAsync(IExam entity);
        Task<int> DeleteAsync(Guid id);

        Task<List<IExam>> GetByYear(int year);
        Task<List<IExam>> GetByTestingArea(Guid testingAreaId);
    }
}
