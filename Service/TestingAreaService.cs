using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using ExamPreparation.Service.Common;

namespace ExamPreparation.Service
{
    public class TestingAreaService : ITestingAreaService
    {
        protected ITestingAreaRepository Repository { get; set; }

        public TestingAreaService(ITestingAreaRepository repository)
        {
            Repository = repository;
        }

        public async Task<List<ITestingArea>> GetAsync(string sortOrder = "areaId", int pageNumber = 0, int pageSize = 50)
        {
            try
            {
                return await Repository.GetAsync(sortOrder, pageNumber, pageSize);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<ITestingArea> GetAsync(Guid id)
        {
            try
            {
                return await Repository.GetAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<int> AddAsync(ITestingArea entity)
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

        public async Task<int> UpdateAsync(ITestingArea entity)
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

        public async Task<int> DeleteAsync(ITestingArea entity)
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
    }
}
