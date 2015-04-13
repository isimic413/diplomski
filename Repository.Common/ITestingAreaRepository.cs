using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface ITestingAreaRepository
    {
        Task<List<ITestingArea>> GetAsync(string sortOrder = "areaId", int pageNumber = 0, int pageSize = 50);
        Task<ITestingArea> GetAsync(Guid id);
        Task<int> AddAsync(ITestingArea entity);
        Task<int> UpdateAsync(ITestingArea entity);
        Task<int> DeleteAsync(ITestingArea entity);
        Task<int> DeleteAsync(Guid id);
    }
}
