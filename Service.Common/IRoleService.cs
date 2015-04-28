using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface IRoleService
    {
        Task<List<IRole>> GetAsync(RoleFilter filter);
        Task<IRole> GetAsync(Guid id);
        Task<int> AddAsync(IRole entity);
        Task<int> UpdateAsync(IRole entity);
        Task<int> DeleteAsync(IRole entity);
        Task<int> DeleteAsync(Guid id);
    }
}
