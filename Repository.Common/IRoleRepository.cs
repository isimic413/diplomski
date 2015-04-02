using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IRoleRepository
    {
        IUnitOfWork UnitOfWork { get; set; }

        void CreateUnitOfWork();

        Task<List<IRole>> GetPageAsync(int pageSize = 0, int pageNumber = 0);

        Task<List<IRole>> GetAllAsync();
        Task<IRole> GetByIdAsync(Guid id);
        Task<int> AddAsync(IRole entity);
        Task<int> UpdateAsync(IRole entity);
        Task<int> DeleteAsync(IRole entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddAsync(IUnitOfWork unitOfWork, IRole entity);
    }
}
