using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IQuestionRepository
    {
        Task<List<IQuestion>> GetAsync(QuestionFilter filter);

        Task<IQuestion> GetAsync(Guid id);

        Task<int> InsertAsync(IQuestion entity, List<IAnswerChoice> choices,
            IQuestionPicture picture = null, List<IAnswerChoicePicture> choicePictures = null);

        Task<int> UpdateAsync(IQuestion entity, IQuestionPicture picture = null);
        Task UnitOfWorkUpdateAsync(IUnitOfWork unitOfWork,
            IQuestion entity, IQuestionPicture picture = null);

        Task<int> DeleteAsync(IQuestion entity);

        Task<int> DeleteAsync(Guid id);


        Task<List<IQuestion>> GetByTestingAreaIdAsync(Guid testingAreaId, QuestionFilter filter);
        Task<List<IQuestion>> GetByTypeIdAsync(Guid typeId, QuestionFilter filter);

        Task<IUnitOfWork> CreateUnitOfWork();
    }
}
