using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IAnswerStepPictureRepository
    {
        Task<List<IAnswerStepPicture>> GetAsync();
        Task<IAnswerStepPicture> GetAsync(Guid id);

        Task<int> AddAsync(IUnitOfWork unitOfWork, IAnswerStepPicture entity);
        Task<int> InsertAsync(IAnswerStepPicture entity);

        Task<int> UpdateAsync(IUnitOfWork unitOfWork, IAnswerStepPicture entity);
        Task<int> UpdateAsync(IAnswerStepPicture entity);

        Task<int> DeleteAsync(IAnswerStepPicture entity);
        Task<int> DeleteAsync(Guid id);
    }
}
