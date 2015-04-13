using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface IAnswerChoiceService
    {
        Task<List<IAnswerChoice>> GetAsync(string sortOrder = "problemId", int pageNumber = 0, int pageSize = 50);
        Task<IAnswerChoice> GetAsync(Guid id);
        Task<int> AddAsync(IAnswerChoice entity, IAnswerChoicePicture picture = null);
        Task<int> UpdateAsync(IAnswerChoice entity, IAnswerChoicePicture picture = null);
        Task<int> DeleteAsync(IAnswerChoice entity);
        Task<int> DeleteAsync(Guid id);

        Task<IAnswerChoice> GetCorrectAnswerAsync(Guid problemId);
        Task<List<IAnswerChoice>> GetChoicesAsync(Guid problemId);
    }
}
