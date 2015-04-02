using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IExamProblemRepository
    {
        IUnitOfWork UnitOfWork { get; set; }

        void CreateUnitOfWork();

        Task<List<IExamProblem>> GetPageAsync(int pageSize = 0, int pageNumber = 0);

        Task<List<IExamProblem>> GetAllAsync();
        Task<IExamProblem> GetByIdAsync(Guid id);
        Task<int> AddAsync(IExamProblem entity);
        Task<int> UpdateAsync(IExamProblem entity);
        Task<int> DeleteAsync(IExamProblem entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddAsync(IUnitOfWork unitOfWork, IExamProblem entity);
    }
}
