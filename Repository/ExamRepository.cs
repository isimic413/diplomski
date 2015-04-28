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
    public class ExamRepository: IExamRepository
    {
        protected IRepository Repository { get; private set; }

        public ExamRepository(IRepository repository)
        {
            Repository = repository;
        }

        public virtual async Task<List<IExam>> GetAsync(ExamFilter filter)
        {
            try
            {
                return Mapper.Map<List<IExam>>(
                    await Repository.WhereAsync<DALModel.Exam>()
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

        public virtual async Task<IExam> GetAsync(Guid id)
        {
            try
            {
                return Mapper.Map<ExamModel.Exam>(await Repository.SingleAsync<DALModel.Exam>(id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<int> AddAsync(IExam entity)
        {
            try
            {
                return await Repository.AddAsync<DALModel.Exam>(Mapper.Map<DALModel.Exam>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> UpdateAsync(IExam entity)
        {
            try
            {
                return await Repository.UpdateAsync<DALModel.Exam>(Mapper.Map<DALModel.Exam>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> DeleteAsync(IExam entity)
        {
            try
            {
                return await Repository.DeleteAsync<DALModel.Exam>(Mapper.Map<DALModel.Exam>(entity));
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
                return await Repository.DeleteAsync<DALModel.Exam>(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<List<IExam>> GetByYearAsync(int year, ExamFilter filter)
        {
            try
            {
                return Mapper.Map<List<IExam>>(
                    await Repository.WhereAsync<DALModel.Exam>()
                        .Where(item => item.Year == year)
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

        public async Task<List<IExam>> GetByTestingAreaIdAsync(Guid testingAreaId, ExamFilter filter)
        {
            try
            {
                return Mapper.Map<List<IExam>>(
                    await Repository.WhereAsync<DALModel.Exam>()
                        .Where(item => item.ExamQuestions
                            .First<DALModel.ExamQuestion>()
                            .Question.TestingAreaId == testingAreaId)
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
    }
}
