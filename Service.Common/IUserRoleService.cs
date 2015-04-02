using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface IUserRoleService
    {
        Task<List<IUserRole>> GetPageAsync(int pageSize, int pageNumber);

        Task<List<IUserRole>> GetAllAsync();
        Task<IUserRole> GetByIdAsync(Guid id);
        Task<int> AddAsync(IUserRole entity);
        Task<int> UpdateAsync(IUserRole entity);
        Task<int> DeleteAsync(IUserRole entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddUoWAsync(IUserRole entity);
    }
}
