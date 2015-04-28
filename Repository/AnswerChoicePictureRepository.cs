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
    public class AnswerChoicePictureRepository: IAnswerChoicePictureRepository
    {
        protected IRepository Repository { get; private set; }

        public AnswerChoicePictureRepository(IRepository repository)
        {
            Repository = repository;
        }

        public virtual async Task<List<IAnswerChoicePicture>> GetAsync(string sortOrder = "choiceId", 
            int pageNumber = 1, int pageSize = 50)
        {
            try
            {
                List<DALModel.AnswerChoicePicture> page;
                pageSize = (pageSize > 50) ? 50 : pageSize;

                switch (sortOrder)
                {
                    case "choiceId":
                        page = await Repository.WhereAsync<DALModel.AnswerChoicePicture>()
                            .OrderBy(item => item.AnswerChoiceId)
                            .Skip<DALModel.AnswerChoicePicture>((pageNumber - 1) * pageSize)
                            .Take<DALModel.AnswerChoicePicture>(pageSize)
                            .ToListAsync<DALModel.AnswerChoicePicture>();
                        break;
                    default:
                        throw new ArgumentException("Invalid sortOrder.");
                }

                return Mapper.Map<List<IAnswerChoicePicture>>(page); // testirati!
                // new List<IAnswerChoicePicture>(
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<IAnswerChoicePicture> GetAsync(Guid id)
        {
            try
            {
                return Mapper.Map<ExamModel.AnswerChoicePicture>(
                    await Repository.SingleAsync<DALModel.AnswerChoicePicture>(id)
                    );
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual Task<int> AddAsync(IAnswerChoicePicture entity)
        {
            try
            {
                return Repository.AddAsync<DALModel.AnswerChoicePicture>(Mapper.Map<DALModel.AnswerChoicePicture>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual Task<int> UpdateAsync(IAnswerChoicePicture entity)
        {
            try
            {
                return Repository.UpdateAsync<DALModel.AnswerChoicePicture>(Mapper.Map<DALModel.AnswerChoicePicture>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual Task<int> DeleteAsync(IAnswerChoicePicture entity)
        {
            try
            {
                return Repository.DeleteAsync<DALModel.AnswerChoicePicture>(Mapper.Map<DALModel.AnswerChoicePicture>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual Task<int> DeleteAsync(Guid id)
        {
            return Repository.DeleteAsync<DALModel.AnswerChoicePicture>(id);
        }

        public virtual async Task<IAnswerChoicePicture> GetByChoiceIdAsync(Guid choiceId)
        {
            try
            {
                return Mapper.Map<ExamModel.AnswerChoicePicture>(
                    await Repository.WhereAsync<DALModel.AnswerChoicePicture>()
                    .Where(item => item.AnswerChoiceId == choiceId)
                    .SingleAsync()
                    );
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
