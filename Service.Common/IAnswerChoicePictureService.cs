using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface IAnswerChoicePictureService
    {
        Task<List<IAnswerChoicePicture>> GetPageAsync(int pageSize, int pageNumber);

        Task<List<IAnswerChoicePicture>> GetAllAsync();
        Task<IAnswerChoicePicture> GetByIdAsync(Guid id);
        Task<int> AddAsync(IAnswerChoicePicture entity);
        Task<int> UpdateAsync(IAnswerChoicePicture entity);
        Task<int> DeleteAsync(IAnswerChoicePicture entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddUoWAsync(IAnswerChoicePicture entity);
    }
}
