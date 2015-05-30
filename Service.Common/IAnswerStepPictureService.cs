using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface IAnswerStepPictureService
    {
        Task<List<IAnswerStepPicture>> GetAsync();
        Task<IAnswerStepPicture> GetAsync(Guid id);

        Task<int> InsertAsync(IAnswerStepPicture entity);

        Task<int> UpdateAsync(IAnswerStepPicture entity);

        Task<int> DeleteAsync(IAnswerStepPicture entity);
        Task<int> DeleteAsync(Guid id);
    }
}
