using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IUserRepository
    {
        IUnitOfWork CreateUnitOfWork();

        Task<List<IUser>> GetAsync(string sortOrder = "email", int pageNumber = 0, int pageSize = 50);
        Task<IUser> GetAsync(Guid id);
        Task<int> AddAsync(IUser entity, List<IRole> roles = null);
        Task<int> UpdateAsync(IUser entity);
        Task<int> DeleteAsync(IUser entity);
        Task<int> DeleteAsync(Guid id);
    }
}
