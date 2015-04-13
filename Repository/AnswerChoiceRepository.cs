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
    public class AnswerChoiceRepository: IAnswerChoiceRepository
    {
        protected IRepository Repository { get; set; }

        public AnswerChoiceRepository(IRepository repository)
        {
            Repository = repository;
        }

        private IUnitOfWork CreateUnitOfWork()
        {
            return Repository.CreateUnitOfWork();
        }

        public virtual async Task<List<IAnswerChoice>> GetAsync(string sortOrder = "problemId", int pageNumber = 0, int pageSize = 50)
        {
            try
            {
                List<DALModel.AnswerChoice> page;
                pageSize = (pageSize > 50) ? 50 : pageSize;

                switch (sortOrder)
                { 
                    case "problemId":
                        page = await Repository.WhereAsync<DALModel.AnswerChoice>()
                            .OrderBy(item => item.ProblemId)
                            .Skip<DALModel.AnswerChoice>((pageNumber - 1) * pageSize)
                            .Take<DALModel.AnswerChoice>(pageSize)
                            .ToListAsync<DALModel.AnswerChoice>();
                        break;
                    default:
                        throw new ArgumentException("Invalid sortOrder.");
                }

                return new List<IAnswerChoice>(Mapper.Map<List<ExamModel.AnswerChoice>>(page));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<IAnswerChoice> GetAsync(Guid id)
        {
            try
            {
                var dalAnswerChoice = await Repository.SingleAsync<DALModel.AnswerChoice>(id);
                return Mapper.Map<ExamModel.AnswerChoice>(dalAnswerChoice);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> AddAsync(IAnswerChoice entity, IAnswerChoicePicture picture = null)
        {
            try
            {
                if (picture != null && entity.HasPicture)
                {
                    IUnitOfWork unitOfWork = CreateUnitOfWork();
                    
                    await unitOfWork.AddAsync<DALModel.AnswerChoice>(Mapper.Map<DALModel.AnswerChoice>(entity));
                    await unitOfWork.AddAsync<DALModel.AnswerChoicePicture>(Mapper.Map<DALModel.AnswerChoicePicture>(picture));
                    
                    return await unitOfWork.CommitAsync();
                }
                else if (picture == null && !entity.HasPicture)
                {
                    return await Repository.AddAsync<DALModel.AnswerChoice>(Mapper.Map<DALModel.AnswerChoice>(entity));
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> UpdateAsync(IAnswerChoice entity, IAnswerChoicePicture picture = null)
        {
            try
            {
                if (picture != null && entity.HasPicture)
                {
                    IUnitOfWork unitOfWork = CreateUnitOfWork();

                    await unitOfWork.UpdateAsync<DALModel.AnswerChoice>(Mapper.Map<DALModel.AnswerChoice>(entity));
                    await unitOfWork.UpdateAsync<DALModel.AnswerChoicePicture>(Mapper.Map<DALModel.AnswerChoicePicture>(picture));

                    return await unitOfWork.CommitAsync();
                }
                else if (picture == null && !entity.HasPicture)
                {
                    return await Repository.UpdateAsync<DALModel.AnswerChoice>(Mapper.Map<DALModel.AnswerChoice>(entity));
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> DeleteAsync(IAnswerChoice entity)
        {
            try
            {
                if (entity.HasPicture)
                {
                    IUnitOfWork unitOfWork = CreateUnitOfWork();
                    var pictures = Repository.WhereAsync<DALModel.AnswerChoicePicture>()
                        .Where(e => e.AnswerChoiceId == entity.Id);

                    await unitOfWork.DeleteAsync<DALModel.AnswerChoice>(Mapper.Map<DALModel.AnswerChoice>(entity));                  
                    foreach(var picture in pictures) 
                    {
                        await unitOfWork.DeleteAsync<DALModel.AnswerChoicePicture>(Mapper.Map<DALModel.AnswerChoicePicture>(picture));
                    }

                    return await unitOfWork.CommitAsync();
                }
                else
                {
                    return await Repository.DeleteAsync<DALModel.AnswerChoice>(Mapper.Map<DALModel.AnswerChoice>(entity));
                }
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
                return await Repository.DeleteAsync<DALModel.AnswerChoice>(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
