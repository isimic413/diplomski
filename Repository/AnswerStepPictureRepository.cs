using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using DALModel = ExamPreparation.DAL.Models;
using ExamModel = ExamPreparation.Model;

namespace ExamPreparation.Repository
{
    public class AnswerStepPictureRepository: IAnswerStepPictureRepository
    {
        protected IRepository Repository { get; private set; }

        public AnswerStepPictureRepository(IRepository repository)
        {
            Repository = repository;
        }

        public virtual async Task<List<IAnswerStepPicture>> GetAsync(string sortOrder = "stepId", int pageNumber = 0, int pageSize = 50)
        {
            try
            {
                List<DALModel.AnswerStepPicture> page;
                pageSize = (pageSize > 50) ? 50 : pageSize;


                switch (sortOrder)
                {
                    case "stepId":
                        page = await Repository.WhereAsync<DALModel.AnswerStepPicture>()
                        .OrderBy(item => item.AnswerStepId)
                        .Skip<DALModel.AnswerStepPicture>((pageNumber - 1) * pageSize)
                        .Take<DALModel.AnswerStepPicture>(pageSize)
                        .ToListAsync<DALModel.AnswerStepPicture>();
                        break;
                    default:
                        throw new ArgumentException("Invalid sortOrder.");
                }

                return new List<IAnswerStepPicture>(Mapper.Map<List<ExamModel.AnswerStepPicture>>(page));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<IAnswerStepPicture> GetAsync(Guid id)
        {
            try
            {
                var dalAnswerStepPicture = await Repository.SingleAsync<DALModel.AnswerStepPicture>(id);
                return Mapper.Map<ExamModel.AnswerStepPicture>(dalAnswerStepPicture);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> AddAsync(IAnswerStepPicture entity)
        {
            try
            {
                return await Repository.AddAsync<DALModel.AnswerStepPicture>(Mapper.Map<DALModel.AnswerStepPicture>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> UpdateAsync(IAnswerStepPicture entity)
        {
            try
            {
                return await Repository.UpdateAsync<DALModel.AnswerStepPicture>(Mapper.Map<DALModel.AnswerStepPicture>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> DeleteAsync(IAnswerStepPicture entity)
        {
            try 
            {
                return await Repository.DeleteAsync<DALModel.AnswerStepPicture>(Mapper.Map<DALModel.AnswerStepPicture>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> DeleteAsync(Guid id)
        {
            try
            {
                return await Repository.DeleteAsync<DALModel.AnswerStepPicture>(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
