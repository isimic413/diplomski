using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using ExamPreparation.Service.Common;

namespace ExamPreparation.Service
{
    public class AnswerStepPictureService: IAnswerStepPictureService
    {
        protected IAnswerStepPictureRepository Repository { get; private set; }

        public AnswerStepPictureService(IAnswerStepPictureRepository repository)
        {
            Repository = repository;
        }


        public Task<List<IAnswerStepPicture>> GetAsync(string sortOrder = "stepId", int pageNumber = 0, int pageSize = 50)
        {
            try
            {
                return Repository.GetAsync(sortOrder, pageNumber, pageSize);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public Task<IAnswerStepPicture> GetAsync(Guid id)
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

        public Task<int> AddAsync(IAnswerStepPicture entity)
        {
            try
            {
                return Repository.AddAsync(entity);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
        public Task<int> UpdateAsync(IAnswerStepPicture entity)
        {
            try
            {
                return Repository.UpdateAsync(entity);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public Task<int> DeleteAsync(IAnswerStepPicture entity)
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
    }
}
