using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using ExamPreparation.Service.Common;

namespace ExamPreparation.Service
{
    public class QuestionService : IQuestionService
    {
        protected IQuestionRepository Repository { get; private set; }

        public QuestionService(IQuestionRepository repository)
        {
            Repository = repository;
        }

        public Task<List<IQuestion>> GetAsync(QuestionFilter filter)
        {
            try
            {
                return Repository.GetAsync(filter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<IQuestion> GetAsync(Guid id)
        {
            try
            {
                return Repository.GetAsync(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> AddAsync(IQuestion entity, IQuestionPicture picture = null,
            List<IAnswerChoice> choices = null, List<IAnswerChoicePicture> choicePictures = null,
            List<IAnswerStep> steps = null, List<IAnswerStepPicture> stepPictures = null)
        {
            try
            {
                return await Repository.AddAsync(entity, picture, choices, choicePictures, steps, stepPictures);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<int> UpdateAsync(IQuestion entity, IQuestionPicture picture = null,
            List<IAnswerChoice> choices = null, List<IAnswerChoicePicture> choicePictures = null,
            List<IAnswerStep> steps = null, List<IAnswerStepPicture> stepPictures = null)
        {
            try
            {
                return await Repository.UpdateAsync(entity, picture, choices, choicePictures, steps, stepPictures);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<int> DeleteAsync(IQuestion entity)
        {
            try
            {
                return await Repository.DeleteAsync(entity);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            try
            {
                return await Repository.DeleteAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public Task<List<IQuestion>> GetByTestingAreaIdAsync(Guid testingAreaId, QuestionFilter filter)
        {
            try
            {
                return Repository.GetByTestingAreaIdAsync(testingAreaId, filter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<List<IQuestion>> GetByTypeIdAsync(Guid typeId, QuestionFilter filter)
        {
            try
            {
                return Repository.GetByTypeIdAsync(typeId, filter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
