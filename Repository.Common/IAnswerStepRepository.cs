using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IAnswerStepRepository
    {
        Task<List<IAnswerStep>> GetAsync(AnswerStepFilter filter = null);
        Task<IAnswerStep> GetAsync(Guid id);
        Task<List<IAnswerStep>> GetStepsAsync(Guid questionId);

        Task<int> AddAsync(IUnitOfWork unitOfWork, IAnswerStep entity);
        Task<int> AddAsync(IUnitOfWork unitOfWork, List<IAnswerStep> entities,
            List<IAnswerStepPicture> pictures = null);
        Task<int> InsertAsync(IAnswerStep entity);

        Task<int> UpdateAsync(IUnitOfWork unitOfWork, IAnswerStep entity);
        Task<int> UpdateAsync(IAnswerStep entity);

        Task<int> DeleteAsync(IUnitOfWork unitOfWork, Guid questionId);
        Task<int> DeleteAsync(IUnitOfWork unitOfWork, IAnswerStep entity);
        Task<int> DeleteAsync(IAnswerStep entity);
        Task<int> DeleteAsync(Guid id);

        Task<IUnitOfWork> CreateUnitOfWork();
    }
}
