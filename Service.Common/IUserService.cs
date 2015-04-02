using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface IUserService
    {
        Task<List<IUser>> GetPageAsync(int pageSize, int pageNumber);

        Task<List<IUser>> GetAllAsync();
        Task<IUser> GetByIdAsync(Guid id);
        Task<int> AddAsync(IUser entity);
        Task<int> UpdateAsync(IUser entity);
        Task<int> DeleteAsync(IUser entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddUoWAsync(IUser entity);
    }
}
