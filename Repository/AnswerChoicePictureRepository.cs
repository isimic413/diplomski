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

        // always get all entities from db
        public virtual async Task<List<IAnswerChoicePicture>> GetAsync()
        {
            try
            {
                return Mapper.Map<List<IAnswerChoicePicture>>(
                    await Repository.WhereAsync<AnswerChoicePicture>()
                    .ToListAsync()
                    );
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<IAnswerChoicePicture> GetAsync(Guid id)
        {
            try
            {
                return Mapper.Map<IAnswerChoicePicture>(
                    await Repository.SingleAsync<AnswerChoicePicture>(id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual Task<int> AddAsync(IUnitOfWork unitOfWork, IAnswerChoicePicture entity)
        {
            try
            {
                return unitOfWork.AddAsync<AnswerChoicePicture>(
                    Mapper.Map<AnswerChoicePicture>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // dodati DateCreated i DateUpdated?
        public virtual Task<int> InsertAsync(IAnswerChoicePicture entity)
        {
            try
            {
                return Repository.InsertAsync<AnswerChoicePicture>(
                    Mapper.Map<AnswerChoicePicture>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual Task<int> UpdateAsync(IUnitOfWork unitOfWork, IAnswerChoicePicture entity)
        {
            try
            {
                return unitOfWork.AddAsync<AnswerChoicePicture>(
                    Mapper.Map<AnswerChoicePicture>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // dodati DateUpdated
        public virtual Task<int> UpdateAsync(IAnswerChoicePicture entity)
        {
            try
            {
                return Repository.UpdateAsync<AnswerChoicePicture>(
                    Mapper.Map<AnswerChoicePicture>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual Task<int> DeleteAsync(IAnswerChoicePicture entity)
        {
            try
            {
                return Repository.DeleteAsync<AnswerChoicePicture>
                    (Mapper.Map<AnswerChoicePicture>(entity));
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
                return Repository.DeleteAsync<AnswerChoicePicture>(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Methods
    }
}
