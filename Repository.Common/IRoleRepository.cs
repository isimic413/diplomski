using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IRoleRepository
    {
        Task<List<IRole>> GetAsync(RoleFilter filter);
        Task<IRole> GetAsync(Guid id);

        Task<int> InsertAsync(IRole entity);

        Task<int> UpdateAsync(IRole entity);

        Task<int> DeleteAsync(IRole entity);
        Task<int> DeleteAsync(Guid id);
    }
}
