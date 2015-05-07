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

        #region Get

        public virtual async Task<List<IAnswerChoice>> GetAsync(AnswerChoiceFilter filter = null)
        {
            try
            {
                List<IAnswerChoice> page = Mapper.Map<List<IAnswerChoice>>(
                            await Repository.WhereAsync<AnswerChoice>()
                            .OrderBy(filter.SortOrder)
                            .Skip<AnswerChoice>((filter.PageNumber - 1) * filter.PageSize)
                            .Take<AnswerChoice>(filter.PageSize)
                            .ToListAsync<AnswerChoice>()
                            );


                foreach (var choice in page)
                {
                    if (choice.HasPicture)
                    {
                        choice.AnswerChoicePictures = Mapper.Map<List<IAnswerChoicePicture>>(
                            await Repository.WhereAsync<AnswerChoicePicture>()
                            .Where<AnswerChoicePicture>(item => item.AnswerChoiceId == choice.Id)
                            .ToListAsync()
                            );

                        if (choice.AnswerChoicePictures.Count < 1)
                        {
                            throw new ArgumentNullException("AnswerChoice.HasPicture (id=" + choice.Id 
                                + ") set to true, but no AnswerChoicePicture found for that AnswerChoice.");
                        }
                    }
                    else
                    {
                        choice.AnswerChoicePictures = null;
                    }
                }

                return page;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<IAnswerChoice> GetAsync(Guid id)
        {
            try
            {
                var result = Mapper.Map<IAnswerChoice>(await Repository.SingleAsync<AnswerChoice>(id));
                if (result.HasPicture)
                {
                    result.AnswerChoicePictures = Mapper.Map<List<IAnswerChoicePicture>>(
                            await Repository.WhereAsync<AnswerChoicePicture>()
                            .Where<AnswerChoicePicture>(item => item.AnswerChoiceId == result.Id)
                            .ToListAsync()
                            );

                    if (result.AnswerChoicePictures.Count < 1)
                    {
                        throw new ArgumentNullException("AnswerChoice.HasPicture (id=" + result.Id
                            + ") set to true, but no AnswerChoicePicture found for that AnswerChoice.");
                    }
                }
                else
                {
                    result.AnswerChoicePictures = null;
                }

                return result;
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
                List<IAnswerChoice> choices = Mapper.Map<List<IAnswerChoice>>(
                    await Repository.WhereAsync<AnswerChoice>()
                    .Where<AnswerChoice>(item => item.QuestionId == questionId)
                    .ToListAsync<AnswerChoice>()
                    );

                foreach (var choice in choices)
                {
                    if (choice.HasPicture)
                    {
                        choice.AnswerChoicePictures = Mapper.Map<List<IAnswerChoicePicture>>(
                            await Repository.WhereAsync<AnswerChoicePicture>()
                            .Where<AnswerChoicePicture>(item => item.AnswerChoiceId == choice.Id)
                            .ToListAsync()
                            );

                        if (choice.AnswerChoicePictures.Count < 1)
                        {
                            throw new ArgumentNullException("AnswerChoice.HasPicture (id=" + choice.Id
                                + ") set to true, but no AnswerChoicePicture found for that AnswerChoice.");
                        }
                        // dodati provjeru prema tipu zadatka (QuestionType)
                    }
                    else
                    {
                        choice.AnswerChoicePictures = null;
                    }
                }

                return choices;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Get

        #region Insert

        public virtual async Task<int> InsertAsync(IAnswerChoice entity, IAnswerChoicePicture picture = null)
        {
            try
            {
                if (entity.HasPicture)
                {
                    if (picture != null)
                    {
                        IUnitOfWork unitOfWork = Repository.CreateUnitOfWork();

                        await unitOfWork.AddAsync<AnswerChoice>(Mapper.Map<AnswerChoice>(entity));
                        await unitOfWork.AddAsync<AnswerChoicePicture>(Mapper.Map<AnswerChoicePicture>(picture));

                        return await unitOfWork.CommitAsync();
                    }
                    else
                    {
                        throw new ArgumentNullException("AnswerChoice.HasPicture set to true, but no AnswerChoicePicture sent.");
                    }
                }
                else // !entity.HasPicture
                {
                    if (picture == null)
                    {
                        return await Repository.InsertAsync<AnswerChoice>(Mapper.Map<AnswerChoice>(entity));
                    }
                    else
                    {
                        throw new ArgumentException("AnswerChoice.HasPicture set to false, but AnswerChoicePicture for this AnswerChoice was sent.");
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Insert
        
        #region Update

        public virtual async Task<int> UpdateAsync(IAnswerChoice entity, IAnswerChoicePicture picture = null)
        {
            try
            {
                IUnitOfWork unitOfWork = Repository.CreateUnitOfWork();

                var updatePicture = (await Repository.SingleAsync<AnswerChoice>(entity.Id)).HasPicture;

                if (updatePicture) // entity in DB has picture
                {
                    var dalPicture = await Repository.WhereAsync<AnswerChoicePicture>()
                            .Where<AnswerChoicePicture>(item => item.AnswerChoiceId == entity.Id)
                            .SingleAsync<AnswerChoicePicture>();

                    if (entity.HasPicture)
                    {
                        if (picture != null)
                        {
                            dalPicture.Picture = picture.Picture;
                            await unitOfWork.UpdateAsync<AnswerChoicePicture>(dalPicture);
                        }
                    }
                    else // !entity.HasPicture
                    {
                        await unitOfWork.DeleteAsync<AnswerChoicePicture>(dalPicture.Id);
                    }
                }
                else // entity in DB does not have picture
                {
                    if (entity.HasPicture)
                    {
                        if (picture != null)
                        {
                            await unitOfWork.AddAsync<AnswerChoicePicture>(
                                Mapper.Map<AnswerChoicePicture>(picture));
                        }
                        else
                        {
                            throw new ArgumentNullException("AnswerChoicePicture");
                        }
                    }
                }

                await unitOfWork.UpdateAsync<AnswerChoice>(Mapper.Map<AnswerChoice>(entity));
                return await unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Update

        #region Delete

        public async Task<int> DeleteAsync(IAnswerChoice entity)
        {
            try
            {
                var choices = await Repository.WhereAsync<AnswerChoice>()
                    .Where<AnswerChoice>(item => item.QuestionId == entity.QuestionId)
                    .ToListAsync();

                if (choices.Count == 1)
                {
                    throw new InvalidOperationException("Cannot delete the only correct answer for question.");
                }


                if (entity.HasPicture)
                {
                    IUnitOfWork unitOfWork = Repository.CreateUnitOfWork();
                    var pictures = Repository.WhereAsync<AnswerChoicePicture>()
                        .Where(e => e.AnswerChoiceId == entity.Id);
              
                    foreach(var picture in pictures) 
                    {
                        await unitOfWork.DeleteAsync<AnswerChoicePicture>(Mapper.Map<AnswerChoicePicture>(picture));
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

        #endregion Delete

        #endregion Methods
    }
}
