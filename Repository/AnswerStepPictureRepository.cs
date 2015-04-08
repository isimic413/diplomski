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
    public class AnswerStepPictureRepository: IAnswerStepPictureRepository
    {
        protected IRepository Repository { get; set; }

        public AnswerStepPictureRepository(IRepository repository)
        {
            Repository = repository;
        }

        public virtual async Task<List<IAnswerStepPicture>> GetAsync(string sortOrder = "stepId", int pageNumber = 0, int pageSize = 50)
        {
            pageSize = (pageSize > 50) ? 50 : pageSize;

            var page = await Repository.WhereAsync<DALModel.AnswerStepPicture>()
                .OrderBy(item => item.AnswerStepId)
                .Skip<DALModel.AnswerStepPicture>((pageNumber - 1) * pageSize)
                .Take<DALModel.AnswerStepPicture>(pageSize)
                .ToListAsync<DALModel.AnswerStepPicture>();

            return new List<IAnswerStepPicture>(Mapper.Map<List<ExamModel.AnswerStepPicture>>(page));
        }

        public virtual async Task<IAnswerStepPicture> GetAsync(Guid id)
        {
            var dalAnswerStepPicture = await Repository.SingleAsync<DALModel.AnswerStepPicture>(id);
            return Mapper.Map<ExamModel.AnswerStepPicture>(dalAnswerStepPicture);
        }

        public virtual async Task<int> AddAsync(IAnswerStepPicture entity)
        {
            try
            {
                return await Repository.AddAsync<DALModel.AnswerStepPicture>(Mapper.Map<DALModel.AnswerStepPicture>(entity));
            }
            catch
            {
                return 0;
            }
        }

        public virtual async Task<int> UpdateAsync(IAnswerStepPicture entity)
        {
            try
            {
                return await Repository.UpdateAsync<DALModel.AnswerStepPicture>(Mapper.Map<DALModel.AnswerStepPicture>(entity));
            }
            catch
            {
                return 0;
            }
        }

        public virtual async Task<int> DeleteAsync(IAnswerStepPicture entity)
        {
            return await Repository.DeleteAsync<DALModel.AnswerStepPicture>(Mapper.Map<DALModel.AnswerStepPicture>(entity));
        }

        public virtual async Task<int> DeleteAsync(Guid id)
        {
            return await Repository.DeleteAsync<DALModel.AnswerStepPicture>(id);
        }
    }
}
