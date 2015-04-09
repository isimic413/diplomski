using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IExamProblemRepository
    {
        Task<List<IExamProblem>> GetAsync(string sortOrder = "examProblemId", int pageNumber = 0, int pageSize = 50);
        Task<IExamProblem> GetAsync(Guid id);
        Task<int> AddAsync(IExamProblem entity);
        Task<int> UpdateAsync(IExamProblem entity);
        Task<int> DeleteAsync(IExamProblem entity);
        Task<int> DeleteAsync(Guid id);
    }
}
