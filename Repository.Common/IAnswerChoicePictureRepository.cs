using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IAnswerChoicePictureRepository
    {
        Task<List<IAnswerChoicePicture>> GetAsync();
        Task<IAnswerChoicePicture> GetAsync(Guid id);

        Task<int> AddAsync(IUnitOfWork unitOfWork, IAnswerChoicePicture entity);
        Task<int> InsertAsync(IAnswerChoicePicture entity);

        Task<int> UpdateAsync(IAnswerChoicePicture entity);
        Task<int> UpdateAsync(IUnitOfWork unitOfWork, IAnswerChoicePicture entity);

        Task<int> DeleteAsync(IAnswerChoicePicture entity);
        Task<int> DeleteAsync(Guid id);
    }
}
