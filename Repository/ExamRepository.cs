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
    public class ExamRepository: IExamRepository
    {
        #region Properties

        protected IRepository Repository { get; private set; }

        #endregion Properties

        #region Constructors

        public ExamRepository(IRepository repository)
        {
            Repository = repository;
        }

        #endregion Constructors

        #region Methods

        #region Get

        public virtual async Task<List<IExam>> GetAsync(ExamFilter filter)
        {
            try
            {
                return Mapper.Map<List<IExam>>(
                    await Repository.WhereAsync<Exam>()
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
                return Mapper.Map<IExam>(await Repository.SingleAsync<Exam>(id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<List<IExam>> GetByYearAsync(int year, ExamFilter filter)
        {
            try
            {
                return Mapper.Map<List<IExam>>(
                    await Repository.WhereAsync<Exam>()
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
                    await Repository.WhereAsync<Exam>()
                        .Where(item => item.ExamQuestions
                            .First<ExamQuestion>()
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

        #endregion Get

        #region Insert

        public virtual Task<int> InsertAsync(IExam entity)
        {
            try
            {
                return Repository.InsertAsync<Exam>(Mapper.Map<Exam>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Insert

        #region Update

        public virtual Task<int> UpdateAsync(IExam entity)
        {
            try
            {
                return Repository.UpdateAsync<Exam>(Mapper.Map<Exam>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Update

        #region Delete

        public async Task<int> DeleteAsync(IExam entity)
        {
            try
            {
                IUnitOfWork unitOfWork = Repository.CreateUnitOfWork();

                var examQuestions = await Repository.WhereAsync<ExamQuestion>()
                    .Where<ExamQuestion>(item => item.ExamId == entity.Id)
                    .ToListAsync<ExamQuestion>();

                if (examQuestions.Count > 0)
                {
                    foreach (var item in examQuestions)
                    {
                        await unitOfWork.DeleteAsync<ExamQuestion>(item);
                    }
                }

                await unitOfWork.DeleteAsync<Exam>(entity.Id);

                return await unitOfWork.CommitAsync();
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

                return await DeleteAsync(Mapper.Map<IExam>(
                    await Repository.SingleAsync<Exam>(id))
                    );
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
