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
    public class QuestionPictureRepository: IQuestionPictureRepository
    {
        protected IRepository Repository { get; private set; }

        public QuestionPictureRepository(IRepository repository)
        {
            Repository = repository;
        }

        public virtual async Task<List<IQuestionPicture>> GetAsync(string sortOrder = "questionId", int pageNumber = 0, int pageSize = 50)
        {
            try
            {
                List<DALModel.QuestionPicture> page;
                pageSize = (pageSize > 50) ? 50 : pageSize;

                switch (sortOrder)
                {
                    case "questionId":
                        page = await Repository.WhereAsync<DALModel.QuestionPicture>()
                            .OrderBy(item => item.QuestionId)
                            .Skip<DALModel.QuestionPicture>((pageNumber - 1) * pageSize)
                            .Take<DALModel.QuestionPicture>(pageSize)
                            .ToListAsync<DALModel.QuestionPicture>();
                        break;
                    default:
                        throw new ArgumentException("Invalid sortOrder.");
                }

                return new List<IQuestionPicture>(Mapper.Map<List<ExamModel.QuestionPicture>>(page));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<IQuestionPicture> GetAsync(Guid id)
        {
            try
            {
                var dalQuestionPicture = await Repository.SingleAsync<DALModel.QuestionPicture>(id);
                return Mapper.Map<ExamModel.QuestionPicture>(dalQuestionPicture);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> AddAsync(IQuestionPicture entity)
        {
            try
            {
                return await Repository.AddAsync<DALModel.QuestionPicture>(Mapper.Map<DALModel.QuestionPicture>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> UpdateAsync(IQuestionPicture entity)
        {
            try
            {
                return await Repository.UpdateAsync<DALModel.QuestionPicture>(Mapper.Map<DALModel.QuestionPicture>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> DeleteAsync(IQuestionPicture entity)
        {
            try
            {
                return await Repository.DeleteAsync<DALModel.QuestionPicture>(Mapper.Map<DALModel.QuestionPicture>(entity));
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
                return await Repository.DeleteAsync<DALModel.QuestionPicture>(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
