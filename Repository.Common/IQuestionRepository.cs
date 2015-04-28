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

        Task<int> AddAsync(IQuestion entity, IQuestionPicture picture = null,
            List<IAnswerChoice> choices = null, List<IAnswerChoicePicture> choicePictures = null,
            List<IAnswerStep> steps = null, List<IAnswerStepPicture> stepPictures = null);

        Task<int> UpdateAsync(IQuestion entity, IQuestionPicture picture = null,
            List<IAnswerChoice> choices = null, List<IAnswerChoicePicture> choicePictures = null,
            List<IAnswerStep> steps = null, List<IAnswerStepPicture> stepPictures = null);

        Task<int> DeleteAsync(IQuestion entity);

        Task<int> DeleteAsync(Guid id);


        Task<List<IQuestion>> GetByTestingAreaIdAsync(Guid testingAreaId, QuestionFilter filter);
        Task<List<IQuestion>> GetByTypeIdAsync(Guid typeId, QuestionFilter filter);
    }
}
