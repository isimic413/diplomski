using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public virtual async Task<List<IQuestionPicture>> GetAsync()
        {
            try
            {
                return Mapper.Map<List<IQuestionPicture>>(
                    await Repository.WhereAsync<QuestionPicture>()
                    .ToListAsync()
                    );
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<IQuestionPicture> GetAsync(Guid id)
        {
            try
            {
                return Mapper.Map<IQuestionPicture>(
                    await Repository.SingleAsync<QuestionPicture>(id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // DateCreated, DateUpdated?
        public virtual Task<int> InsertAsync(IQuestionPicture entity)
        {
            try
            {
                return Repository.InsertAsync<QuestionPicture>(
                    Mapper.Map<QuestionPicture>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<int> AddAsync(IUnitOfWork unitOfWork, IQuestionPicture entity)
        {
            try
            {
                return unitOfWork.AddAsync<QuestionPicture>(
                    Mapper.Map<QuestionPicture>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // DateUpdated?
        public virtual Task<int> UpdateAsync(IQuestionPicture entity)
        {
            try
            {
                return Repository.UpdateAsync<QuestionPicture>(
                    Mapper.Map<QuestionPicture>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual Task<int> DeleteAsync(IQuestionPicture entity)
        {
            try
            {
                return Repository.DeleteAsync<QuestionPicture>(
                    Mapper.Map<QuestionPicture>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual Task<int> DeleteAsync(Guid id)
        {
            try
            {
                return Repository.DeleteAsync<QuestionPicture>(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Methods
    }
}
