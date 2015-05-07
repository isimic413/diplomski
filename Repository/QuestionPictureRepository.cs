using AutoMapper;
using System;
using System.Threading.Tasks;

using ExamPreparation.DAL.Models;
using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;

namespace ExamPreparation.Repository
{
    public class QuestionPictureRepository: IQuestionPictureRepository
    {
        #region Properties

        protected IRepository Repository { get; private set; }

        #endregion Properties

        #region Constructors

        public QuestionPictureRepository(IRepository repository)
        {
            Repository = repository;
        }

        #endregion Constructors

        #region Methods

        public virtual async Task<IQuestionPicture> GetAsync(Guid id)
        {
            try
            {
                return Mapper.Map<IQuestionPicture>(await Repository.SingleAsync<QuestionPicture>(id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public virtual async Task<int> UpdateAsync(IQuestionPicture entity)
        {
            try
            {
                return await Repository.UpdateAsync<QuestionPicture>(Mapper.Map<QuestionPicture>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        #endregion Methods
    }
}
