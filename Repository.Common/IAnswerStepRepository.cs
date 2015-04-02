using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IAnswerStepRepository
    {
        IUnitOfWork UnitOfWork { get; set; }

        void CreateUnitOfWork();

        Task<List<IAnswerStep>> GetPageAsync(int pageSize = 0, int pageNumber = 0);

        Task<List<IAnswerStep>> GetAllAsync();
        Task<IAnswerStep> GetByIdAsync(Guid id);
        Task<int> AddAsync(IAnswerStep entity);
        Task<int> UpdateAsync(IAnswerStep entity);
        Task<int> DeleteAsync(IAnswerStep entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddAsync(IUnitOfWork unitOfWork, IAnswerStep entity);
    }
}
