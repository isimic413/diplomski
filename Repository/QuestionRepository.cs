using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.DAL.Models;
using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;

namespace ExamPreparation.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        #region Properties

        protected IRepository Repository { get; private set; }

        #endregion Properties

        #region Constructors

        public QuestionRepository(IRepository repository)
        {
            Repository = repository;
        }

        #endregion Constructors

        #region Methods

        public virtual async Task<List<IQuestion>> GetAsync(QuestionFilter filter = null)
        {
            try
            {
                if (filter != null)
                {
                    return Mapper.Map<List<IQuestion>>(
                        await Repository.WhereAsync<Question>()
                                 .OrderBy(filter.SortOrder)
                                 .Skip<Question>((filter.PageNumber - 1) * filter.PageSize)
                                 .Take<Question>(filter.PageSize)
                                 .Include(item => item.QuestionPictures)
                                 .ToListAsync<Question>()
                                 );
                }
                else // return all
                {
                    return Mapper.Map<List<IQuestion>>(
                        await Repository.WhereAsync<Question>()
                        .Include(item => item.QuestionPictures)
                        .ToListAsync()
                        );
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<IQuestion> GetAsync(Guid id)
        {
            try
            {
                return Mapper.Map<IQuestion>(
                    await Repository.WhereAsync<Question>()
                    .Where(item => item.Id == id)
                    .Include(item => item.QuestionPictures)
                    .SingleAsync()
                    );
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<IQuestion>> GetByTestingAreaIdAsync(Guid testingAreaId, QuestionFilter filter = null)
        {
            try
            {
                if (filter != null)
                {
                    return Mapper.Map<List<IQuestion>>(
                        await Repository.WhereAsync<Question>()
                        .Where<Question>(item => item.TestingAreaId == testingAreaId)
                        .OrderBy(filter.SortOrder)
                        .Skip<Question>((filter.PageNumber - 1) * filter.PageSize)
                        .Take<Question>(filter.PageSize)
                        .Include(item => item.QuestionPictures)
                        .ToListAsync<Question>()
                        );
                }
                else // return all
                {
                    return Mapper.Map<List<IQuestion>>(
                        await Repository.WhereAsync<Question>()
                        .Where<Question>(item => item.TestingAreaId == testingAreaId)
                        .Include(item => item.QuestionPictures)
                        .ToListAsync<Question>()
                        );
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<IQuestion>> GetByTypeIdAsync(Guid typeId, QuestionFilter filter = null)
        {
            try
            {
                if (filter != null)
                {
                    return Mapper.Map<List<IQuestion>>(
                        await Repository.WhereAsync<Question>()
                        .Where<Question>(item => item.QuestionTypeId == typeId)
                        .OrderBy(filter.SortOrder)
                        .Skip<Question>((filter.PageNumber - 1) * filter.PageSize)
                        .Take<Question>(filter.PageSize)
                        .Include(item => item.QuestionPictures)
                        .ToListAsync<Question>()
                        );
                }
                else // return all
                {
                    return Mapper.Map<List<IQuestion>>(
                        await Repository.WhereAsync<Question>()
                        .Where<Question>(item => item.QuestionTypeId == typeId)
                        .Include(item => item.QuestionPictures)
                        .ToListAsync<Question>()
                        );
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual Task<int> InsertAsync(IQuestion entity)
        {
            try
            {
                return Repository.InsertAsync<Question>(
                    Mapper.Map<Question>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual Task<int> AddAsync(IUnitOfWork unitOfWork, IQuestion entity)
        {
            try
            {
                return unitOfWork.AddAsync<Question>(
                    Mapper.Map<Question>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual Task<int> UpdateAsync(IQuestion entity)
        {
            try
            {
                return Repository.UpdateAsync<Question>(
                    Mapper.Map<Question>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task<int> DeleteAsync(IUnitOfWork unitOfWork, Question entity)
        {
            try
            {
                var result = 0;

                var pictures = await Repository.WhereAsync<QuestionPicture>()
                    .Where(item => item.QuestionId == entity.Id)
                    .ToListAsync();

                foreach (var picture in pictures)
                {
                    result += await unitOfWork.DeleteAsync<QuestionPicture>(picture);
                }

                result += await unitOfWork.DeleteAsync<Question>(entity);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<int> DeleteAsync(IUnitOfWork unitOfWork, IQuestion entity)
        {
            try
            {
                return this.DeleteAsync(unitOfWork, 
                    Mapper.Map<Question>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> DeleteAsync(IUnitOfWork unitOfWork, Guid id)
        {
            try
            {
                return await DeleteAsync(unitOfWork, await Repository.SingleAsync<Question>(id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<IUnitOfWork> CreateUnitOfWork()
        {
            return Task.FromResult( Repository.CreateUnitOfWork() );
        }

        #endregion Methods

    }
}
