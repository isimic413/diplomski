using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface IRoleService
    {
        Task<List<IRole>> GetAsync(string sortOrder = "roleId", int pageNumber = 0, int pageSize = 50);
        Task<IRole> GetAsync(Guid id);
        Task<int> AddAsync(IRole entity);
        Task<int> UpdateAsync(IRole entity);
        Task<int> DeleteAsync(IRole entity);
        Task<int> DeleteAsync(Guid id);
    }
}
