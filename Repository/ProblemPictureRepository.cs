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
    public class ProblemPictureRepository: IProblemPictureRepository
    {
        protected IRepository Repository { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }

        public ProblemPictureRepository(IRepository repository)
        {
            Repository = repository;
        }

        public void CreateUnitOfWork()
        {
            UnitOfWork = Repository.CreateUnitOfWork();
        }

        public virtual Task<List<IProblemPicture>> GetPageAsync(int pageSize = 0, int pageNumber = 0)
        {
            if (pageSize <= 0) return GetAllAsync();

            var dalPage = Repository.WhereAsync<DALModel.ProblemPicture>()
                .OrderBy(item => item.ProblemId)
                .Skip<DALModel.ProblemPicture>((pageNumber - 1) * pageSize)
                .Take<DALModel.ProblemPicture>(pageSize)
                .ToListAsync<DALModel.ProblemPicture>()
                .Result;

            var problemPictures = Mapper.Map<List<DALModel.ProblemPicture>, List<ExamModel.ProblemPicture>>(dalPage);
            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.ProblemPicture>, List<IProblemPicture>>(problemPictures));
        }

        public virtual Task<List<IProblemPicture>> GetAllAsync()
        {
            var dalProblemPictures = Repository.WhereAsync<DALModel.ProblemPicture>().ToListAsync<DALModel.ProblemPicture>().Result;
            var problemPictures = Mapper.Map<List<DALModel.ProblemPicture>, List<ExamModel.ProblemPicture>>(dalProblemPictures);
            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.ProblemPicture>, List<IProblemPicture>>(problemPictures));
        }

        public virtual Task<IProblemPicture> GetByIdAsync(Guid id)
        {
            var dalProblemPicture = Repository.SingleAsync<DALModel.ProblemPicture>(id).Result;
            IProblemPicture problemPicture = Mapper.Map<DALModel.ProblemPicture, ExamModel.ProblemPicture>(dalProblemPicture);
            return Task.Factory.StartNew(() => problemPicture);
        }

        public virtual Task<int> AddAsync(IProblemPicture entity)
        {
            try
            {
                var problemPicture = Mapper.Map<ExamModel.ProblemPicture>(entity);
                var dalProblemPicture = Mapper.Map<ExamModel.ProblemPicture, DALModel.ProblemPicture>(problemPicture);
                return Repository.AddAsync<DALModel.ProblemPicture>(dalProblemPicture);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }

        }

        public virtual Task<int> UpdateAsync(IProblemPicture entity)
        {
            var problemPicture = Mapper.Map<IProblemPicture, ExamModel.ProblemPicture>(entity);
            var dalProblemPicture = Mapper.Map<ExamModel.ProblemPicture, DALModel.ProblemPicture>(problemPicture);
            try
            {
                return Repository.UpdateAsync<DALModel.ProblemPicture>(dalProblemPicture);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual Task<int> DeleteAsync(IProblemPicture entity)
        {
            var problemPicture = Mapper.Map<IProblemPicture, ExamModel.ProblemPicture>(entity);
            var dalProblemPicture = Mapper.Map<ExamModel.ProblemPicture, DALModel.ProblemPicture>(problemPicture);
            return Repository.DeleteAsync<DALModel.ProblemPicture>(dalProblemPicture);
        }

        public virtual Task<int> DeleteAsync(Guid id)
        {
            return Repository.DeleteAsync<DALModel.ProblemPicture>(id);
        }

        public virtual Task<int> AddAsync(IUnitOfWork unitOfWork, IProblemPicture entity)
        {
            var problemPicture = Mapper.Map<ExamModel.ProblemPicture>(entity);
            var dalProblemPicture = Mapper.Map<ExamModel.ProblemPicture, DALModel.ProblemPicture>(problemPicture);
            return unitOfWork.AddAsync<DALModel.ProblemPicture>(dalProblemPicture);
        }
    }
}
