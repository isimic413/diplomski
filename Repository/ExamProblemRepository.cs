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
    public class ExamProblemRepository: IExamProblemRepository
    {
        protected IRepository Repository { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }

        public ExamProblemRepository(IRepository repository)
        {
            Repository = repository;
        }

        public void CreateUnitOfWork()
        {
            UnitOfWork = Repository.CreateUnitOfWork();
        }

        public virtual Task<List<IExamProblem>> GetPageAsync(int pageSize = 0, int pageNumber = 0)
        {
            if (pageSize <= 0) return GetAllAsync();

            var dalPage = Repository.WhereAsync<DALModel.ExamProblem>()
                .OrderBy(item => item.ExamId)
                .Skip<DALModel.ExamProblem>((pageNumber - 1) * pageSize)
                .Take<DALModel.ExamProblem>(pageSize)
                .ToListAsync<DALModel.ExamProblem>()
                .Result;

            var examProblems = Mapper.Map<List<DALModel.ExamProblem>, List<ExamModel.ExamProblem>>(dalPage);
            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.ExamProblem>, List<IExamProblem>>(examProblems));
        }

        public virtual Task<List<IExamProblem>> GetAllAsync()
        {
            var dalExamProblems = Repository.WhereAsync<DALModel.ExamProblem>().ToListAsync<DALModel.ExamProblem>().Result;
            var examProblems = Mapper.Map<List<DALModel.ExamProblem>, List<ExamModel.ExamProblem>>(dalExamProblems);
            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.ExamProblem>, List<IExamProblem>>(examProblems));
        }

        public virtual Task<IExamProblem> GetByIdAsync(Guid id)
        {
            var dalExamProblem = Repository.SingleAsync<DALModel.ExamProblem>(id).Result;
            IExamProblem examProblem = Mapper.Map<DALModel.ExamProblem, ExamModel.ExamProblem>(dalExamProblem);
            return Task.Factory.StartNew(() => examProblem);
        }

        public virtual Task<int> AddAsync(IExamProblem entity)
        {
            try
            {
                var examProblem = Mapper.Map<ExamModel.ExamProblem>(entity);
                var dalExamProblem = Mapper.Map<ExamModel.ExamProblem, DALModel.ExamProblem>(examProblem);
                return Repository.AddAsync<DALModel.ExamProblem>(dalExamProblem);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }

        }

        public virtual Task<int> UpdateAsync(IExamProblem entity)
        {
            var examProblem = Mapper.Map<IExamProblem, ExamModel.ExamProblem>(entity);
            var dalExamProblem = Mapper.Map<ExamModel.ExamProblem, DALModel.ExamProblem>(examProblem);
            try
            {
                return Repository.UpdateAsync<DALModel.ExamProblem>(dalExamProblem);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual Task<int> DeleteAsync(IExamProblem entity)
        {
            var examProblem = Mapper.Map<IExamProblem, ExamModel.ExamProblem>(entity);
            var dalExamProblem = Mapper.Map<ExamModel.ExamProblem, DALModel.ExamProblem>(examProblem);
            return Repository.DeleteAsync<DALModel.ExamProblem>(dalExamProblem);
        }

        public virtual Task<int> DeleteAsync(Guid id)
        {
            return Repository.DeleteAsync<DALModel.ExamProblem>(id);
        }

        public virtual Task<int> AddAsync(IUnitOfWork unitOfWork, IExamProblem entity)
        {
            var examProblem = Mapper.Map<ExamModel.ExamProblem>(entity);
            var dalExamProblem = Mapper.Map<ExamModel.ExamProblem, DALModel.ExamProblem>(examProblem);
            return unitOfWork.AddAsync<DALModel.ExamProblem>(dalExamProblem);
        }
    }
}
