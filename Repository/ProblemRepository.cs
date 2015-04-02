using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using DALModel = ExamPreparation.DAL.Models;
using ExamModel = ExamPreparation.Model;

namespace ExamPreparation.Repository
{
    public class ProblemRepository: IProblemRepository
    {
        protected IRepository Repository { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }

        public ProblemRepository(IRepository repository)
        {
            Repository = repository;
        }

        public void CreateUnitOfWork()
        {
            UnitOfWork = Repository.CreateUnitOfWork();
        }

        public virtual Task<List<IProblem>> GetPageAsync(int pageSize = 0, int pageNumber = 0)
        {
            if (pageSize <= 0) return GetAllAsync();

            var dalPage = Repository.WhereAsync<DALModel.Problem>()
                .OrderBy(item => item.TestingAreaId)
                .Skip<DALModel.Problem>((pageNumber - 1) * pageSize)
                .Take<DALModel.Problem>(pageSize)
                .ToListAsync<DALModel.Problem>()
                .Result;

            var problems = Mapper.Map<List<DALModel.Problem>, List<ExamModel.Problem>>(dalPage);
            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.Problem>, List<IProblem>>(problems));
        }

        public virtual Task<List<IProblem>> GetAllAsync()
        {
            var dalProblems = Repository.WhereAsync<DALModel.Problem>().ToListAsync<DALModel.Problem>().Result;
            var problems = Mapper.Map<List<DALModel.Problem>, List<ExamModel.Problem>>(dalProblems);
            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.Problem>, List<IProblem>>(problems));
        }

        public virtual Task<IProblem> GetByIdAsync(Guid id) 
        {
            var dalProblem = Repository.SingleAsync<DALModel.Problem>(id).Result;
            IProblem problem = Mapper.Map<DALModel.Problem, ExamModel.Problem>(dalProblem);
            return Task.Factory.StartNew(() => problem);
        }

        public virtual Task<int> AddAsync(IProblem entity)
        {
            try
            {

                var problem = Mapper.Map<ExamModel.Problem>(entity);
                var dalProblem = Mapper.Map<ExamModel.Problem, DALModel.Problem>(problem);
                return Repository.AddAsync<DALModel.Problem>(dalProblem);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            
        }

        public virtual Task<int> UpdateAsync(IProblem entity)
        {
            var problem = Mapper.Map<IProblem, ExamModel.Problem>(entity);
            var dalProblem = Mapper.Map<ExamModel.Problem, DALModel.Problem>(problem);
            try
            {
                return Repository.UpdateAsync<DALModel.Problem>(dalProblem);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual Task<int> DeleteAsync(IProblem entity)
        {
            var problem = Mapper.Map<IProblem, ExamModel.Problem>(entity);
            var dalProblem = Mapper.Map<ExamModel.Problem, DALModel.Problem>(problem);
            return Repository.DeleteAsync<DALModel.Problem>(dalProblem);
        }

        public virtual Task<int> DeleteAsync(Guid id)
        {
            return Repository.DeleteAsync<DALModel.Problem>(id);
        }

        public virtual Task<int> AddAsync(IUnitOfWork unitOfWork, IProblem entity)
        {
            var problem = Mapper.Map<ExamModel.Problem>(entity);
            var dalProblem = Mapper.Map<ExamModel.Problem, DALModel.Problem>(problem);
            return unitOfWork.AddAsync<DALModel.Problem>(dalProblem);
        }

        public virtual Task<int> AddAsync(IUnitOfWork unitOfWork, IAnswerChoice entity)
        {
            var choice = Mapper.Map<ExamModel.AnswerChoice>(entity);
            var dalChoice = Mapper.Map<ExamModel.AnswerChoice, DALModel.AnswerChoice>(choice);
            return unitOfWork.AddAsync<DALModel.AnswerChoice>(dalChoice);
        }
    }
}
