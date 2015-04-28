using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IQuestionPictureRepository
    {
        Task<List<IQuestionPicture>> GetAsync(string sortOrder = "problemId", int pageNumber = 0, int pageSize = 50);
        Task<IQuestionPicture> GetAsync(Guid id);
        Task<int> AddAsync(IQuestionPicture entity);
        Task<int> UpdateAsync(IQuestionPicture entity);
        Task<int> DeleteAsync(IQuestionPicture entity);
        Task<int> DeleteAsync(Guid id);
    }
}
