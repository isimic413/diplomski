using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IAnswerChoiceRepository
    {
        IUnitOfWork UnitOfWork { get; set; }

        void CreateUnitOfWork();

        Task<List<IAnswerChoice>> GetPageAsync(int pageSize=0, int pageNumber=0);

        Task<List<IAnswerChoice>> GetAllAsync();
        Task<IAnswerChoice> GetByIdAsync(Guid id);
        Task<int> AddAsync(IAnswerChoice entity);
        Task<int> UpdateAsync(IAnswerChoice entity);
        Task<int> DeleteAsync(IAnswerChoice entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddAsync(IUnitOfWork unitOfWork, IAnswerChoice entity);
    }
}
