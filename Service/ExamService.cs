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
    public class ExamService: IExamService
    {
        protected IExamRepository Repository { get; private set; }

        public ExamService(IExamRepository repository)
        {
            Repository = repository;
        }

        public Task<List<IExam>> GetAsync(ExamFilter filter)
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

        public async Task<IExam> GetAsync(Guid id)
        {
            try
            {
                return await Repository.GetAsync(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> AddAsync(IExam entity)
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

        public async Task<int> UpdateAsync(IExam entity)
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

        public async Task<int> DeleteAsync(IExam entity)
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

        public Task<List<IExam>> GetByYearAsync(int year, ExamFilter filter)
        {
            try
            {
                return Repository.GetByYearAsync(year, filter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<List<IExam>> GetByTestingAreaIdAsync(Guid testingAreaId, ExamFilter filter)
        {
            try
            {
                return Repository.GetByTestingAreaIdAsync(testingAreaId, filter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
