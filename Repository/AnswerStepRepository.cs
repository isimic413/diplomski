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

        private IUnitOfWork CreateUnitOfWork()
        {
            return Repository.CreateUnitOfWork();
        }

        public virtual async Task<List<IAnswerStep>> GetAsync(string sortOrder = "problemId", int pageNumber = 0, int pageSize = 500)
        {
            try
            { 
                List<DALModel.AnswerStep> page;
                pageSize = (pageSize > 50) ? 50 : pageSize;

                switch (sortOrder)
                {
                    case "problemId":
                        page = await Repository.WhereAsync<DALModel.AnswerStep>()
                            .OrderBy(item => item.ProblemId)
                            .ThenBy(item => item.StepNumber)
                            .Skip<DALModel.AnswerStep>((pageNumber - 1) * pageSize)
                            .Take<DALModel.AnswerStep>(pageSize)
                            .ToListAsync<DALModel.AnswerStep>();
                        break;
                    default:
                        throw new ArgumentException("Invalid sortOrder.");
                }

                return new List<IAnswerStep>(Mapper.Map<List<ExamModel.AnswerStep>>(page));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<IAnswerStep> GetAsync(Guid id)
        {
            try
            {
                var dalAnswerStep = await Repository.SingleAsync<DALModel.AnswerStep>(id);
                return Mapper.Map<ExamModel.AnswerStep>(dalAnswerStep);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
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
            catch (Exception e)
            {
                throw new Exception(e.ToString());
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
            catch (Exception e)
            {
                throw new Exception(e.ToString());
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
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> DeleteAsync(Guid id)
        {
            try
            {
                return await Repository.DeleteAsync<DALModel.AnswerStep>(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
