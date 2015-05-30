using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IAnswerChoiceRepository
    {
        Task<List<IAnswerChoice>> GetAsync(AnswerChoiceFilter filter = null);
        Task<IAnswerChoice> GetAsync(Guid id);
        Task<List<IAnswerChoice>> GetCorrectAnswersAsync(Guid questionId);
        Task<List<IAnswerChoice>> GetChoicesAsync(Guid questionId);

        Task<int> AddAsync(IUnitOfWork unitOfWork, IAnswerChoice entity);
        Task<int> AddAsync(IUnitOfWork unitOfWork, List<IAnswerChoice> entities,
            List<IAnswerChoicePicture> pictures = null);
        Task<int> InsertAsync(IAnswerChoice entity);

        Task<int> UpdateAsync(IUnitOfWork unitOfWork, IAnswerChoice entity);
        Task<int> UpdateAsync(IAnswerChoice entity);

        Task<int> DeleteAsync(IUnitOfWork unitOfWork, Guid questionId);
        Task<int> DeleteAsync(IAnswerChoice entity);
        Task<int> DeleteAsync(Guid id);

        Task<IUnitOfWork> CreateUnitOfWork();
    }
}
