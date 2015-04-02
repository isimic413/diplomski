using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IUserAnswerRepository
    {
        IUnitOfWork UnitOfWork { get; set; }

        void CreateUnitOfWork();

        Task<List<IUserAnswer>> GetPageAsync(int pageSize = 0, int pageNumber = 0);

        Task<List<IUserAnswer>> GetAllAsync();
        Task<IUserAnswer> GetByIdAsync(Guid id);
        Task<int> AddAsync(IUserAnswer entity);
        Task<int> UpdateAsync(IUserAnswer entity);
        Task<int> DeleteAsync(IUserAnswer entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddAsync(IUnitOfWork unitOfWork, IUserAnswer entity);
    }
}
