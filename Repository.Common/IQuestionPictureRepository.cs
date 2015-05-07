using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IQuestionPictureRepository
    {
        Task<IQuestionPicture> GetAsync(Guid id);
        Task<int> UpdateAsync(IQuestionPicture entity);
    }
}
