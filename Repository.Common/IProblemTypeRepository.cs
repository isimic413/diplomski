using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IProblemTypeRepository
    {
        IUnitOfWork UnitOfWork { get; set; }

        void CreateUnitOfWork();

        Task<List<IProblemType>> GetPageAsync(int pageSize = 0, int pageNumber = 0);

        Task<List<IProblemType>> GetAllAsync();
        Task<IProblemType> GetByIdAsync(Guid id);
        Task<int> AddAsync(IProblemType entity);
        Task<int> UpdateAsync(IProblemType entity);
        Task<int> DeleteAsync(IProblemType entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddAsync(IUnitOfWork unitOfWork, IProblemType entity);
    }
}
