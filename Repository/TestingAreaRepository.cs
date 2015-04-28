using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Repository.Common;
using ExamPreparation.Model.Common;
using DALModel = ExamPreparation.DAL.Models;
using ExamModel = ExamPreparation.Model;

namespace ExamPreparation.Repository
{
    public class TestingAreaRepository : ITestingAreaRepository
    {
        protected IRepository Repository { get; private set; }

        public TestingAreaRepository(IRepository repository)
        {
            Repository = repository;
        }

        public virtual async Task<List<ITestingArea>> GetAsync(TestingAreaFilter filter)
        {
            try
            {
                return Mapper.Map<List<ITestingArea>>(
                    await Repository.WhereAsync<DALModel.TestingArea>()
                            .OrderBy(filter.SortOrder)
                            .Skip<DALModel.TestingArea>((filter.PageNumber - 1) * filter.PageSize)
                            .Take<DALModel.TestingArea>(filter.PageSize)
                            .ToListAsync<DALModel.TestingArea>()
                    );
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
                return Mapper.Map<ExamModel.TestingArea>(await Repository.SingleAsync<DALModel.TestingArea>(id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual Task<int> AddAsync(ITestingArea entity)
        {
            try
            {
                return Repository.AddAsync<DALModel.TestingArea>(Mapper.Map<DALModel.TestingArea>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual Task<int> UpdateAsync(ITestingArea entity)
        {
            try
            {
                return Repository.UpdateAsync<DALModel.TestingArea>(Mapper.Map<DALModel.TestingArea>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual Task<int> DeleteAsync(ITestingArea entity)
        {
            try
            {
                return Repository.DeleteAsync<DALModel.TestingArea>(Mapper.Map<DALModel.TestingArea>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual Task<int> DeleteAsync(Guid id)
        {
            try
            {
                return Repository.DeleteAsync<DALModel.TestingArea>(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
