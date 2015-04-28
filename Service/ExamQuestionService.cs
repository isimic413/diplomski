using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using ExamPreparation.Service.Common;

namespace ExamPreparation.Service
{
    public class ExamQuestionService: IExamQuestionService
    {
        protected IExamQuestionRepository Repository { get; private set; }

        public ExamQuestionService(IExamQuestionRepository repository)
        {
            Repository = repository;
        }


        public Task<List<IExamQuestion>> GetAsync(ExamQuestionFilter filter)
        {
            try
            {
                return Repository.GetAsync(filter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<IExamQuestion> GetAsync(Guid id)
        {
            try
            {
                return Repository.GetAsync(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> AddAsync(IExamQuestion entity)
        {
            try
            {
                return await Repository.AddAsync(entity);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<int> UpdateAsync(IExamQuestion entity)
        {
            try
            {
                return await Repository.UpdateAsync(entity);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<int> DeleteAsync(IExamQuestion entity)
        {
            try
            {
                return await Repository.DeleteAsync(entity);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            try
            {
                return await Repository.DeleteAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public Task<List<IQuestion>> GetExamQuestionsAsync(Guid examId)
        {
            try
            {
                return Repository.GetExamQuestionsAsync(examId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<IQuestion> GetQuestionAsync(Guid examId, int questionNumber)
        {
            try
            {
                return Repository.GetQuestionAsync(examId, questionNumber);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
