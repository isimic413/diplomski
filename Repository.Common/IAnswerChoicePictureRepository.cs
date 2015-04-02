using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IAnswerChoicePictureRepository
    {
        IUnitOfWork UnitOfWork { get; set; }

        void CreateUnitOfWork();

        Task<List<IAnswerChoicePicture>> GetPageAsync(int pageSize = 0, int pageNumber = 0);

        Task<List<IAnswerChoicePicture>> GetAllAsync();
        Task<IAnswerChoicePicture> GetByIdAsync(Guid id);
        Task<int> AddAsync(IAnswerChoicePicture entity);
        Task<int> UpdateAsync(IAnswerChoicePicture entity);
        Task<int> DeleteAsync(IAnswerChoicePicture entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddAsync(IUnitOfWork unitOfWork, IAnswerChoicePicture entity);
    }
}
