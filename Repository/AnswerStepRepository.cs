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
    public class AnswerStepRepository: IAnswerStepRepository
    {
        protected IRepository Repository { get; set; }

        public AnswerStepRepository(IRepository repository)
        {
            Repository = repository;
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            return Repository.CreateUnitOfWork();
        }

        public virtual async Task<List<IAnswerStep>> GetAsync(
            string sortOrderFirst = "problemId", string sortOrderSecond = "stepNumber",
            int pageNumber = 0, int pageSize = 500)
        {
            pageSize = (pageSize > 50) ? 50 : pageSize;

            var page = await Repository.WhereAsync<DALModel.AnswerStep>()
                .OrderBy(item => item.ProblemId)
                .ThenBy(item => item.StepNumber)
                .Skip<DALModel.AnswerStep>((pageNumber - 1) * pageSize)
                .Take<DALModel.AnswerStep>(pageSize)
                .ToListAsync<DALModel.AnswerStep>();

            return new List<IAnswerStep>(Mapper.Map<List<ExamModel.AnswerStep>>(page));
        }

        public virtual async Task<IAnswerStep> GetAsync(Guid id)
        {
            var dalAnswerStep = await Repository.SingleAsync<DALModel.AnswerStep>(id);
            return Mapper.Map<ExamModel.AnswerStep>(dalAnswerStep);
        }

        public virtual async Task<int> AddAsync(IAnswerStep entity, IAnswerStepPicture picture = null)
        {
            try
            {
                if (picture != null && entity.HasPicture)
                {
                    IUnitOfWork unitOfWork = CreateUnitOfWork();

                    await unitOfWork.AddAsync<DALModel.AnswerStep>(Mapper.Map<DALModel.AnswerStep>(entity));
                    await unitOfWork.AddAsync<DALModel.AnswerStepPicture>(Mapper.Map<DALModel.AnswerStepPicture>(picture));

                    return await unitOfWork.CommitAsync();
                }
                else if (picture == null && !entity.HasPicture)
                {
                    return await Repository.AddAsync<DALModel.AnswerStep>(Mapper.Map<DALModel.AnswerStep>(entity));
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

        public virtual async Task<int> UpdateAsync(IAnswerStep entity, IAnswerStepPicture picture) // picture...?
        {
            try
            {
                if (picture != null && entity.HasPicture)
                {
                    IUnitOfWork unitOfWork = CreateUnitOfWork();

                    await unitOfWork.UpdateAsync<DALModel.AnswerStep>(Mapper.Map<DALModel.AnswerStep>(entity));
                    await unitOfWork.UpdateAsync<DALModel.AnswerStepPicture>(Mapper.Map<DALModel.AnswerStepPicture>(picture));

                    return await unitOfWork.CommitAsync();
                }
                else if (picture == null && !entity.HasPicture)
                {
                    return await Repository.UpdateAsync<DALModel.AnswerStep>(Mapper.Map<DALModel.AnswerStep>(entity));
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

        public virtual async Task<int> DeleteAsync(IAnswerStep entity)
        {

            try
            {
                if (entity.HasPicture)
                {
                    IUnitOfWork unitOfWork = CreateUnitOfWork();
                    var pictures = Repository.WhereAsync<DALModel.AnswerStepPicture>()
                        .Where(e => e.AnswerStepId == entity.Id);

                    await unitOfWork.DeleteAsync<DALModel.AnswerStep>(Mapper.Map<DALModel.AnswerStep>(entity));
                    foreach (var picture in pictures)
                    {
                        await unitOfWork.DeleteAsync<DALModel.AnswerStepPicture>(Mapper.Map<DALModel.AnswerStepPicture>(picture));
                    }

                    return await unitOfWork.CommitAsync();
                }
                else
                {
                    return await Repository.DeleteAsync<DALModel.AnswerStep>(Mapper.Map<DALModel.AnswerStep>(entity));
                }
            }
            catch
            {
                return 0;
            }
        }

        public virtual async Task<int> DeleteAsync(Guid id)
        {
            return await Repository.DeleteAsync<DALModel.AnswerStep>(id);
        }
    }
}
