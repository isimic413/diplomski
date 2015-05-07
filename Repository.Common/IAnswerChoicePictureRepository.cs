using System;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IAnswerChoicePictureRepository
    {
        Task<IAnswerChoicePicture> GetAsync(Guid id);
        Task<int> UpdateAsync(IAnswerChoicePicture entity);
    }
}
