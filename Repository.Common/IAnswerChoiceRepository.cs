using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IAnswerChoiceRepository
    {
        Task<List<IAnswerChoice>> GetAsync(AnswerChoiceFilter filter);
        Task<IAnswerChoice> GetAsync(Guid id);
        Task<List<IAnswerChoice>> GetCorrectAnswersAsync(Guid questionId);
        Task<List<IAnswerChoice>> GetChoicesAsync(Guid questionId);

        Task<int> InsertAsync(IAnswerChoice entity, IAnswerChoicePicture picture = null);

        Task<int> UpdateAsync(IAnswerChoice entity, IAnswerChoicePicture picture = null);

        Task<int> DeleteAsync(IAnswerChoice entity);
        Task<int> DeleteAsync(Guid id);
    }
}
