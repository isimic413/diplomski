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
    public class AnswerChoiceService: IAnswerChoiceService
    {
        protected IAnswerChoiceRepository Repository { get; private set; }

        public AnswerChoiceService(IAnswerChoiceRepository repository)
        {
            Repository = repository;
        }

        public Task<List<IAnswerChoice>> GetAsync(AnswerChoiceFilter filter)
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

        public Task<IAnswerChoice> GetAsync(Guid id)
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

        public Task<int> AddAsync(IAnswerChoice entity, IAnswerChoicePicture picture = null)
        {
            try
            {
                return Repository.AddAsync(entity, picture);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public Task<int> UpdateAsync(IAnswerChoice entity, IAnswerChoicePicture picture = null)
        {
            try
            {
                return Repository.UpdateAsync(entity, picture);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public  Task<int> DeleteAsync(IAnswerChoice entity)
        {
            try
            {
                return Repository.DeleteAsync(entity);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public Task<int> DeleteAsync(Guid id)
        {
            try
            {
                return Repository.DeleteAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            } 
        }

        public Task<List<IAnswerChoice>> GetCorrectAnswersAsync(Guid questionId)
        {
            try
            {
                return Repository.GetCorrectAnswersAsync(questionId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<List<IAnswerChoice>> GetChoicesAsync(Guid questionId)
        {
            try
            {
                return Repository.GetChoicesAsync(questionId);
            }
            catch (Exception e)
            {
                throw e;
            } 
        }
    }
}
