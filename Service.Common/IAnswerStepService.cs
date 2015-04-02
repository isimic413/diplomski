using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface IAnswerStepService
    {
        Task<List<IAnswerStep>> GetPageAsync(int pageSize, int pageNumber);

        Task<List<IAnswerStep>> GetAllAsync();
        Task<IAnswerStep> GetByIdAsync(Guid id);
        Task<int> AddAsync(IAnswerStep entity);
        Task<int> UpdateAsync(IAnswerStep entity);
        Task<int> DeleteAsync(IAnswerStep entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddUoWAsync(IAnswerStep entity);

        Task<List<IAnswerStep>> GetStepsByProblemId(Guid problemId);
    }
}
