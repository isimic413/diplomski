using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using ExamPreparation.Service.Common;

namespace ExamPreparation.Service
{
    public class AnswerStepService: IAnswerStepService
    {
        #region Properties

        protected IAnswerStepRepository Repository { get; private set; }
        protected IAnswerStepPictureRepository PictureRepository { get; private set; }

        #endregion Properties

        #region Constructors

        public AnswerStepService(IAnswerStepRepository repository, IAnswerStepPictureRepository pictureRepository)
        {
            Repository = repository;
            PictureRepository = pictureRepository;
        }

        #endregion Constructors

        #region Methods

        #region Get

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
                throw e;
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
                throw e;
            }
        }

        #endregion Get

        #region Insert

        public Task<int> InsertAsync(IAnswerStep entity, IAnswerStepPicture picture = null)
        {
            try
            {
                return Repository.InsertAsync(entity, picture);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Insert

        #region Update

        public Task<int> UpdateAsync(IAnswerStep entity, IAnswerStepPicture picture = null)
        {
            try
            {
                return Repository.UpdateAsync(entity, picture);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<int> UpdatePictureAsync(IAnswerStepPicture picture)
        {
            try
            {
                return PictureRepository.UpdateAsync(picture);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Update

        #region Delete

        public Task<int> DeleteAsync(IAnswerStep entity)
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

        #endregion Delete

        #endregion Methods
    }
}
