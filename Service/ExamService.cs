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
    public class ExamService: IExamService
    {
        protected IExamRepository Repository { get; set; }
        protected IUnitOfWork UnitOfWork;

        public ExamService(IExamRepository repository)
        {
            Repository = repository;
        }

        public Task<List<IExam>> GetPageAsync(int pageSize, int pageNumber)
        {
            return Repository.GetPageAsync(pageSize, pageNumber);
        }

        public Task<List<IExam>> GetAllAsync()
        {
            return Repository.GetAllAsync();
        }

        public Task<IExam> GetByIdAsync(Guid id)
        {
            return Repository.GetByIdAsync(id);
        }

        public Task<int> AddAsync(IExam entity)
        {
            return Repository.AddAsync(entity);
        }

        public Task<int> UpdateAsync(IExam entity)
        {
            try
            {
                return Repository.UpdateAsync(entity);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public Task<int> DeleteAsync(IExam entity)
        {
            return Repository.DeleteAsync(entity);
        }

        public Task<int> DeleteAsync(Guid id)
        {
            return Repository.DeleteAsync(id);
        }

        public Task<int> AddUoWAsync(IExam entity)
        {
            using(TransactionScope scope = new TransactionScope())
            {
                Repository.CreateUnitOfWork();
                UnitOfWork = Repository.UnitOfWork;

                Repository.AddAsync(UnitOfWork, entity); 
                var result = UnitOfWork.CommitAsync();
                
                if(result.Result == 1)
                {
                    scope.Complete();
                }
                
                scope.Dispose();
                return result;
            }
        }

        public Task<List<IExam>> GetByYear(int year)
        {
            return Task.Factory.StartNew(() => GetAllAsync().Result.Where(exam => exam.Year == year).ToList());
        }

        public Task<List<IExam>> GetByTestingAreaId(Guid testingAreaId)
        {
            return Task.Factory.StartNew(() => GetAllAsync().Result.Where(exam => exam.TestingAreaId == testingAreaId).ToList());
        }
    }
}
