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

        public virtual async Task<List<IExamQuestion>> GetAsync(ExamQuestionFilter filter)
        {
            try
            {
                return Mapper.Map<List<IExamQuestion>>(
                    await Repository.WhereAsync<ExamQuestion>()
                        .OrderBy(filter.SortOrder)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync()
                        );
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
        public async Task<List<IQuestion>> GetExamQuestionsAsync(Guid examId)
        {
            try
            {
                // get all ExamQuestions with ExamId == examId
                var questions = await Repository.WhereAsync<ExamQuestion>()
                    .Where(item => item.ExamId == examId)
                    .OrderBy(item => item.QuestionNumber)
                    .ToListAsync();

                // get the list of Questions that were on that exam
                List<IQuestion> result = new List<IQuestion>();
                foreach (var question in questions)
                {
                    result.Add(Mapper.Map<IQuestion>(
                        await Repository.SingleAsync<Question>(question.QuestionId)
                        ));
                }

                return result;
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
                    .SingleAsync();

                // return that Question
                return Mapper.Map<IQuestion>(await Repository.SingleAsync<Question>(question.QuestionId));

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
                if (e.ToString().Contains("DbUpdateException"))
                {
                    throw new Exception("Question (id=" + entity.QuestionId 
                        + ") is already on Exam (id=" + entity.ExamId + ") or\n\r"
                        +"Exam (id=" + entity.ExamId +") already has Question number "
                        + entity.QuestionNumber + ".");
                }
                else
                {
                    throw e;
                }
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
