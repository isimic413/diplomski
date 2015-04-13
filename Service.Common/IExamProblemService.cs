using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface IExamProblemService
    {
        Task<List<IExamProblem>> GetAsync(string sortOrder = "examProblemId", int pageNumber = 0, int pageSize = 50);
        Task<IExamProblem> GetAsync(Guid id);
        Task<int> AddAsync(IExamProblem entity);
        Task<int> UpdateAsync(IExamProblem entity);
        Task<int> DeleteAsync(IExamProblem entity);
        Task<int> DeleteAsync(Guid id);

        Task<List<IExamProblem>> GetByExamAsync(Guid examId);
    }
}
