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
    public class ExamQuestionRepository: IExamQuestionRepository
    {
        #region Properties

        protected IRepository Repository { get; private set; }

        #endregion Properties

        #region Constructors

        public ExamQuestionRepository(IRepository repository)
        {
            Repository = repository;
        }

        #endregion Constructors

        #region Methods

        #region Get

        public virtual async Task<List<IExamQuestion>> GetAsync(ExamQuestionFilter filter = null)
        {
            try
            {
                if (filter != null)
                {
                    return Mapper.Map<List<IExamQuestion>>(
                        await Repository.WhereAsync<ExamQuestion>()
                            .OrderBy(filter.SortOrder)
                            .Skip((filter.PageNumber - 1) * filter.PageSize)
                            .Take(filter.PageSize)
                            .ToListAsync()
                            );
                }
                else // return all
                {
                    return Mapper.Map<List<IExamQuestion>>(
                        await Repository.WhereAsync<ExamQuestion>()
                        .ToListAsync()
                        );
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<IExamQuestion> GetAsync(Guid id)
        {
            try
            {
                return Mapper.Map<IExamQuestion>(await Repository.SingleAsync<ExamQuestion>(id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<IExamQuestion>> GetExamQuestionsAsync(Guid examId, ExamQuestionFilter filter = null)
        {
            try
            {
                if (filter != null)
                {
                    return Mapper.Map<List<IExamQuestion>>(
                        await Repository.WhereAsync<ExamQuestion>()
                        .Where(item => item.ExamId == examId)
                        .OrderBy(item => item.QuestionNumber)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .Include(item => item.Question)
                        .ToListAsync()
                        );
                }
                else // return all
                {

                    return Mapper.Map<List<IExamQuestion>>(
                        await Repository.WhereAsync<ExamQuestion>()
                        .Where(item => item.ExamId == examId)
                        .OrderBy(item => item.QuestionNumber)
                        .Include(item => item.Question)
                        .ToListAsync()
                        );
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<IQuestion> GetQuestionAsync(Guid examId, int questionNumber)
        {
            try
            {
                // get entity with QuestionId of the Question we are looking for
                var question = await Repository.WhereAsync<ExamQuestion>()
                    .Where(item => item.ExamId == examId && item.QuestionNumber == questionNumber)
                    .Include(item => item.Question)
                    .SingleAsync();

                // return that Question
                return Mapper.Map<IQuestion>(question.Question);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Get

        #region Insert

        public virtual Task<int> InsertAsync(IExamQuestion entity)
        {
            try
            {
                return Repository.InsertAsync<ExamQuestion>(Mapper.Map<ExamQuestion>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Insert

        #region Update

        public virtual Task<int> UpdateAsync(IExamQuestion entity)
        {
            try
            {
                return Repository.UpdateAsync<ExamQuestion>(Mapper.Map<ExamQuestion>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Update

        #region Delete

        public Task<int> DeleteAsync(IExamQuestion entity)
        {
            try
            {
                return Repository.DeleteAsync<ExamQuestion>(Mapper.Map<ExamQuestion>(entity));
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
                return Repository.DeleteAsync<ExamQuestion>(id);
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
