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
    public class QuestionTypeRepository: IQuestionTypeRepository
    {
        #region Properties

        protected IRepository Repository { get; private set; }

        #endregion Properties

        #region Constructors

        public QuestionTypeRepository(IRepository repository)
        {
            Repository = repository;
        }

        #endregion Constructors

        #region Methods

        #region Get

        public virtual async Task<List<IQuestionType>> GetAsync(QuestionTypeFilter filter = null)
        {
            try
            {
                if (filter != null)
                {
                    return Mapper.Map<List<IQuestionType>>(
                        await Repository.WhereAsync<QuestionType>()
                                .OrderBy(filter.SortOrder)
                                .Skip<QuestionType>((filter.PageNumber - 1) * filter.PageSize)
                                .Take<QuestionType>(filter.PageSize)
                                .ToListAsync<QuestionType>()
                        );
                }
                else // return all
                {
                    return Mapper.Map<List<IQuestionType>>(
                        await Repository.WhereAsync<QuestionType>()
                        .ToListAsync()
                        );
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<IQuestionType> GetAsync(Guid id)
        {
            try
            {
                return Mapper.Map<IQuestionType>(
                    await Repository.SingleAsync<QuestionType>(id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Get

        #region Insert

        public virtual Task<int> InsertAsync(IQuestionType entity)
        {
            try
            {
                return Repository.InsertAsync<QuestionType>(Mapper.Map<QuestionType>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Insert

        #region Update

        public virtual Task<int> UpdateAsync(IQuestionType entity)
        {
            try
            {
                return Repository.UpdateAsync<QuestionType>(Mapper.Map<QuestionType>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Update

        #region Delete

        public async Task<int> DeleteAsync(IQuestionType entity)
        {
            try
            {
                if (entity.Abrv.ToLower() == "undef")
                {
                    throw new ArgumentException("QuestionType \"Undefined\" cannot be deleted.");
                }
                else
                {
                    IUnitOfWork unitOfWork = Repository.CreateUnitOfWork();

                    var questions = await Repository.WhereAsync<Question>()
                        .Where<Question>(item => item.QuestionTypeId == entity.Id)
                        .ToListAsync();

                    var typeUndef = await Repository.WhereAsync<QuestionType>()
                        .Where<QuestionType>(item => item.Abrv.ToLower() == "undef")
                        .SingleAsync();

                    foreach (var question in questions)
                    {
                        question.QuestionTypeId = typeUndef.Id;
                        await unitOfWork.UpdateAsync<Question>(question); 
                    }

                    await unitOfWork.DeleteAsync<QuestionType>(entity.Id);

                    return await unitOfWork.CommitAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            try
            {
                return await DeleteAsync(Mapper.Map<IQuestionType>(
                    await Repository.SingleAsync<QuestionType>(id))
                    );
            }
            catch (ArgumentException e)
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
