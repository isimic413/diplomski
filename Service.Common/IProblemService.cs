using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface IProblemService
    {
        Task<List<IProblem>> GetPageAsync(int pageSize, int pageNumber);

        Task<List<IProblem>> GetAllAsync();
        Task<IProblem> GetByIdAsync(Guid id);
        Task<int> AddAsync(IProblem entity);
        Task<int> UpdateAsync(IProblem entity);
        Task<int> DeleteAsync(IProblem entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddAsync(IProblem entity, List<IAnswerChoice> choices);

        Task<List<IProblem>> GetByTypeId(Guid typeId);
        Task<List<IProblem>> GetByTestingAreaId(Guid testingAreaId);
    }
}
