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
        #region Properties

        protected IExamRepository Repository { get; private set; }

        #endregion Properties

        #region Constructors

        public ExamService(IExamRepository repository)
        {
            Repository = repository;
        }

        #endregion Constructors

        #region Methods

        public Task<List<IExam>> GetAsync(ExamFilter filter = null)
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

        public Task<List<IExam>> GetByYearAsync(int year, ExamFilter filter = null)
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

        public Task<List<IExam>> GetByTestingAreaIdAsync(Guid testingAreaId, ExamFilter filter = null)
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

        public Task<int> InsertAsync(IExam entity)
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

        public Task<int> UpdateAsync(IExam entity)
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

        public Task<int> DeleteAsync(IExam entity)
        {
            try
            {
                return Repository.DeleteAsync(entity);
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
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Methods
    }
}
