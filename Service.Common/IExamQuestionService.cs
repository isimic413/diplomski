using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface IExamQuestionService
    {
        Task<List<IExamQuestion>> GetAsync(ExamQuestionFilter filter);
        Task<IExamQuestion> GetAsync(Guid id);
        Task<List<IExamQuestion>> GetExamQuestionsAsync(Guid examId, ExamQuestionFilter filter = null);
        Task<IQuestion> GetQuestionAsync(Guid examId, int questionNumber);

        Task<int> InsertAsync(IExamQuestion entity);

        Task<int> UpdateAsync(IExamQuestion entity);

        Task<int> DeleteAsync(IExamQuestion entity);
        Task<int> DeleteAsync(Guid id);
    }
}
