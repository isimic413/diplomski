using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using DALModel = ExamPreparation.DAL.Models;
using ExamModel = ExamPreparation.Model;

namespace ExamPreparation.Repository
{
    public class ExamQuestionRepository: IExamQuestionRepository
    {
        protected IRepository Repository { get; private set; }

        public ExamQuestionRepository(IRepository repository)
        {
            Repository = repository;
        }

        public virtual async Task<List<IExamQuestion>> GetAsync(ExamQuestionFilter filter)
        {
            try
            {
                return Mapper.Map<List<IExamQuestion>>(
                    await Repository.WhereAsync<DALModel.ExamQuestion>()
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
                return Mapper.Map<ExamModel.ExamQuestion>(await Repository.SingleAsync<DALModel.ExamQuestion>(id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<int> AddAsync(IExamQuestion entity)
        {
            try
            {
                return await Repository.AddAsync<DALModel.ExamQuestion>(Mapper.Map<DALModel.ExamQuestion>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> UpdateAsync(IExamQuestion entity)
        {
            try
            {
                return await Repository.UpdateAsync<DALModel.ExamQuestion>(Mapper.Map<DALModel.ExamQuestion>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> DeleteAsync(IExamQuestion entity)
        {
            try
            {
                return await Repository.DeleteAsync<DALModel.ExamQuestion>(Mapper.Map<DALModel.ExamQuestion>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> DeleteAsync(Guid id)
        {
            try
            {
                return await Repository.DeleteAsync<DALModel.ExamQuestion>(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<List<IQuestion>> GetExamQuestionsAsync(Guid examId)
        {
            try
            {
                // get all ExamQuestions with ExamId == examId
                var questions = await Repository.WhereAsync<DALModel.ExamQuestion>()
                    .Where(item => item.ExamId == examId)
                    .OrderBy(item => item.QuestionNumber)
                    .ToListAsync();

                // get the list of Questions that were on that exam
                List<IQuestion> result = new List<IQuestion>();
                foreach (var question in questions)
                {
                    result.Add(Mapper.Map<IQuestion>(
                        await Repository.SingleAsync<DALModel.Question>(question.QuestionId)
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
                var question = await Repository.WhereAsync<DALModel.ExamQuestion>()
                    .Where(item => item.ExamId == examId && item.QuestionNumber == questionNumber)
                    .SingleAsync();

                // return that Question
                return Mapper.Map<IQuestion>(await Repository.SingleAsync<DALModel.Question>(question.QuestionId));

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
