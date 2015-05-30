using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IQuestionPictureRepository
    {
        Task<List<IQuestionPicture>> GetAsync();
        Task<IQuestionPicture> GetAsync(Guid id);

        Task<int> InsertAsync(IQuestionPicture entity);
        Task<int> AddAsync(IUnitOfWork unitOfWork, IQuestionPicture entity);

        Task<int> UpdateAsync(IQuestionPicture entity);

        Task<int> DeleteAsync(IQuestionPicture entity);
        Task<int> DeleteAsync(Guid id);
    }
}
