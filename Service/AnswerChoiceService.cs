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
    public class AnswerChoiceService: IAnswerChoiceService
    {
        protected IAnswerChoiceRepository Repository { get; set; }
        protected IUnitOfWork UnitOfWork;

        public AnswerChoiceService(IAnswerChoiceRepository repository)
        {
            Repository = repository;
        }

        public Task<List<IAnswerChoice>> GetPageAsync(int pageSize, int pageNumber)
        {
            return Repository.GetPageAsync(pageSize, pageNumber);
        }

        public Task<List<IAnswerChoice>> GetAllAsync()
        {
            return Repository.GetAllAsync();
        }

        public Task<IAnswerChoice> GetByIdAsync(Guid id)
        {
            return Repository.GetByIdAsync(id);
        }

        public Task<int> AddAsync(IAnswerChoice entity)
        {
            return Repository.AddAsync(entity);
        }

        public Task<int> UpdateAsync(IAnswerChoice entity)
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

        public Task<int> DeleteAsync(IAnswerChoice entity)
        {
            return Repository.DeleteAsync(entity);
        }

        public Task<int> DeleteAsync(Guid id)
        {
            return Repository.DeleteAsync(id);
        }

        public Task<int> AddUoWAsync(IAnswerChoice entity)
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

        public Task<IAnswerChoice> GetCorrectAnswer(Guid problemId)
        {
            return Task.Factory.StartNew(() => GetChoicesByProblemId(problemId).Result.Find(c => c.IsCorrect));
        }
        public Task<List<IAnswerChoice>> GetChoicesByProblemId(Guid problemId)
        {
            return Task.Factory.StartNew(() => GetAllAsync().Result.Where(choice => choice.ProblemId == problemId).ToList());
        }
    }
}
