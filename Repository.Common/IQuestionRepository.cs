using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IQuestionRepository
    {
        Task<List<IQuestion>> GetAsync(QuestionFilter filter = null);
        Task<IQuestion> GetAsync(Guid id); 
        Task<List<IQuestion>> GetByTestingAreaIdAsync(Guid testingAreaId, QuestionFilter filter = null);
        Task<List<IQuestion>> GetByTypeIdAsync(Guid typeId, QuestionFilter filter = null);

        Task<int> InsertAsync(IQuestion entity);
        Task<int> AddAsync(IUnitOfWork unitOfWork, IQuestion entity);

        Task<int> UpdateAsync(IQuestion entity);

        Task<int> DeleteAsync(IUnitOfWork unitOfWork, IQuestion entity);
        Task<int> DeleteAsync(IUnitOfWork unitOfWork, Guid id);

        Task<IUnitOfWork> CreateUnitOfWork();
    }
}
