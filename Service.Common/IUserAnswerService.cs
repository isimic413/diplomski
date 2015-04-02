using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface IUserAnswerService
    {
        Task<List<IUserAnswer>> GetPageAsync(int pageSize, int pageNumber);

        Task<List<IUserAnswer>> GetAllAsync();
        Task<IUserAnswer> GetByIdAsync(Guid id);
        Task<int> AddAsync(IUserAnswer entity);
        Task<int> UpdateAsync(IUserAnswer entity);
        Task<int> DeleteAsync(IUserAnswer entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddUoWAsync(IUserAnswer entity);
    }
}
