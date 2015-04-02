using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IExamRepository
    {
        IUnitOfWork UnitOfWork { get; set; }

        void CreateUnitOfWork();

        Task<List<IExam>> GetPageAsync(int pageSize = 0, int pageNumber = 0);

        Task<List<IExam>> GetAllAsync();
        Task<IExam> GetByIdAsync(Guid id);
        Task<int> AddAsync(IExam entity);
        Task<int> UpdateAsync(IExam entity);
        Task<int> DeleteAsync(IExam entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddAsync(IUnitOfWork unitOfWork, IExam entity);
    }
}
