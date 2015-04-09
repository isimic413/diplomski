using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IExamRepository
    {
        Task<List<IExam>> GetAsync(string sortOrder = "yearAsc", int pageNumber = 0, int pageSize = 50);
        Task<IExam> GetAsync(Guid id);
        Task<int> AddAsync(IExam entity);
        Task<int> UpdateAsync(IExam entity);
        Task<int> DeleteAsync(IExam entity);
        Task<int> DeleteAsync(Guid id);
    }
}
