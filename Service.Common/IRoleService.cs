using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface IRoleService
    {
        Task<List<IRole>> GetPageAsync(int pageSize, int pageNumber);

        Task<List<IRole>> GetAllAsync();
        Task<IRole> GetByIdAsync(Guid id);
        Task<int> AddAsync(IRole entity);
        Task<int> UpdateAsync(IRole entity);
        Task<int> DeleteAsync(IRole entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddUoWAsync(IRole entity);
    }
}
