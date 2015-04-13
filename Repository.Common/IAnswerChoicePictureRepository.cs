using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IAnswerChoicePictureRepository
    {
        Task<List<IAnswerChoicePicture>> GetAsync(string sortOrder = "choiceId", int pageNumber = 0, int pageSize = 50);
        Task<IAnswerChoicePicture> GetAsync(Guid id);
        Task<int> AddAsync(IAnswerChoicePicture entity);
        Task<int> UpdateAsync(IAnswerChoicePicture entity);
        Task<int> DeleteAsync(IAnswerChoicePicture entity);
        Task<int> DeleteAsync(Guid id);
    }
}
