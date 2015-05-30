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

        public virtual async Task<List<IAnswerStep>> GetAsync(AnswerStepFilter filter = null)
        {
            try
            {
                if (filter != null)
                {
                    return Mapper.Map<List<IAnswerStep>>(
                        await Repository.WhereAsync<AnswerStep>()
                        .OrderBy(filter.SortOrder)
                        .Skip<AnswerStep>((filter.PageNumber - 1) * filter.PageSize)
                        .Take<AnswerStep>(filter.PageSize)
                        .Include(item => item.AnswerStepPictures)
                        .ToListAsync<AnswerStep>()
                        );
                }
                else // return all
                {
                    return Mapper.Map<List<IAnswerStep>>(
                        await Repository.WhereAsync<AnswerStep>()
                        .Include(item => item.AnswerStepPictures)
                        .ToListAsync()
                        );
                }
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
                return Mapper.Map<IAnswerStep>(
                    await Repository.WhereAsync<AnswerStep>()
                    .Where(item => item.Id == id)
                    .Include(item => item.AnswerStepPictures)
                    .SingleAsync()
                    );
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
                    .Include(item => item.AnswerStepPictures)
                    .ToListAsync<AnswerStep>()
                    );
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> AddAsync(IUnitOfWork unitOfWork, IAnswerStep entity)
        {
            try
            {
                var numberOfSteps = (short)(await GetStepsAsync(entity.QuestionId)).Count;
                if (entity.StepNumber < 1 || entity.StepNumber > numberOfSteps)
                {
                    throw new ArgumentException("Invalid StepNumber.");
                }

                var stepEntity = Mapper.Map<AnswerStep>(entity);

                var stepNumber = stepEntity.StepNumber;
                stepEntity.StepNumber = numberOfSteps;
                stepEntity.StepNumber++;

                // insert as last step
                await unitOfWork.AddAsync<AnswerStep>(stepEntity);

                // update step numbers
                stepEntity.StepNumber = stepNumber;
                return await UpdateAsync(unitOfWork, stepEntity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<int> InsertAsync(IAnswerStep entity)
        {
            try
            {
                IUnitOfWork unitOfWork = Repository.CreateUnitOfWork();
                await AddAsync(unitOfWork, entity);
                return await unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<int> AddAsync(IUnitOfWork unitOfWork, List<IAnswerStep> entities,
            List<IAnswerStepPicture> pictures = null)
        {
            try
            {
                var result = 0;
                entities = entities.OrderBy(item => item.StepNumber).ToList();

                for (int i = 0; i < entities.Count; i++)
                {
                    result += await this.AddAsync(unitOfWork, entities.ElementAt(i));
                }

                if (pictures != null)
                {
                    foreach (var picture in pictures)
                    {
                        result += await unitOfWork.AddAsync<AnswerStepPicture>(
                            Mapper.Map<AnswerStepPicture>(picture));
                    }
                }

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task<int> UpdateAsync(IUnitOfWork unitOfWork, AnswerStep entity)
        {
            try
            {
                var steps = await Repository.WhereAsync<AnswerStep>()
                    .Where(item => item.QuestionId == entity.QuestionId)
                    .OrderBy(item => item.StepNumber)
                    .ToListAsync();

                var numberOfSteps = steps.Count;
                var oldStepNumber = (await Repository.SingleAsync<AnswerStep>(entity.Id)).StepNumber;
                var newStepNumber = entity.StepNumber;

                if (oldStepNumber == newStepNumber)
                {
                    return await unitOfWork.UpdateAsync<AnswerStep>(entity);
                }
                else
                {
                    var tempStepNumber = (short)(numberOfSteps + 1);
                    entity.StepNumber = tempStepNumber;
                    await unitOfWork.UpdateAsync<AnswerStep>(entity);

                    if (newStepNumber < oldStepNumber) // update 5 to 2
                    {
                        for (int i = newStepNumber - 1; i < oldStepNumber - 1; i++)
                        {
                            steps[i].StepNumber++;
                            await unitOfWork.UpdateAsync<AnswerStep>(steps[i]);
                        }
                    }
                    else // update 2 to 5
                    {
                        for (int i = oldStepNumber; i < newStepNumber; i++)
                        {
                            steps[i].StepNumber--;
                            await unitOfWork.UpdateAsync<AnswerStep>(steps[i]);
                        }
                    }
                    entity.StepNumber = newStepNumber;
                    return await unitOfWork.UpdateAsync<AnswerStep>(entity);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<int> UpdateAsync(IUnitOfWork unitOfWork, IAnswerStep entity)
        {
            try
            {
                return UpdateAsync(unitOfWork, Mapper.Map<AnswerStep>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> UpdateAsync(IAnswerStep entity)
        {
            try
            {
                IUnitOfWork unitOfWork = Repository.CreateUnitOfWork();
                await UpdateAsync(unitOfWork, entity);
                return await unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> DeleteAsync(IUnitOfWork unitOfWork, IAnswerStep entity)
        {
            try
            {
                var stepEntity = Mapper.Map<AnswerStep>(entity);

                var numberOfSteps = (await Repository.WhereAsync<AnswerStep>()
                    .Where(item => item.QuestionId == entity.QuestionId)
                    .ToListAsync())
                    .Count;

                if (entity.StepNumber < numberOfSteps)
                {
                    // move to last place
                    stepEntity.StepNumber = (short)numberOfSteps;
                    await UpdateAsync(unitOfWork, stepEntity);
                }
                
                return await unitOfWork.DeleteAsync<AnswerStep>(stepEntity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> DeleteAsync(IUnitOfWork unitOfWork, Guid questionId)
        {
            try
            {
                var result = 0;

                var steps = await Repository.WhereAsync<AnswerStep>()
                    .Where<AnswerStep>(item => item.QuestionId == questionId)
                    .Include(item => item.AnswerStepPictures)
                    .ToListAsync<AnswerStep>();

                foreach (var step in steps)
                {
                    foreach (var picture in step.AnswerStepPictures)
                    {
                        result += await unitOfWork.DeleteAsync<AnswerStepPicture>(picture);
                    }
                    result += await unitOfWork.DeleteAsync<AnswerStep>(step);
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> DeleteAsync(IAnswerStep entity)
        {
            try
            {
                var unitOfWork = Repository.CreateUnitOfWork();

                var pictures = await Repository.WhereAsync<AnswerStepPicture>()
                    .Where(item => item.AnswerStepId == entity.Id)
                    .ToListAsync();

                if (pictures.Count > 0)
                {
                    foreach (var picture in pictures)
                    {
                        await unitOfWork.DeleteAsync<AnswerStepPicture>(picture);
                    }
                }
                await this.DeleteAsync(unitOfWork, entity);

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
                    await Repository.SingleAsync<AnswerStep>(id)));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<IUnitOfWork> CreateUnitOfWork()
        {
            try
            {
                return Task.FromResult(Repository.CreateUnitOfWork());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Methods
    }
}
