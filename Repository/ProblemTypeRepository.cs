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
    public class ProblemTypeRepository: IProblemTypeRepository
    {
        protected IRepository Repository { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }

        public ProblemTypeRepository(IRepository repository)
        {
            Repository = repository;
        }

        public void CreateUnitOfWork()
        {
            UnitOfWork = Repository.CreateUnitOfWork();
        }

        public virtual Task<List<IProblemType>> GetPageAsync(int pageSize = 0, int pageNumber = 0)
        {
            if (pageSize <= 0) return GetAllAsync();

            var dalPage = Repository.WhereAsync<DALModel.ProblemType>()
                .OrderBy(item => item.Abrv)
                .Skip<DALModel.ProblemType>((pageNumber - 1) * pageSize)
                .Take<DALModel.ProblemType>(pageSize)
                .ToListAsync<DALModel.ProblemType>()
                .Result;

            var problemTypes = Mapper.Map<List<DALModel.ProblemType>, List<ExamModel.ProblemType>>(dalPage);
            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.ProblemType>, List<IProblemType>>(problemTypes));
        }

        public virtual Task<List<IProblemType>> GetAllAsync()
        {
            var dalProblemTypes = Repository.WhereAsync<DALModel.ProblemType>().ToListAsync<DALModel.ProblemType>().Result;
            var problemTypes = Mapper.Map<List<DALModel.ProblemType>, List<ExamModel.ProblemType>>(dalProblemTypes);
            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.ProblemType>, List<IProblemType>>(problemTypes));
        }

        public virtual Task<IProblemType> GetByIdAsync(Guid id)
        {
            var dalProblemType = Repository.SingleAsync<DALModel.ProblemType>(id).Result;
            IProblemType problemType = Mapper.Map<DALModel.ProblemType, ExamModel.ProblemType>(dalProblemType);
            return Task.Factory.StartNew(() => problemType);
        }

        public virtual Task<int> AddAsync(IProblemType entity)
        {
            try
            {
                var problemType = Mapper.Map<ExamModel.ProblemType>(entity);
                var dalProblemType = Mapper.Map<ExamModel.ProblemType, DALModel.ProblemType>(problemType);
                return Repository.AddAsync<DALModel.ProblemType>(dalProblemType);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }

        }

        public virtual Task<int> UpdateAsync(IProblemType entity)
        {
            var problemType = Mapper.Map<IProblemType, ExamModel.ProblemType>(entity);
            var dalProblemType = Mapper.Map<ExamModel.ProblemType, DALModel.ProblemType>(problemType);
            try
            {
                return Repository.UpdateAsync<DALModel.ProblemType>(dalProblemType);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual Task<int> DeleteAsync(IProblemType entity)
        {
            var problemType = Mapper.Map<IProblemType, ExamModel.ProblemType>(entity);
            var dalProblemType = Mapper.Map<ExamModel.ProblemType, DALModel.ProblemType>(problemType);
            return Repository.DeleteAsync<DALModel.ProblemType>(dalProblemType);
        }

        public virtual Task<int> DeleteAsync(Guid id)
        {
            return Repository.DeleteAsync<DALModel.ProblemType>(id);
        }

        public virtual Task<int> AddAsync(IUnitOfWork unitOfWork, IProblemType entity)
        {
            var problemType = Mapper.Map<ExamModel.ProblemType>(entity);
            var dalProblemType = Mapper.Map<ExamModel.ProblemType, DALModel.ProblemType>(problemType);
            return unitOfWork.AddAsync<DALModel.ProblemType>(dalProblemType);
        }
    }
}
