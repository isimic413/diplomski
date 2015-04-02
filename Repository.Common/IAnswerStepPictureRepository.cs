using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IAnswerStepPictureRepository
    {
        IUnitOfWork UnitOfWork { get; set; }

        void CreateUnitOfWork();

        Task<List<IAnswerStepPicture>> GetPageAsync(int pageSize = 0, int pageNumber = 0);

        Task<List<IAnswerStepPicture>> GetAllAsync();
        Task<IAnswerStepPicture> GetByIdAsync(Guid id);
        Task<int> AddAsync(IAnswerStepPicture entity);
        Task<int> UpdateAsync(IAnswerStepPicture entity);
        Task<int> DeleteAsync(IAnswerStepPicture entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddAsync(IUnitOfWork unitOfWork, IAnswerStepPicture entity);
    }
}
