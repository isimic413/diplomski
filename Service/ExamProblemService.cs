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
    public class ExamProblemService: IExamProblemService
    {
        protected IExamProblemRepository Repository { get; set; }
        protected IUnitOfWork UnitOfWork;

        public ExamProblemService(IExamProblemRepository repository)
        {
            Repository = repository;
        }

        public Task<List<IExamProblem>> GetPageAsync(int pageSize, int pageNumber)
        {
            return Repository.GetPageAsync(pageSize, pageNumber);
        }

        public Task<List<IExamProblem>> GetAllAsync()
        {
            return Repository.GetAllAsync();
        }

        public Task<List<IExamProblem>> GetExamProblemsById(Guid examId)
        {
            return Task.Factory.StartNew(() => Repository.GetAllAsync().Result.Where(problem => problem.ExamId == examId).ToList());
        }

        public Task<IExamProblem> GetExamProblemByExamId(Guid examId, int number)
        {
            return Task.Factory.StartNew(() => GetExamProblemsById(examId).Result.Find(problem => problem.ProblemNumber == number));
        }

        public Task<IExamProblem> GetByIdAsync(Guid id)
        {
            return Repository.GetByIdAsync(id);
        }

        public Task<int> AddAsync(IExamProblem entity)
        {
            return Repository.AddAsync(entity);
        }

        public Task<int> UpdateAsync(IExamProblem entity)
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

        public Task<int> DeleteAsync(IExamProblem entity)
        {
            return Repository.DeleteAsync(entity);
        }

        public Task<int> DeleteAsync(Guid id)
        {
            return Repository.DeleteAsync(id);
        }

        public Task<int> AddUoWAsync(IExamProblem entity)
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
    }
}
