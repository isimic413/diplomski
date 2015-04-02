using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface ITestingAreaService
    {
        Task<List<ITestingArea>> GetPageAsync(int pageSize, int pageNumber);

        Task<List<ITestingArea>> GetAllAsync();
        Task<ITestingArea> GetByIdAsync(Guid id);
        Task<int> AddAsync(ITestingArea entity);
        Task<int> UpdateAsync(ITestingArea entity);
        Task<int> DeleteAsync(ITestingArea entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddUoWAsync(ITestingArea entity);
    }
}