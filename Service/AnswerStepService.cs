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
    public class AnswerStepService: IAnswerStepService
    {
        protected IAnswerStepRepository Repository { get; set; }
        protected IUnitOfWork UnitOfWork;

        public AnswerStepService(IAnswerStepRepository repository)
        {
            Repository = repository;
        }

        public Task<List<IAnswerStep>> GetPageAsync(int pageSize, int pageNumber)
        {
            return Repository.GetPageAsync(pageSize, pageNumber);
        }

        public Task<List<IAnswerStep>> GetAllAsync()
        {
            return Repository.GetAllAsync();
        }

        public Task<IAnswerStep> GetByIdAsync(Guid id)
        {
            return Repository.GetByIdAsync(id);
        }

        public Task<int> AddAsync(IAnswerStep entity)
        {
            return Repository.AddAsync(entity);
        }

        public Task<int> UpdateAsync(IAnswerStep entity)
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

        public Task<int> DeleteAsync(IAnswerStep entity)
        {
            return Repository.DeleteAsync(entity);
        }

        public Task<int> DeleteAsync(Guid id)
        {
            return Repository.DeleteAsync(id);
        }

        public Task<int> AddUoWAsync(IAnswerStep entity)
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

        public Task<List<IAnswerStep>> GetStepsByProblemId(Guid problemId)
        {
            return Task.Factory.StartNew(() => GetAllAsync().Result.Where(step => step.ProblemId == problemId).ToList());
        }
    }
}
