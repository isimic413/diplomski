using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.DAL.Models;
using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;

namespace ExamPreparation.Repository
{
    public class AnswerStepRepository: IAnswerStepRepository
    {
        #region Properties

        protected IRepository Repository { get; private set; }

        #endregion Properties

        #region Constructors

        public AnswerStepRepository(IRepository repository)
        {
            Repository = repository;
        }

        #endregion Constructors

        #region Methods

        #region Get

        public virtual async Task<List<IAnswerStep>> GetAsync(AnswerStepFilter filter = null)
        {
            try
            { 
                return Mapper.Map<List<IAnswerStep>>(
                    await Repository.WhereAsync<AnswerStep>()
                    .OrderBy(filter.SortOrder)
                    .Skip<AnswerStep>((filter.PageNumber - 1) * filter.PageSize)
                    .Take<AnswerStep>(filter.PageSize)
                    .ToListAsync<AnswerStep>()
                    );
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<IAnswerStep> GetAsync(Guid id)
        {
            try
            {
                return Mapper.Map<IAnswerStep>(await Repository.SingleAsync<AnswerStep>(id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<IAnswerStep>> GetStepsAsync(Guid questionId)
        {
            try
            {
                return Mapper.Map<List<IAnswerStep>>(
                    await Repository.WhereAsync<AnswerStep>()
                    .Where<AnswerStep>(item => item.QuestionId == questionId)
                    .OrderBy(item => item.StepNumber)
                    .ToListAsync<AnswerStep>()
                    );
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Get

        #region Insert

        public async Task AddAsync(IUnitOfWork unitOfWork, IAnswerStep entity, IAnswerStepPicture picture = null)
        {
            try
            {
                if (entity.StepNumber < 1)
                {
                    throw new ArgumentException("StepNumber cannot be less than 1.");
                }
                if (entity.HasPicture && picture == null)
                {
                    throw new ArgumentNullException("AnswerStep.HasPicture set to true, but no AnswerStepPicture sent.");
                }
                if (!entity.HasPicture && picture != null)
                {
                    throw new ArgumentException("AnswerStep.HasPicture set to false, but AnswerStepPicture was sent.");
                }
                
                if (entity.StepNumber == 1)
                {
                    var question = await Repository.SingleAsync<Question>(entity.QuestionId);
                    if (!question.HasSteps)
                    {
                        question.HasSteps = true;
                        await unitOfWork.UpdateAsync<Question>(question);
                        await unitOfWork.AddAsync<AnswerStep>(Mapper.Map<AnswerStep>(entity));

                        if (entity.HasPicture)
                        {
                            await unitOfWork.AddAsync<AnswerStepPicture>(Mapper.Map<AnswerStepPicture>(picture));
                        }

                        return;
                    }
                    else
                    {
                        throw new ArgumentException("Question (id=" + question.Id + ") already has AnswerStep number 1.");
                    }
                }
                else // entity.StepNumber > 1
                {
                    var question = await Repository.SingleAsync<Question>(entity.QuestionId);
                    if (question.HasSteps)
                    {
                        var lastStepNumber = Repository.WhereAsync<AnswerStep>()
                            .Where<AnswerStep>(item => item.QuestionId == question.Id)
                            .OrderBy<AnswerStep>("StepNumber")
                            .Last<AnswerStep>()
                            .StepNumber;

                        if (entity.StepNumber == lastStepNumber + 1)
                        {
                            await unitOfWork.AddAsync<AnswerStep>(Mapper.Map<AnswerStep>(entity));

                            if (entity.HasPicture)
                            {
                                await unitOfWork.AddAsync<AnswerStepPicture>(Mapper.Map<AnswerStepPicture>(picture));
                            }

                            return;
                        }
                        else
                        {
                            throw new ArgumentException("AnswerStep.StepNumber should be " + (lastStepNumber + 1) + ".");
                        }
                    }
                    else // !question.HasSteps
                    {
                        throw new ArgumentException("AnswerStep number 1 for Question (id=" + question.Id + ") must be set first.");
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<int> InsertAsync(IAnswerStep entity, IAnswerStepPicture picture = null)
        {
            try
            {
                IUnitOfWork unitOfWork = Repository.CreateUnitOfWork();
                await AddAsync(unitOfWork, entity, picture);

                return await unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Insert

        #region Update

        private async Task UpdateStepAsync(IUnitOfWork unitOfWork, IAnswerStep entity, 
            AnswerStep step, IAnswerStepPicture picture = null)
        {
            try
            {
                if(step.HasPicture) // entity in DB has picture
                {
                    var dalPicture = await Repository.WhereAsync<AnswerStepPicture>()
                            .Where<AnswerStepPicture>(item => item.AnswerStepId == entity.Id)
                            .SingleAsync<AnswerStepPicture>();

                    if (entity.HasPicture)
                    {
                        if (picture != null)
                        {
                            dalPicture.Picture = picture.Picture;
                            await unitOfWork.UpdateAsync<AnswerStepPicture>(dalPicture);
                        }
                    }
                    else // !entity.HasPicture
                    {
                        await unitOfWork.DeleteAsync<AnswerStepPicture>(dalPicture.Id);
                    }
                }
                else // entity in DB does not have picture
                {
                    if (entity.HasPicture)
                    {
                        if (picture != null)
                        {
                            await unitOfWork.AddAsync<AnswerStepPicture>(
                                Mapper.Map<AnswerStepPicture>(picture));
                        }
                        else
                        {
                            throw new ArgumentNullException("AnswerStepPicture");
                        }
                    }
                }

                await unitOfWork.UpdateAsync<AnswerStep>(Mapper.Map<AnswerStep>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task UnitOfWorkUpdateAsync(IUnitOfWork unitOfWork, 
            IAnswerStep entity, IAnswerStepPicture picture = null)
        {
            try
            {
                var dalStep = await Repository.SingleAsync<AnswerStep>(entity.Id);

                if (dalStep.StepNumber == entity.StepNumber)
                {
                    await UpdateStepAsync(unitOfWork, entity, dalStep, picture);
                }
                else
                {
                    var steps = await GetStepsAsync(entity.QuestionId);

                    var oldStepNumber = dalStep.StepNumber;
                    var newStepNumber = entity.StepNumber;
                    var tempStepNumber = (short)(steps.Count + 1);

                    dalStep.StepNumber = tempStepNumber;
                    await unitOfWork.UpdateAsync<AnswerStep>(dalStep);

                    if (newStepNumber < oldStepNumber) // update 5 to 2
                    {
                        for (int i = newStepNumber - 1; i < oldStepNumber; i++)
                        {
                            steps[i].StepNumber++;
                            await unitOfWork.UpdateAsync<AnswerStep>(
                                Mapper.Map<AnswerStep>(steps[i]));
                        }
                    }
                    else // update 2 to 5
                    {
                        for (int i = oldStepNumber; i < newStepNumber; i++)
                        {
                            steps[i].StepNumber--;
                            await unitOfWork.UpdateAsync<AnswerStep>(
                                Mapper.Map<AnswerStep>(steps[i]));
                        }
                    }
                    dalStep.StepNumber = newStepNumber;
                    await UpdateStepAsync(unitOfWork, entity, dalStep, picture);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<int> UpdateAsync(IAnswerStep entity, IAnswerStepPicture picture = null)
        {
            try
            {
                IUnitOfWork unitOfWork = Repository.CreateUnitOfWork();
                await UnitOfWorkUpdateAsync(unitOfWork, entity, picture);
                return await unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Update

        #region Delete

        public async Task UnitOfWorkDeleteAsync(IUnitOfWork unitOfWork, IAnswerStep entity)
        {
            try
            {
                // if entity has pictures, delete them first
                if (entity.HasPicture)
                {
                    var pictures = Repository.WhereAsync<AnswerStepPicture>()
                        .Where(e => e.AnswerStepId == entity.Id);

                    foreach (var picture in pictures)
                    {
                        await unitOfWork.DeleteAsync<AnswerStepPicture>(Mapper.Map<AnswerStepPicture>(picture));
                    }
                }

                var question = await Repository.SingleAsync<Question>(entity.QuestionId);
                var steps = await Repository.WhereAsync<AnswerStep>()
                    .Where<AnswerStep>(item => item.QuestionId == entity.QuestionId)
                    .ToListAsync<AnswerStep>();

                // if there was only one step to show, update Question.HasSteps
                if (steps.Count == 1)
                {
                    question.HasSteps = false;
                    await unitOfWork.UpdateAsync<Question>(question);
                }
                else // if steps.Count > 1
                {
                    if (entity.StepNumber < steps.Count) // it was not the last step
                    {
                        // update StepNumber where necessary
                        foreach (var step in steps)
                        {
                            if (step.StepNumber > entity.StepNumber)
                            {
                                step.StepNumber--;
                                await unitOfWork.UpdateAsync<AnswerStep>(step);
                            }
                        }
                    }
                }

                await unitOfWork.DeleteAsync<AnswerStep>(Mapper.Map<AnswerStep>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<int> DeleteAsync(IAnswerStep entity)
        {

            try
            {
                IUnitOfWork unitOfWork = Repository.CreateUnitOfWork();

                await UnitOfWorkDeleteAsync(unitOfWork, entity);
                return await unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async virtual Task<int> DeleteAsync(Guid id)
        {
            try
            {
                return await DeleteAsync(Mapper.Map<IAnswerStep>(
                    await Repository.SingleAsync<AnswerStep>(id))
                    );
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Delete

        #endregion Methods
    }
}
