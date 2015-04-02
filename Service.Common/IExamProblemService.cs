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
        Task<List<IExamProblem>> GetPageAsync(int pageSize, int pageNumber);

        Task<List<IExamProblem>> GetAllAsync();
        Task<IExamProblem> GetByIdAsync(Guid id);
        Task<int> AddAsync(IExamProblem entity);
        Task<int> UpdateAsync(IExamProblem entity);
        Task<int> DeleteAsync(IExamProblem entity);
        Task<int> DeleteAsync(Guid id);

        Task<List<IExamProblem>> GetExamProblemsById(Guid examId);
        Task<IExamProblem> GetExamProblemByExamId(Guid examId, int number);

        Task<int> AddUoWAsync(IExamProblem entity);
    }
}
