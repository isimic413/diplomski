using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface IAnswerChoiceService
    {
        Task<List<IAnswerChoice>> GetPageAsync(int pageSize, int pageNumber);

        Task<List<IAnswerChoice>> GetAllAsync();
        Task<IAnswerChoice> GetByIdAsync(Guid id);
        Task<int> AddAsync(IAnswerChoice entity);
        Task<int> UpdateAsync(IAnswerChoice entity);
        Task<int> DeleteAsync(IAnswerChoice entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddUoWAsync(IAnswerChoice entity);

        Task<IAnswerChoice> GetCorrectAnswer(Guid problemId);
        Task<List<IAnswerChoice>> GetChoicesByProblemId(Guid problemId);
    }
}
