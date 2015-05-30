using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface IAnswerStepService
    {
        Task<List<IAnswerStep>> GetAsync(AnswerStepFilter filter = null);
        Task<IAnswerStep> GetAsync(Guid id);
        Task<List<IAnswerStep>> GetStepsAsync(Guid questionId);

        Task<int> InsertAsync(IAnswerStep entity, IAnswerStepPicture picture = null);

        Task<int> UpdateAsync(IAnswerStep entity, IAnswerStepPicture picture = null);

        Task<int> DeleteAsync(IAnswerStep entity);
        Task<int> DeleteAsync(Guid id);
    }
}
