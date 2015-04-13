using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface IAnswerStepService
    {
        Task<List<IAnswerStep>> GetAsync(string sortOrder = "problemId", int pageNumber = 0, int pageSize = 50);
        Task<IAnswerStep> GetAsync(Guid id);
        Task<int> AddAsync(IAnswerStep entity, IAnswerStepPicture picture = null);
        Task<int> UpdateAsync(IAnswerStep entity, IAnswerStepPicture picture = null);
        Task<int> DeleteAsync(IAnswerStep entity);
        Task<int> DeleteAsync(Guid id);

        Task<List<IAnswerStep>> GetStepsAsync(Guid problemId);
    }
}
