using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using ExamPreparation.Service.Common;

namespace ExamPreparation.Service
{
    public class AnswerChoicePictureService: IAnswerChoicePictureService
    {
        protected IAnswerChoicePictureRepository Repository { get; private set; }

        public AnswerChoicePictureService(IAnswerChoicePictureRepository repository)
        {
            Repository = repository;
        }

        public Task<List<IAnswerChoicePicture>> GetAsync(string sortOrder = "choiceId", int pageNumber = 1, int pageSize = 50)
        {
            try
            {
                return Repository.GetAsync(sortOrder, pageNumber, pageSize);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<IAnswerChoicePicture> GetAsync(Guid id)
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

        public Task<int> AddAsync(IAnswerChoicePicture entity)
        {
            try
            {
                return Repository.AddAsync(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<int> UpdateAsync(IAnswerChoicePicture entity)
        {
            try
            {
                return Repository.UpdateAsync(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<int> DeleteAsync(IAnswerChoicePicture entity)
        {
            try
            {
                return Repository.DeleteAsync(entity);
            }
            catch (Exception e)
            {
                throw e;
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
                throw e;
            }
        }

        public Task<IAnswerChoicePicture> GetByChoiceIdAsync(Guid choiceId)
        {
            try
            {
                return Repository.GetByChoiceIdAsync(choiceId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
