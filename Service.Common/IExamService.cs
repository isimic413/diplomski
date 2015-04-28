using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface IExamService
    {
        Task<List<IExam>> GetAsync(ExamFilter filter);
        Task<IExam> GetAsync(Guid id);
        Task<int> AddAsync(IExam entity);
        Task<int> UpdateAsync(IExam entity);
        Task<int> DeleteAsync(IExam entity);
        Task<int> DeleteAsync(Guid id);

        Task<List<IExam>> GetByYearAsync(int year, ExamFilter filter);
        Task<List<IExam>> GetByTestingAreaIdAsync(Guid testingAreaId, ExamFilter filter);
    }
}
