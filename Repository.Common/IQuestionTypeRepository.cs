using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IQuestionTypeRepository
    {
        Task<List<IQuestionType>> GetAsync(QuestionTypeFilter filter);
        Task<IQuestionType> GetAsync(Guid id);
        Task<int> InsertAsync(IQuestionType entity);
        Task<int> UpdateAsync(IQuestionType entity);
        Task<int> DeleteAsync(IQuestionType entity);
        Task<int> DeleteAsync(Guid id);
    }
}
