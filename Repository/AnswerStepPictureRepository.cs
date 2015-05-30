using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        // GetAll
        public virtual async Task<List<IAnswerStepPicture>> GetAsync()
        {
            try
            {
                return Mapper.Map<List<IAnswerStepPicture>>(
                    await Repository.WhereAsync<AnswerStepPicture>()
                    .ToListAsync()
                    );
            }
            catch (Exception e)
            {
                throw e;
            }
        }

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

        public virtual Task<int> AddAsync(IUnitOfWork unitOfWork, IAnswerStepPicture entity)
        {
            try
            {
                return unitOfWork.AddAsync<AnswerStepPicture>(
                    Mapper.Map<AnswerStepPicture>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // DateCreated, DateUpdated?
        public virtual Task<int> InsertAsync(IAnswerStepPicture entity)
        {
            try
            {
                return Repository.InsertAsync<AnswerStepPicture>(
                    Mapper.Map<AnswerStepPicture>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual Task<int> UpdateAsync(IUnitOfWork unitOfWork, IAnswerStepPicture entity)
        {
            try
            {
                return unitOfWork.UpdateAsync<AnswerStepPicture>(
                    Mapper.Map<AnswerStepPicture>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // DateUpdated?
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

        public virtual Task<int> DeleteAsync(IAnswerStepPicture entity)
        {
            try
            {
                return Repository.DeleteAsync<AnswerStepPicture>(
                    Mapper.Map<AnswerStepPicture>(entity));
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
                return Repository.DeleteAsync<AnswerStepPicture>(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Methods
    }
}
