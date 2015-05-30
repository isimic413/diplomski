using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface IAnswerChoicePictureService
    {
        Task<List<IAnswerChoicePicture>> GetAsync();
        Task<IAnswerChoicePicture> GetAsync(Guid id);

        Task<int> InsertAsync(IAnswerChoicePicture entity);

        Task<int> UpdateAsync(IAnswerChoicePicture entity);

        Task<int> DeleteAsync(IAnswerChoicePicture entity);
        Task<int> DeleteAsync(Guid id);
    }
}
