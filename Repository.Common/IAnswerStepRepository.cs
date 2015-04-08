using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IAnswerStepRepository
    {
        IUnitOfWork CreateUnitOfWork();

        Task<List<IAnswerStep>> GetAsync(string sortOrderFirst = "problemId", string sortOrderSecond = "stepNumber", 
            int pageNumber = 0, int pageSize = 50);
        Task<IAnswerStep> GetAsync(Guid id);
        //Task<int> AddAsync(IAnswerStep entity);
        Task<int> AddAsync(IAnswerStep entity, IAnswerStepPicture picture = null);
        Task<int> UpdateAsync(IAnswerStep entity, IAnswerStepPicture picture = null);
        Task<int> DeleteAsync(IAnswerStep entity);
        Task<int> DeleteAsync(Guid id);
    }
}
