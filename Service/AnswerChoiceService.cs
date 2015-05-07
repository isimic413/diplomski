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
        #region Properties

        protected IAnswerChoiceRepository Repository { get; private set; }
        protected IAnswerChoicePictureRepository PictureRepository { get; private set; }

        #endregion Properties

        #region Constructors

        public AnswerChoiceService(IAnswerChoiceRepository repository, IAnswerChoicePictureRepository pictureRepository)
        {
            Repository = repository;
            PictureRepository = pictureRepository;
        }

        #endregion Constructors

        #region Methods

        #region Get

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

        #endregion Get

        #region Insert

        public Task<int> InsertAsync(IAnswerChoice entity, IAnswerChoicePicture picture = null)
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

        public Task<int> UpdateAsync(IAnswerChoice entity, IAnswerChoicePicture picture = null)
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

        public Task<int> UpdatePictureAsync(IAnswerChoicePicture entity)
        {
            try
            {
                return PictureRepository.UpdateAsync(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Update

        #region Delete

        public Task<int> DeleteAsync(IAnswerChoice entity)
        {
            try
            {
                return Repository.DeleteAsync(entity);
            }
            catch (InvalidOperationException e)
            {
                throw e;
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
            catch (InvalidOperationException e)
            {
                throw e;
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
