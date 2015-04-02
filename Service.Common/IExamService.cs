using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface IExamService
    {
        Task<List<IExam>> GetPageAsync(int pageSize, int pageNumber);

        Task<List<IExam>> GetAllAsync();
        Task<IExam> GetByIdAsync(Guid id);
        Task<int> AddAsync(IExam entity);
        Task<int> UpdateAsync(IExam entity);
        Task<int> DeleteAsync(IExam entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddUoWAsync(IExam entity);

        Task<List<IExam>> GetByYear(int year);
        Task<List<IExam>> GetByTestingAreaId(Guid testingAreaId);
    }
}
