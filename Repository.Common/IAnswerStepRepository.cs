using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IAnswerStepRepository
    {
        Task<List<IAnswerStep>> GetAsync(AnswerStepFilter filter);
        Task<IAnswerStep> GetAsync(Guid id);
        Task<List<IAnswerStep>> GetStepsAsync(Guid questionId);

        Task AddAsync(IUnitOfWork unitOfWork, IAnswerStep entity, IAnswerStepPicture picture = null);
        Task<int> InsertAsync(IAnswerStep entity, IAnswerStepPicture picture = null);

        Task<int> UpdateAsync(IAnswerStep entity, IAnswerStepPicture picture = null);
        Task UnitOfWorkUpdateAsync(IUnitOfWork unitOfWork,
            IAnswerStep entity, IAnswerStepPicture picture = null);

        Task UnitOfWorkDeleteAsync(IUnitOfWork unitOfWork, IAnswerStep entity);
        Task<int> DeleteAsync(IAnswerStep entity);
        Task<int> DeleteAsync(Guid id);
    }
}
