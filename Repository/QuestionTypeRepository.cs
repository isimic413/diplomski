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
    public class QuestionTypeRepository: IQuestionTypeRepository
    {
        protected IRepository Repository { get; private set; }

        public QuestionTypeRepository(IRepository repository)
        {
            Repository = repository;
        }

        public virtual async Task<List<IQuestionType>> GetAsync(QuestionTypeFilter filter)
        {
            try
            {
                return Mapper.Map<List<IQuestionType>>(
                    await Repository.WhereAsync<DALModel.QuestionType>()
                            .OrderBy(filter.SortOrder)
                            .Skip<DALModel.QuestionType>((filter.PageNumber - 1) * filter.PageSize)
                            .Take<DALModel.QuestionType>(filter.PageSize)
                            .ToListAsync<DALModel.QuestionType>()
                    );
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
                return Mapper.Map<ExamModel.QuestionType>(await Repository.SingleAsync<DALModel.QuestionType>(id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual Task<int> AddAsync(IQuestionType entity)
        {
            try
            {
                return Repository.AddAsync<DALModel.QuestionType>(Mapper.Map<DALModel.QuestionType>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual Task<int> UpdateAsync(IQuestionType entity)
        {
            try
            {
                return Repository.UpdateAsync<DALModel.QuestionType>(Mapper.Map<DALModel.QuestionType>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual Task<int> DeleteAsync(IQuestionType entity)
        {
            try
            {
                return Repository.DeleteAsync<DALModel.QuestionType>(Mapper.Map<DALModel.QuestionType>(entity));
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
                return Repository.DeleteAsync<DALModel.QuestionType>(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
