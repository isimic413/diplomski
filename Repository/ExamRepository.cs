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

        public virtual async Task<List<IExam>> GetAsync(ExamFilter filter = null)
        {
            try
            {
                if (filter != null)
                {
                    return Mapper.Map<List<IExam>>(
                        await Repository.WhereAsync<Exam>()
                            .OrderBy(filter.SortOrder)
                            .Skip((filter.PageNumber - 1) * filter.PageSize)
                            .Take(filter.PageSize)
                            .ToListAsync()
                            );
                }
                else // return all
                {
                    return Mapper.Map<List<IExam>>(
                        await Repository.WhereAsync<Exam>()
                        .ToListAsync()
                        );
                }
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

        public async Task<List<IExam>> GetByYearAsync(int year, ExamFilter filter = null)
        {
            try
            {
                if (filter != null)
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
                else // return all
                {
                    return Mapper.Map<List<IExam>>(
                        await Repository.WhereAsync<Exam>()
                            .Where(item => item.Year == year)
                            .OrderBy(item => item.Year)
                            .ToListAsync()
                            ); 
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<List<IExam>> GetByTestingAreaIdAsync(Guid testingAreaId, ExamFilter filter = null)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

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

        private async Task<int> DeleteAsync(Exam entity)
        {
            try
            {
                var unitOfWork = Repository.CreateUnitOfWork();

                var examQuestions = await Repository.WhereAsync<ExamQuestion>()
                    .Where(item => item.ExamId == entity.Id)
                    .ToListAsync();

                if (examQuestions.Count > 0)
                {
                    foreach (var examQuestion in examQuestions)
                    {
                        await unitOfWork.DeleteAsync<ExamQuestion>(examQuestion);
                    }
                }
                await unitOfWork.DeleteAsync<Exam>(entity);

                return await unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Task<int> DeleteAsync(IExam entity)
        {
            try
            {
                return this.DeleteAsync(Mapper.Map<Exam>(entity));
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
                return await this.DeleteAsync(
                    await Repository.SingleAsync<Exam>(id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Methods
    }
}
