//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Transactions;

//using ExamPreparation.Model.Common;
//using ExamPreparation.Repository.Common;
//using ExamPreparation.Service.Common;

//namespace ExamPreparation.Service
//{
//    public class ProblemService: IProblemService
//    {
//        protected IProblemRepository Repository { get; set; }
//        protected IUnitOfWork UnitOfWork;

//        public ProblemService(IProblemRepository repository)
//        {
//            Repository = repository;
//        }

//        public Task<List<IProblem>> GetPageAsync(int pageSize, int pageNumber)
//        {
//            return Repository.GetPageAsync(pageSize, pageNumber);
//        }

//        public Task<List<IProblem>> GetAllAsync()
//        {
//            return Repository.GetAllAsync();
//        }

//        public Task<IProblem> GetByIdAsync(Guid id)
//        {
//            return Repository.GetByIdAsync(id);
//        }

//        public Task<int> AddAsync(IProblem entity)
//        {
//            return Repository.AddAsync(entity);
//        }

//        public Task<int> UpdateAsync(IProblem entity)
//        {
//            try
//            {
//                return Repository.UpdateAsync(entity);
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.ToString());
//            }
//        }

//        public Task<int> DeleteAsync(IProblem entity)
//        {
//            return Repository.DeleteAsync(entity);
//        }

//        public Task<int> DeleteAsync(Guid id)
//        {
//            return Repository.DeleteAsync(id);
//        }

//        public Task<int> AddAsync(IProblem entity, List<IAnswerChoice> choices)
//        {
//            using(TransactionScope scope = new TransactionScope())
//            {
//                Repository.CreateUnitOfWork();
//                UnitOfWork = Repository.UnitOfWork;

//                Repository.AddAsync(UnitOfWork, entity); 
//                foreach (var choice in choices) 
//                {
//                    Repository.AddAsync(UnitOfWork, choice);
//                }
//                var result = UnitOfWork.CommitAsync();
                
//                if(result.Result == 1)
//                {
//                    scope.Complete();
//                }
                
//                scope.Dispose();
//                return result;
//            }
//        }

//        public Task<List<IProblem>> GetByTypeId(Guid typeId)
//        {
//            return Task.Factory.StartNew(() => GetAllAsync().Result.Where(problem => problem.ProblemTypeId==typeId).ToList());
//        }

//        public Task<List<IProblem>> GetByTestingAreaId(Guid testingAreaId)
//        {
//            return Task.Factory.StartNew(() => GetAllAsync().Result.Where(problem => problem.TestingAreaId == testingAreaId).ToList());
//        }
//    }
//}
