using System;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IAnswerStepPictureRepository
    {
        Task<IAnswerStepPicture> GetAsync(Guid id);
        Task<int> UpdateAsync(IAnswerStepPicture entity);
    }
}
