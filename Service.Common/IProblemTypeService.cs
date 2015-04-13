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
        Task<List<IProblemType>> GetAsync(string sortOrder = "typeId", int pageNumber = 0, int pageSize = 50);
        Task<IProblemType> GetAsync(Guid id);
        Task<int> AddAsync(IProblemType entity);
        Task<int> UpdateAsync(IProblemType entity);
        Task<int> DeleteAsync(IProblemType entity);
        Task<int> DeleteAsync(Guid id);
    }
}
