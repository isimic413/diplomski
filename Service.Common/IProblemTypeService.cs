using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface IProblemTypeService
    {
        Task<List<IProblemType>> GetPageAsync(int pageSize, int pageNumber);

        Task<List<IProblemType>> GetAllAsync();
        Task<IProblemType> GetByIdAsync(Guid id);
        Task<int> AddAsync(IProblemType entity);
        Task<int> UpdateAsync(IProblemType entity);
        Task<int> DeleteAsync(IProblemType entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddUoWAsync(IProblemType entity);
    }
}
