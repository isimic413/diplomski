using AutoMapper;
using System;
using System.Threading.Tasks;

using ExamPreparation.Model.Common;
using ExamPreparation.DAL.Models;
using ExamPreparation.Repository.Common;

namespace ExamPreparation.Repository
{
    public class AnswerStepPictureRepository: IAnswerStepPictureRepository
    {
        #region Properties

        protected IRepository Repository { get; private set; }

        #endregion Properties

        #region Constructors

        public AnswerStepPictureRepository(IRepository repository)
        {
            Repository = repository;
        }

        #endregion Constructors

        #region Methods

        public virtual async Task<IAnswerStepPicture> GetAsync(Guid id)
        {
            try
            {
                return Mapper.Map<IAnswerStepPicture>(
                    await Repository.SingleAsync<AnswerStepPicture>(id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public virtual Task<int> UpdateAsync(IAnswerStepPicture entity)
        {
            try
            {
                return Repository.UpdateAsync<AnswerStepPicture>(
                    Mapper.Map<AnswerStepPicture>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Methods
    }
}
