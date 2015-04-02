using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface ITestingAreaRepository
    {
        IUnitOfWork UnitOfWork { get; set; }

        void CreateUnitOfWork();

        Task<List<ITestingArea>> GetPageAsync(int pageSize=0, int pageNumber=0);

        Task<List<ITestingArea>> GetAllAsync();
        Task<ITestingArea> GetByIdAsync(Guid id);
        Task<int> AddAsync(ITestingArea entity);
        Task<int> UpdateAsync(ITestingArea entity);
        Task<int> DeleteAsync(ITestingArea entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddAsync(IUnitOfWork unitOfWork, ITestingArea entity);
    }
}
