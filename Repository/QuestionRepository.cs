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
    public class QuestionRepository : IQuestionRepository
    {
        #region Properties

        protected IRepository Repository { get; private set; }

        #endregion Properties

        #region Constructors

        public QuestionRepository(IRepository repository)
        {
            Repository = repository;
        }

        #endregion Constructors

        #region Methods

        #region Get

        public virtual async Task<List<IQuestion>> GetAsync(QuestionFilter filter)
        {
            try
            {
                List<IQuestion> page = Mapper.Map<List<IQuestion>>(
                    await Repository.WhereAsync<Question>()
                             .OrderBy(filter.SortOrder)
                             .Skip<Question>((filter.PageNumber - 1) * filter.PageSize)
                             .Take<Question>(filter.PageSize)
                             .ToListAsync<Question>()
                             );

                foreach(var question in page)
                {
                    if (question.HasPicture)
                    {
                        question.QuestionPictures = await GetQuestionPictures(question.Id);
                    }
                    else
                    {
                        question.QuestionPictures = null;
                    }
                }

                return page;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<IQuestion> GetAsync(Guid id)
        {
            try
            {
                return Mapper.Map<IQuestion>(await Repository.SingleAsync<Question>(id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<IQuestion>> GetByTestingAreaIdAsync(Guid testingAreaId, QuestionFilter filter)
        {
            try
            {
                List<IQuestion> page = Mapper.Map<List<IQuestion>>(
                    await Repository.WhereAsync<Question>()
                    .Where<Question>(item => item.TestingAreaId == testingAreaId)
                    .OrderBy(filter.SortOrder)
                    .Skip<Question>((filter.PageNumber - 1) * filter.PageSize)
                    .Take<Question>(filter.PageSize)
                    .ToListAsync<Question>()
                    );

                foreach (var question in page)
                {
                    if (question.HasPicture)
                    {
                        question.QuestionPictures = await GetQuestionPictures(question.Id);
                    }
                    else
                    {
                        question.QuestionPictures = null;
                    }
                }

                return page;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<IQuestion>> GetByTypeIdAsync(Guid typeId, QuestionFilter filter)
        {
            try
            {
                List<IQuestion> page = Mapper.Map<List<IQuestion>>(
                    await Repository.WhereAsync<Question>()
                    .Where<Question>(item => item.QuestionTypeId == typeId)
                    .OrderBy(filter.SortOrder)
                    .Skip<Question>((filter.PageNumber - 1) * filter.PageSize)
                    .Take<Question>(filter.PageSize)
                    .ToListAsync<Question>()
                    );

                foreach (var question in page)
                {
                    if (question.HasPicture)
                    {
                        question.QuestionPictures = await GetQuestionPictures(question.Id);
                    }
                    else
                    {
                        question.QuestionPictures = null;
                    }
                }

                return page;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected async Task<List<IQuestionPicture>> GetQuestionPictures(Guid questionId)
        {
            try
            {
                var pictures = await Repository.WhereAsync<QuestionPicture>()
                        .Where<QuestionPicture>(item => item.QuestionId == questionId)
                        .ToListAsync();

                return Mapper.Map<List<IQuestionPicture>>(pictures);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Get

        #region Insert

        public virtual async Task<int> InsertAsync(IQuestion entity, List<IAnswerChoice> choices,
            IQuestionPicture picture = null, List<IAnswerChoicePicture> choicePictures = null)
        {
            try
            {
                IUnitOfWork unitOfWork = Repository.CreateUnitOfWork();

                await unitOfWork.AddAsync<Question>(Mapper.Map<Question>(entity));

                if(entity.HasPicture)
                {
                    await unitOfWork.AddAsync<QuestionPicture>(Mapper.Map<QuestionPicture>(picture));
                }

                foreach (var choice in choices)
                {
                    await unitOfWork.AddAsync<AnswerChoice>(Mapper.Map<AnswerChoice>(choice));
                    if(choice.HasPicture)
                    {
                        var choicePicture = choicePictures.FindAll(item => item.AnswerChoiceId == choice.Id);
                        if (choicePicture.Count != 1)
                        {
                            throw new ArgumentException("List<IAnswerChoicePicture>");
                        }

                        await unitOfWork.AddAsync<AnswerChoicePicture>(
                            Mapper.Map<AnswerChoicePicture>(choicePicture.First()));
                    }
                }

                return await unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Insert 

        #region Update

        public async Task UnitOfWorkUpdateAsync(IUnitOfWork unitOfWork,
            IQuestion entity, IQuestionPicture picture = null)
        {
            try
            {
                var updatePicture = (await Repository.SingleAsync<Question>(entity.Id)).HasPicture;
                var updateSteps = (await Repository.SingleAsync<Question>(entity.Id)).HasSteps;

                if (updatePicture) // entity in DB has picture
                {
                    var dalPicture = await Repository.WhereAsync<QuestionPicture>()
                            .Where<QuestionPicture>(item => item.QuestionId == entity.Id)
                            .SingleAsync<QuestionPicture>();

                    if (entity.HasPicture)
                    {
                        if (picture != null)
                        {
                            dalPicture.Picture = picture.Picture;
                            await unitOfWork.UpdateAsync<QuestionPicture>(dalPicture);
                        }
                    }
                    else // !entity.HasPicture
                    {
                        await unitOfWork.DeleteAsync<QuestionPicture>(dalPicture.Id);
                    }
                }
                else // entity in DB does not have picture
                {
                    if (entity.HasPicture)
                    {
                        if (picture != null)
                        {
                            await unitOfWork.AddAsync<QuestionPicture>(
                                Mapper.Map<QuestionPicture>(picture));
                        }
                        else
                        {
                            throw new ArgumentNullException("QuestionPicture");
                        }
                    }
                }

                await unitOfWork.UpdateAsync<Question>(Mapper.Map<Question>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<int> UpdateAsync(IQuestion entity, IQuestionPicture picture = null)
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

        public virtual async Task<int> DeleteAsync(IQuestion entity)
        {
            // UserAnswer
            try
            {
                IUnitOfWork unitOfWork = Repository.CreateUnitOfWork();

                // Picture
                if (entity.HasPicture)
                {
                    var picture = await Repository.WhereAsync<QuestionPicture>()
                        .Where(p => entity.Id == p.QuestionId)
                        .SingleAsync();
                    await unitOfWork.DeleteAsync<QuestionPicture>(picture);
                }

                // Steps
                if (entity.HasSteps)
                {
                    var steps = await Repository.WhereAsync<AnswerStep>()
                        .Where(s => entity.Id == s.QuestionId)
                        .ToListAsync();

                    foreach (var step in steps)
                    {
                        if (step.HasPicture)
                        {
                            var stepPicture = await Repository.WhereAsync<AnswerStepPicture>()
                                .Where(s => step.Id == s.AnswerStepId)
                                .SingleAsync();
                            await unitOfWork.DeleteAsync<AnswerStepPicture>(stepPicture);
                        }
                        await unitOfWork.DeleteAsync<AnswerStep>(step);
                    }
                }

                // Choices
                var choices = await Repository.WhereAsync<AnswerChoice>()
                    .Where(c => entity.Id == c.QuestionId)
                    .ToListAsync();

                if (choices != null)
                {
                    foreach (var choice in choices)
                    {
                        if (choice.HasPicture)
                        {
                            var choicePicture = await Repository.WhereAsync<AnswerChoicePicture>()
                                .Where(s => choice.Id == s.AnswerChoiceId)
                                .SingleAsync();
                            await unitOfWork.DeleteAsync<AnswerChoicePicture>(choicePicture);
                        }
                        await unitOfWork.DeleteAsync<AnswerChoice>(choice);
                    }
                }

                // ExamQuestion
                var examQuestion = await Repository.WhereAsync<ExamQuestion>()
                    .Where<ExamQuestion>(item => item.QuestionId == entity.Id)
                    .ToListAsync();

                if (examQuestion != null)
                {
                    foreach (var eq in examQuestion)
                    {
                        await unitOfWork.DeleteAsync<ExamQuestion>(eq);
                    }
                }

                // Question
                await unitOfWork.DeleteAsync<Question>(Mapper.Map<Question>(entity));

                return await unitOfWork.CommitAsync();
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
                return await DeleteAsync(Mapper.Map<IQuestion>(
                    await Repository.SingleAsync<Question>(id))
                    );
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Delete

        #region CreateUnitOfWork

        public Task<IUnitOfWork> CreateUnitOfWork()
        {
            return Task.FromResult( Repository.CreateUnitOfWork() );
        }

        #endregion CreateUnitOfWork

        #endregion Methods

    }
}
