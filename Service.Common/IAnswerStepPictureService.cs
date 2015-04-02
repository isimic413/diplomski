using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface IAnswerStepPictureService
    {
        Task<List<IAnswerStepPicture>> GetPageAsync(int pageSize, int pageNumber);

        Task<List<IAnswerStepPicture>> GetAllAsync();
        Task<IAnswerStepPicture> GetByIdAsync(Guid id);
        Task<int> AddAsync(IAnswerStepPicture entity);
        Task<int> UpdateAsync(IAnswerStepPicture entity);
        Task<int> DeleteAsync(IAnswerStepPicture entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddUoWAsync(IAnswerStepPicture entity);
    }
}
