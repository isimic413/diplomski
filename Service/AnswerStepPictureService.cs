using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using ExamPreparation.Service.Common;

namespace ExamPreparation.Service
{
    public class AnswerStepPictureService : IAnswerStepPictureService
    {
        #region Properties

        protected IAnswerStepPictureRepository Repository { get; private set; }

        #endregion Properties

        #region Constructors

        public AnswerStepPictureService(IAnswerStepPictureRepository repository)
        {
            Repository = repository;
        }

        #endregion Constructors

        #region Methods


        public Task<List<IAnswerStepPicture>> GetAsync()
        {
            try
            {
                return Repository.GetAsync();
            }
            catch (Exception e)
            {
                throw e;
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
                throw e;
            }
        }

        public Task<int> InsertAsync(IAnswerStepPicture entity)
        {
            try
            {
                return Repository.InsertAsync(entity);
            }
            catch (Exception e)
            {
                throw e;
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
                throw e;
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

        #endregion Methods
    }
}
