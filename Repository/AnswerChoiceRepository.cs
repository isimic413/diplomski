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
    public class AnswerChoiceRepository: IAnswerChoiceRepository
    {
        #region Properties

        protected IRepository Repository { get; private set; }

        #endregion Properties

        #region Constructors

        public AnswerChoiceRepository(IRepository repository)
        {
            Repository = repository;
        }

        #endregion Constructors

        #region Methods

        public virtual async Task<List<IAnswerChoice>> GetAsync(AnswerChoiceFilter filter = null)
        {
            try
            {
                if (filter != null)
                {
                    return Mapper.Map<List<IAnswerChoice>>(
                                await Repository.WhereAsync<AnswerChoice>()
                                .OrderBy(filter.SortOrder)
                                .Skip<AnswerChoice>((filter.PageNumber - 1) * filter.PageSize)
                                .Take<AnswerChoice>(filter.PageSize)
                                .Include(ac => ac.AnswerChoicePictures)
                                .ToListAsync<AnswerChoice>()
                                );
                }
                else // return all
                {
                    return Mapper.Map<List<IAnswerChoice>>(
                        await Repository.WhereAsync<AnswerChoice>()
                        .Include(ac => ac.AnswerChoicePictures)
                        .ToListAsync()
                        );
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<IAnswerChoice> GetAsync(Guid id) // Include...
        {
            try
            {
                return Mapper.Map<IAnswerChoice>(
                    await Repository.WhereAsync<AnswerChoice>()
                    .Where(item => item.Id == id)
                    .Include(item => item.AnswerChoicePictures)
                    .SingleAsync()                    
                    );
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<List<IAnswerChoice>> GetCorrectAnswersAsync(Guid questionId)
        {
            try
            {
                return Mapper.Map<List<IAnswerChoice>>(
                    await Repository.WhereAsync<AnswerChoice>()
                    .Where<AnswerChoice>(item => questionId == item.QuestionId && item.IsCorrect)
                    .Include(item => item.AnswerChoicePictures)
                    .ToListAsync<AnswerChoice>()
                    );
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<List<IAnswerChoice>> GetChoicesAsync(Guid questionId)
        {
            try
            {
                return Mapper.Map<List<IAnswerChoice>>(
                    await Repository.WhereAsync<AnswerChoice>()
                    .Where<AnswerChoice>(item => item.QuestionId == questionId)
                    .Include(item => item.AnswerChoicePictures)
                    .ToListAsync<AnswerChoice>()
                    );
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual Task<int> AddAsync(IUnitOfWork unitOfWork, IAnswerChoice entity)
        {
            try
            {
                return unitOfWork.AddAsync<AnswerChoice>(
                    Mapper.Map<AnswerChoice>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<int> AddAsync(IUnitOfWork unitOfWork, List<IAnswerChoice> entities,
            List<IAnswerChoicePicture> pictures = null)
        {
            try
            {
                var hasCorrectAnswers = false;
                var result = 0;

                foreach (var entity in entities)
                {
                    result += await this.AddAsync(unitOfWork, entity);
                    if (entity.IsCorrect)
                    {
                        hasCorrectAnswers = true;
                    }
                }
                if (!hasCorrectAnswers) // no correct answers provided
                {
                    throw new ArgumentException("At least one answer must be correct.");                    
                }

                if (pictures != null)
                {
                    foreach (var picture in pictures)
                    {
                        result += await unitOfWork.AddAsync<AnswerChoicePicture>(
                            Mapper.Map<AnswerChoicePicture>(picture));
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual Task<int> InsertAsync(IAnswerChoice entity)
        {
            try
            {
                return Repository.InsertAsync<AnswerChoice>(
                    Mapper.Map<AnswerChoice>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual Task<int> UpdateAsync(IUnitOfWork unitOfWork, IAnswerChoice entity)
        {
            try
            {
                return unitOfWork.UpdateAsync<AnswerChoice>(
                    Mapper.Map<AnswerChoice>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<int> UpdateAsync(IAnswerChoice entity)
        {
            try
            {
                return await Repository.UpdateAsync<AnswerChoice>(
                    Mapper.Map<AnswerChoice>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> DeleteAsync(IAnswerChoice entity)
        {
            try
            {
                if (entity.IsCorrect)
                {
                    if ((await Repository.WhereAsync<AnswerChoice>()
                        .Where(item => item.QuestionId == entity.QuestionId)
                        .Where(item => item.IsCorrect)
                        .ToListAsync())
                        .Count == 1
                        )
                    {
                        throw new InvalidOperationException("Cannot delete the only correct answer for a question.");
                    }
                }

                var pictures = await Repository.WhereAsync<AnswerChoicePicture>()
                    .Where(item => item.AnswerChoiceId == entity.Id)
                    .ToListAsync();

                if (pictures.Count > 0)
                {
                    IUnitOfWork unitOfWork = Repository.CreateUnitOfWork();

                    foreach (var picture in pictures)
                    {
                        await unitOfWork.DeleteAsync<AnswerChoicePicture>(picture);
                    }
                    await unitOfWork.DeleteAsync<AnswerChoice>(Mapper.Map<AnswerChoice>(entity));

                    return await unitOfWork.CommitAsync();
                }
                else
                {
                    return await Repository.DeleteAsync<AnswerChoice>(Mapper.Map<AnswerChoice>(entity));
                }
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

                var choices = await Repository.WhereAsync<AnswerChoice>()
                    .Where<AnswerChoice>(item => item.QuestionId == questionId)
                    .Include(item => item.AnswerChoicePictures)
                    .ToListAsync<AnswerChoice>();

                foreach (var choice in choices)
                {
                    foreach (var picture in choice.AnswerChoicePictures)
                    {
                        result += await unitOfWork.DeleteAsync<AnswerChoicePicture>(picture);
                    }
                    result += await unitOfWork.DeleteAsync<AnswerChoice>(choice);
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            try
            {
                return await DeleteAsync(Mapper.Map<IAnswerChoice>(
                    await Repository.SingleAsync<AnswerChoice>(id))
                    );
            }
            catch (InvalidOperationException e)
            {
                throw e;
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
