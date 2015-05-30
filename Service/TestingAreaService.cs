using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using ExamPreparation.Service.Common;

namespace ExamPreparation.Service
{
    public class TestingAreaService : ITestingAreaService
    {
        #region Properties

        protected ITestingAreaRepository Repository { get; private set; }

        #endregion Properties

        #region Constructors

        public TestingAreaService(ITestingAreaRepository repository)
        {
            Repository = repository;
        }

        #endregion Constructors

        #region Methods

        public Task<List<ITestingArea>> GetAsync(TestingAreaFilter filter = null)
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

        public Task<ITestingArea> GetAsync(Guid id)
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

        public Task<int> InsertAsync(ITestingArea entity)
        {
            try
            {
                return Repository.InsertAsync(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<int> UpdateAsync(ITestingArea entity)
        {
            try
            {
                return Repository.UpdateAsync(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<int> DeleteAsync(ITestingArea entity)
        {
            try
            {
                return Repository.DeleteAsync(entity);
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

        public Task<int> DeleteAsync(Guid id)
        {
            try
            {
                return Repository.DeleteAsync(id);
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
