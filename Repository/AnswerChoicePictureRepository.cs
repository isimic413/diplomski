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
    public class AnswerChoicePictureRepository: IAnswerChoicePictureRepository
    {
        protected IRepository Repository { get; set; }

        public AnswerChoicePictureRepository(IRepository repository)
        {
            Repository = repository;
        }

        public virtual async Task<List<IAnswerChoicePicture>> GetAsync(string sortOrder = "choiceId", 
            int pageNumber = 0, int pageSize = 50)
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

                // Cannot implicitly convert type List<ExamModel.AnswerChoicePicture> to List<IAnswerChoicePicture>
                return new List<IAnswerChoicePicture>(Mapper.Map<List<ExamModel.AnswerChoicePicture>>(page));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<IAnswerChoicePicture> GetAsync(Guid id)
        {
            var dalAnswerChoicePicture = await Repository.SingleAsync<DALModel.AnswerChoicePicture>(id);
            return Mapper.Map<ExamModel.AnswerChoicePicture>(dalAnswerChoicePicture);
        }

        public virtual async Task<int> AddAsync(IAnswerChoicePicture entity)
        {
            try
            {
                return await Repository.AddAsync<DALModel.AnswerChoicePicture>(Mapper.Map<DALModel.AnswerChoicePicture>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> UpdateAsync(IAnswerChoicePicture entity)
        {
            try
            {
                return await Repository.UpdateAsync<DALModel.AnswerChoicePicture>(Mapper.Map<DALModel.AnswerChoicePicture>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> DeleteAsync(IAnswerChoicePicture entity)
        {
            try
            {
                return await Repository.DeleteAsync<DALModel.AnswerChoicePicture>(Mapper.Map<DALModel.AnswerChoicePicture>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> DeleteAsync(Guid id)
        {
            return await Repository.DeleteAsync<DALModel.AnswerChoicePicture>(id);
        }
    }
}
