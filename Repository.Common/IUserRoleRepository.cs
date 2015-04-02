using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IUserRoleRepository
    {
        IUnitOfWork UnitOfWork { get; set; }

        void CreateUnitOfWork();

        Task<List<IUserRole>> GetPageAsync(int pageSize = 0, int pageNumber = 0);

        Task<List<IUserRole>> GetAllAsync();
        Task<IUserRole> GetByIdAsync(Guid id);
        Task<int> AddAsync(IUserRole entity);
        Task<int> UpdateAsync(IUserRole entity);
        Task<int> DeleteAsync(IUserRole entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddAsync(IUnitOfWork unitOfWork, IUserRole entity);
    }
}
