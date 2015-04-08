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

        public ProblemPictureRepository(IRepository repository)
        {
            Repository = repository;
        }

        public virtual async Task<List<IProblemPicture>> GetAsync(string sortOrder = "problemId", 
            int pageNumber = 0, int pageSize = 50)
        {
            pageSize = (pageSize > 50) ? 50 : pageSize;

            var page = await Repository.WhereAsync<DALModel.ProblemPicture>()
                .OrderBy(item => item.ProblemId)
                .Skip<DALModel.ProblemPicture>((pageNumber - 1) * pageSize)
                .Take<DALModel.ProblemPicture>(pageSize)
                .ToListAsync<DALModel.ProblemPicture>();

            return new List<IProblemPicture>(Mapper.Map<List<ExamModel.ProblemPicture>>(page));
        }

        public virtual async Task<IProblemPicture> GetAsync(Guid id)
        {
            var dalProblemPicture = await Repository.SingleAsync<DALModel.ProblemPicture>(id);
            return Mapper.Map<ExamModel.ProblemPicture>(dalProblemPicture);
        }

        public virtual async Task<int> AddAsync(IProblemPicture entity)
        {
            try
            {
                return await Repository.AddAsync<DALModel.ProblemPicture>(Mapper.Map<DALModel.ProblemPicture>(entity));
            }
            catch
            {
                return 0;
            }
        }

        public virtual async Task<int> UpdateAsync(IProblemPicture entity)
        {
            try
            {
                return await Repository.UpdateAsync<DALModel.ProblemPicture>(Mapper.Map<DALModel.ProblemPicture>(entity));
            }
            catch
            {
                return 0;
            }
        }

        public virtual async Task<int> DeleteAsync(IProblemPicture entity)
        {

            return await Repository.DeleteAsync<DALModel.ProblemPicture>(Mapper.Map<DALModel.ProblemPicture>(entity));
        }

        public virtual async Task<int> DeleteAsync(Guid id)
        {
            return await Repository.DeleteAsync<DALModel.ProblemPicture>(id);
        }
    }
}
