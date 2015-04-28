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
    public class AnswerStepService: IAnswerStepService
    {
        protected IAnswerStepRepository Repository { get; private set; }

        public AnswerStepService(IAnswerStepRepository repository)
        {
            Repository = repository;
        }


        public Task<List<IAnswerStep>> GetAsync(AnswerStepFilter filter)
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

        public Task<IAnswerStep> GetAsync(Guid id)
        {
            try
            {
                return Repository.GetAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public Task<int> AddAsync(IAnswerStep entity, IAnswerStepPicture picture = null)
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

        public Task<int> UpdateAsync(IAnswerStep entity, IAnswerStepPicture picture = null)
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

        public Task<int> DeleteAsync(IAnswerStep entity)
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

        public Task<List<IAnswerStep>> GetStepsAsync(Guid questionId)
        {
            try
            {
                return Repository.GetStepsAsync(questionId);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
