using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IProblemRepository
    {
        IUnitOfWork UnitOfWork { get; set; }

        void CreateUnitOfWork();

        Task<List<IProblem>> GetPageAsync(int pageSize = 0, int pageNumber = 0);

        Task<List<IProblem>> GetAllAsync();
        Task<IProblem> GetByIdAsync(Guid id);
        Task<int> AddAsync(IProblem entity);
        Task<int> UpdateAsync(IProblem entity);
        Task<int> DeleteAsync(IProblem entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddAsync(IUnitOfWork unitOfWork, IProblem entity);
        Task<int> AddAsync(IUnitOfWork unitOfWork, IAnswerChoice entity);
    }
}
