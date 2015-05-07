using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface IQuestionPictureService
    {
        Task<IQuestionPicture> GetAsync(Guid id);
        Task<int> UpdateAsync(IQuestionPicture entity);
    }
}
