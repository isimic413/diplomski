using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface IQuestionService
    {
        Task<List<IQuestion>> GetAsync(QuestionFilter filter);
        Task<IQuestion> GetAsync(Guid id);
        Task<List<IQuestion>> GetByTestingAreaIdAsync(Guid testingAreaId, QuestionFilter filter);
        Task<List<IQuestion>> GetByTypeIdAsync(Guid typeId, QuestionFilter filter);

        Task<int> InsertAsync(IQuestion entity, List<IAnswerChoice> choices,
            IQuestionPicture picture = null, List<IAnswerChoicePicture> choicePictures = null,
            List<IAnswerStep> steps = null, List<IAnswerStepPicture> stepPictures = null);

        Task<int> UpdateAsync(IQuestion entity, IQuestionPicture picture = null,
            List<IAnswerStep> steps = null, List<IAnswerStepPicture> stepPictures = null);
        Task<int> UpdatePictureAsync(IQuestionPicture picture);

        Task<int> DeleteAsync(IQuestion entity);
        Task<int> DeleteAsync(Guid id);
    }
}
