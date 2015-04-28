using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using DALModel = ExamPreparation.DAL.Models;
using ExamModel = ExamPreparation.Model;

namespace ExamPreparation.Repository
{
    public class AnswerChoiceRepository: IAnswerChoiceRepository
    {
        protected IRepository Repository { get; private set; }

        public AnswerChoiceRepository(IRepository repository)
        {
            Repository = repository;
        }

        private IUnitOfWork CreateUnitOfWork()
        {
            return Repository.CreateUnitOfWork();
        }

        public virtual async Task<List<IAnswerChoice>> GetAsync(AnswerChoiceFilter filter = null)
        {
            try
            {
                List<IAnswerChoice> page = Mapper.Map<List<IAnswerChoice>>(
                            await Repository.WhereAsync<DALModel.AnswerChoice>()
                            .OrderBy(filter.SortOrder)
                            .Skip<DALModel.AnswerChoice>((filter.PageNumber - 1) * filter.PageSize)
                            .Take<DALModel.AnswerChoice>(filter.PageSize)
                            .ToListAsync<DALModel.AnswerChoice>()
                            );


                foreach (var choice in page)
                {
                    if (choice.HasPicture)
                    {
                        choice.AnswerChoicePictures = Mapper.Map<List<IAnswerChoicePicture>>(
                            await Repository.WhereAsync<DALModel.AnswerChoicePicture>()
                            .Where<DALModel.AnswerChoicePicture>(item => item.AnswerChoiceId == choice.Id)
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
                var result = Mapper.Map<ExamModel.AnswerChoice>(await Repository.SingleAsync<DALModel.AnswerChoice>(id));
                if (result.HasPicture)
                {
                    result.AnswerChoicePictures = Mapper.Map<List<IAnswerChoicePicture>>(
                            await Repository.WhereAsync<DALModel.AnswerChoicePicture>()
                            .Where<DALModel.AnswerChoicePicture>(item => item.AnswerChoiceId == result.Id)
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

        public virtual async Task<int> AddAsync(IAnswerChoice entity, IAnswerChoicePicture picture = null)
        {
            try
            {
                if (entity.HasPicture)
                {
                    if (picture != null)
                    {
                        IUnitOfWork unitOfWork = CreateUnitOfWork();

                        await unitOfWork.AddAsync<DALModel.AnswerChoice>(Mapper.Map<DALModel.AnswerChoice>(entity));
                        await unitOfWork.AddAsync<DALModel.AnswerChoicePicture>(Mapper.Map<DALModel.AnswerChoicePicture>(picture));

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
                        return await Repository.AddAsync<DALModel.AnswerChoice>(Mapper.Map<DALModel.AnswerChoice>(entity));
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

        public virtual async Task<int> UpdateAsync(IAnswerChoice entity, IAnswerChoicePicture picture = null)
        {
            try
            {
                if (entity.HasPicture)
                {
                    if (picture != null)
                    {
                        IUnitOfWork unitOfWork = CreateUnitOfWork();

                        await unitOfWork.UpdateAsync<DALModel.AnswerChoice>(Mapper.Map<DALModel.AnswerChoice>(entity));
                        await unitOfWork.UpdateAsync<DALModel.AnswerChoicePicture>(Mapper.Map<DALModel.AnswerChoicePicture>(picture));

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
                        return await Repository.UpdateAsync<DALModel.AnswerChoice>(Mapper.Map<DALModel.AnswerChoice>(entity));
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

        public virtual async Task<int> DeleteAsync(IAnswerChoice entity)
        {
            try
            {
                if (entity.HasPicture)
                {
                    IUnitOfWork unitOfWork = CreateUnitOfWork();
                    var pictures = Repository.WhereAsync<DALModel.AnswerChoicePicture>()
                        .Where(e => e.AnswerChoiceId == entity.Id);
              
                    foreach(var picture in pictures) 
                    {
                        await unitOfWork.DeleteAsync<DALModel.AnswerChoicePicture>(Mapper.Map<DALModel.AnswerChoicePicture>(picture));
                    }
                    await unitOfWork.DeleteAsync<DALModel.AnswerChoice>(Mapper.Map<DALModel.AnswerChoice>(entity));    

                    return await unitOfWork.CommitAsync();
                }
                else
                {
                    return await Repository.DeleteAsync<DALModel.AnswerChoice>(Mapper.Map<DALModel.AnswerChoice>(entity));
                }
            }
            catch (Exception e)
            {
                throw e;
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
                throw e;
            }
        }

        public virtual async Task<List<IAnswerChoice>> GetCorrectAnswersAsync(Guid questionId)
        {
            try
            {
                return Mapper.Map<List<IAnswerChoice>>(
                    await Repository.WhereAsync<DALModel.AnswerChoice>()
                    .Where<DALModel.AnswerChoice>(item => questionId == item.QuestionId && item.IsCorrect)
                    .ToListAsync<DALModel.AnswerChoice>()
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
                    await Repository.WhereAsync<DALModel.AnswerChoice>()
                    .Where<DALModel.AnswerChoice>(item => item.QuestionId == questionId)
                    .ToListAsync<DALModel.AnswerChoice>()
                    );

                foreach (var choice in choices)
                {
                    if (choice.HasPicture)
                    {
                        choice.AnswerChoicePictures = Mapper.Map<List<IAnswerChoicePicture>>(
                            await Repository.WhereAsync<DALModel.AnswerChoicePicture>()
                            .Where<DALModel.AnswerChoicePicture>(item => item.AnswerChoiceId == choice.Id)
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
    }
}
