using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IExamQuestionRepository
    {
        Task<List<IExamQuestion>> GetAsync(ExamQuestionFilter filter);
        Task<IExamQuestion> GetAsync(Guid id);
        Task<int> AddAsync(IExamQuestion entity);
        Task<int> UpdateAsync(IExamQuestion entity);
        Task<int> DeleteAsync(IExamQuestion entity);
        Task<int> DeleteAsync(Guid id);

        Task<List<IQuestion>> GetExamQuestionsAsync(Guid examId);
        Task<IQuestion> GetQuestionAsync(Guid examId, int questionNumber);
    }
}
