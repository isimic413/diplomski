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
    public class AnswerChoiceRepository: IAnswerChoiceRepository
    {
        protected IRepository Repository { get; set; }

        public AnswerChoiceRepository(IRepository repository)
        {
            Repository = repository;
        }

        // public??
        public IUnitOfWork CreateUnitOfWork()
        {
            return Repository.CreateUnitOfWork();
        }

        public virtual async Task<List<IAnswerChoice>> GetAsync(string sortOrder = "problemId", int pageNumber = 0, int pageSize = 50)
        {
            pageSize = (pageSize > 50) ? 50 : pageSize;

            var page = await Repository.WhereAsync<DALModel.AnswerChoice>()
                .OrderBy(item => item.ProblemId)
                .Skip<DALModel.AnswerChoice>((pageNumber - 1) * pageSize)
                .Take<DALModel.AnswerChoice>(pageSize)
                .ToListAsync<DALModel.AnswerChoice>();

            return new List<IAnswerChoice>(Mapper.Map<List<ExamModel.AnswerChoice>>(page));
        }

        public virtual async Task<IAnswerChoice> GetAsync(Guid id)
        {
            var dalAnswerChoice = await Repository.SingleAsync<DALModel.AnswerChoice>(id);
            return Mapper.Map<ExamModel.AnswerChoice>(dalAnswerChoice);
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
            catch 
            {
                return 0;
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
            catch
            {
                return 0;
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
            catch
            {
                return 0;
            }
        }

        public virtual async Task<int> DeleteAsync(Guid id)
        {
            return await Repository.DeleteAsync<DALModel.AnswerChoice>(id);
        }
    }
}
