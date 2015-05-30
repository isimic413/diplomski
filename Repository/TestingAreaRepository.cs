using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.DAL.Models;
using ExamPreparation.Repository.Common;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository
{
    public class TestingAreaRepository : ITestingAreaRepository
    {
        #region Properties

        protected IRepository Repository { get; private set; }

        #endregion Properties

        #region Constructors

        public TestingAreaRepository(IRepository repository)
        {
            Repository = repository;
        }

        #endregion Constructors

        #region Methods

        public virtual async Task<List<ITestingArea>> GetAsync(TestingAreaFilter filter = null)
        {
            try
            {
                if (filter != null)
                {
                    return Mapper.Map<List<ITestingArea>>(
                        await Repository.WhereAsync<TestingArea>()
                                .OrderBy(filter.SortOrder)
                                .Skip<TestingArea>((filter.PageNumber - 1) * filter.PageSize)
                                .Take<TestingArea>(filter.PageSize)
                                .ToListAsync<TestingArea>()
                        );
                }
                else // return all
                {
                    return Mapper.Map<List<ITestingArea>>(
                        await Repository.WhereAsync<TestingArea>()
                        .ToListAsync()
                        );
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<ITestingArea> GetAsync(Guid id) 
        {
            try
            {
                return Mapper.Map<ITestingArea>(await Repository.SingleAsync<TestingArea>(id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual Task<int> InsertAsync(ITestingArea entity)
        {
            try
            {
                return Repository.InsertAsync<TestingArea>(Mapper.Map<TestingArea>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual Task<int> UpdateAsync(ITestingArea entity)
        {
            try
            {
                return Repository.UpdateAsync<TestingArea>(Mapper.Map<TestingArea>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> DeleteAsync(ITestingArea entity)
        {
            try
            {
                if (entity.Abrv.ToLower() == "undef")
                {
                    throw new ArgumentException("TestingArea \"Undefined\" cannot be deleted.");
                }
                else
                {
                    IUnitOfWork unitOfWork = Repository.CreateUnitOfWork();

                    var questions = await Repository.WhereAsync<Question>()
                        .Where<Question>(item => item.QuestionTypeId == entity.Id)
                        .ToListAsync();

                    var taUndef = await Repository.WhereAsync<TestingArea>()
                        .Where<TestingArea>(item => item.Abrv.ToLower() == "undef")
                        .SingleAsync();

                    foreach (var question in questions)
                    {
                        question.QuestionTypeId = taUndef.Id;
                        await unitOfWork.UpdateAsync<Question>(question);
                    }

                    await unitOfWork.DeleteAsync<TestingArea>(entity.Id);

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
                return await DeleteAsync(Mapper.Map<ITestingArea>(
                    await Repository.SingleAsync<TestingArea>(id))
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

        #endregion Methods
    }
}
