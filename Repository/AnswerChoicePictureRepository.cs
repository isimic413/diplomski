using AutoMapper;
using System;
using System.Threading.Tasks;

using ExamPreparation.Model.Common;
using ExamPreparation.DAL.Models;
using ExamPreparation.Repository.Common;

namespace ExamPreparation.Repository
{
    public class AnswerChoicePictureRepository: IAnswerChoicePictureRepository
    {
        #region Properties

        protected IRepository Repository { get; private set; }

        #endregion Properties

        #region Constructors

        public AnswerChoicePictureRepository(IRepository repository)
        {
            Repository = repository;
        }

        #endregion Constructors

        #region Methods

        public virtual async Task<IAnswerChoicePicture> GetAsync(Guid id)
        {
            try
            {
                return Mapper.Map<IAnswerChoicePicture>(await Repository.SingleAsync<AnswerChoicePicture>(id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual Task<int> UpdateAsync(IAnswerChoicePicture entity)
        {
            try
            {
                return Repository.UpdateAsync<AnswerChoicePicture>(Mapper.Map<AnswerChoicePicture>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Methods
    }
}
