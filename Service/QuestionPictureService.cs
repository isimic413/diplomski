using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using ExamPreparation.Service.Common;

namespace ExamPreparation.Service
{
    public class QuestionPictureService : IQuestionPictureService
    {
        #region Properties

        protected IQuestionPictureRepository Repository { get; private set; }

        #endregion Properties

        #region Constructors

        public QuestionPictureService(IQuestionPictureRepository repository)
        {
            Repository = repository;
        }

        #endregion Constructors

        #region Methods

        public Task<List<IQuestionPicture>> GetAsync()
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

        public Task<IQuestionPicture> GetAsync(Guid id)
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

        public Task<int> InsertAsync(IQuestionPicture entity)
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

        public Task<int> UpdateAsync(IQuestionPicture entity)
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

        public Task<int> DeleteAsync(IQuestionPicture entity)
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
