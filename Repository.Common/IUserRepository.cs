using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IUserRepository
    {
        IUnitOfWork UnitOfWork { get; set; }

        void CreateUnitOfWork();

        Task<List<IUser>> GetPageAsync(int pageSize = 0, int pageNumber = 0);

        Task<List<IUser>> GetAllAsync();
        Task<IUser> GetByIdAsync(Guid id);
        Task<int> AddAsync(IUser entity);
        Task<int> UpdateAsync(IUser entity);
        Task<int> DeleteAsync(IUser entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddAsync(IUnitOfWork unitOfWork, IUser entity);
    }
}
