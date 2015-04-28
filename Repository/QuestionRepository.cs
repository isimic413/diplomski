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
    public class QuestionRepository : IQuestionRepository
    {
        protected IRepository Repository { get; private set; }

        public QuestionRepository(IRepository repository)
        {
            Repository = repository;
        }

        protected IUnitOfWork CreateUnitOfWork()
        {
            return Repository.CreateUnitOfWork();
        }

        public virtual async Task<List<IQuestion>> GetAsync(QuestionFilter filter)
        {
            try
            {
                List<IQuestion> page = Mapper.Map<List<IQuestion>>(
                    await Repository.WhereAsync<DALModel.Question>()
                             .OrderBy(filter.SortOrder)
                             .Skip<DALModel.Question>((filter.PageNumber - 1) * filter.PageSize)
                             .Take<DALModel.Question>(filter.PageSize)
                             .ToListAsync<DALModel.Question>()
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
                return Mapper.Map<ExamModel.Question>(await Repository.SingleAsync<DALModel.Question>(id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        protected async Task<int> AddChoices(IUnitOfWork unitOfWork, List<IAnswerChoice> choices = null, List<IAnswerChoicePicture> choicePictures = null)
        {
            try
            {
                if (choices == null)
                {
                    return 0;
                }

                if (choices.Count < choicePictures.Count)
                {
                    throw new ArgumentException("List<AnswerChoice> cannot have less elements than List<AnswerChoicePicture>.");
                }

                foreach (var choice in choices)
                {
                    await unitOfWork.AddAsync<DALModel.AnswerChoice>(Mapper.Map<DALModel.AnswerChoice>(choice));

                    var choicePicture = choicePictures.Find(c => choice.Id == c.AnswerChoiceId);
                    if (choice.HasPicture && choicePicture != null)
                    {
                        await unitOfWork.AddAsync<DALModel.AnswerChoicePicture>(Mapper.Map<DALModel.AnswerChoicePicture>(choicePicture));
                    }
                    else if (choice.HasPicture && choicePicture == null)
                    {
                        throw new ArgumentNullException("Picture needed for AnswerChoice with id=" + choice.Id + ".");
                    }
                    else if (!choice.HasPicture && choicePicture != null)
                    {
                        throw new ArgumentException("AnswerChoice with id=" + choice.Id + " should not have picture.");
                    }
                }
                return 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        protected async Task<int> UpdateChoices(IUnitOfWork unitOfWork, List<IAnswerChoice> choices = null, List<IAnswerChoicePicture> choicePictures = null)
        {
            try
            {
                if (choices == null)
                {
                    return 0;
                }

                if (choices.Count < choicePictures.Count)
                {
                    throw new ArgumentException("List<AnswerChoice> cannot have less elements than List<AnswerChoicePicture>.");
                }

                foreach (var choice in choices)
                {
                    await unitOfWork.UpdateAsync<DALModel.AnswerChoice>(Mapper.Map<DALModel.AnswerChoice>(choice));

                    var choicePicture = choicePictures.Find(c => choice.Id == c.AnswerChoiceId);
                    if (choice.HasPicture && choicePicture != null)
                    {
                        await unitOfWork.UpdateAsync<DALModel.AnswerChoicePicture>(Mapper.Map<DALModel.AnswerChoicePicture>(choicePicture));
                    }
                    else if (choice.HasPicture && choicePicture == null)
                    {
                        throw new ArgumentNullException("Picture needed for AnswerChoice with id=" + choice.Id + ".");
                    }
                    else if (!choice.HasPicture && choicePicture != null)
                    {
                        throw new ArgumentException("AnswerChoice with id=" + choice.Id + " should not have picture.");
                    }
                }
                return 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        protected async Task<int> AddSteps(IUnitOfWork unitOfWork, List<IAnswerStep> steps = null, List<IAnswerStepPicture> stepPictures = null)
        {
            try
            {
                if (steps == null)
                {
                    return 0;
                }

                if (steps.Count < stepPictures.Count)
                {
                    throw new ArgumentException("List<AnswerStep> cannot have less elements than List<AnswerStepPicture>.");
                }

                foreach (var step in steps)
                {
                    await unitOfWork.AddAsync<DALModel.AnswerChoice>(Mapper.Map<DALModel.AnswerChoice>(step));

                    var stepPicture = stepPictures.Find(c => step.Id == c.AnswerStepId);
                    if (step.HasPicture && stepPicture != null)
                    {
                        await unitOfWork.AddAsync<DALModel.AnswerChoicePicture>(Mapper.Map<DALModel.AnswerChoicePicture>(stepPicture));
                    }
                    else if (step.HasPicture && stepPicture == null)
                    {
                        throw new ArgumentNullException("Picture needed for AnswerStep with id=" + step.Id + ".");
                    }
                    else if (!step.HasPicture && stepPicture != null)
                    {
                        throw new ArgumentException("AnswerStep with id=" + step.Id + " should not have picture.");
                    }
                }
                return 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        protected async Task<int> UpdateSteps(IUnitOfWork unitOfWork, List<IAnswerStep> steps = null, List<IAnswerStepPicture> stepPictures = null)
        {
            try
            {
                if (steps == null)
                {
                    return 0;
                }

                if (steps.Count < stepPictures.Count)
                {
                    throw new ArgumentException("List<AnswerStep> cannot have less elements than List<AnswerStepPicture>.");
                }

                foreach (var step in steps)
                {
                    await unitOfWork.UpdateAsync<DALModel.AnswerChoice>(Mapper.Map<DALModel.AnswerChoice>(step));

                    var stepPicture = stepPictures.Find(c => step.Id == c.AnswerStepId);
                    if (step.HasPicture && stepPicture != null)
                    {
                        await unitOfWork.UpdateAsync<DALModel.AnswerChoicePicture>(Mapper.Map<DALModel.AnswerChoicePicture>(stepPicture));
                    }
                    else if (step.HasPicture && stepPicture == null)
                    {
                        throw new ArgumentNullException("Picture needed for AnswerStep with id=" + step.Id + ".");
                    }
                    else if (!step.HasPicture && stepPicture != null)
                    {
                        throw new ArgumentException("AnswerStep with id=" + step.Id + " should not have picture.");
                    }
                }
                return 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        protected int CheckProperties(IQuestion entity, IQuestionPicture picture = null,
            List<IAnswerChoice> choices = null, List<IAnswerChoicePicture> choicePictures = null,
            List<IAnswerStep> steps = null, List<IAnswerStepPicture> stepPictures = null) // async? - stavi Task<int>
        {
            try
            {
                if (choices == null && choicePictures != null)
                {
                    throw new ArgumentNullException("List of choices cannot be null if list of choicePictures is not null.");
                }
                if (steps == null && stepPictures != null)
                {
                    throw new ArgumentNullException("List of steps cannot be null if list of stepPictures is not null.");
                }
                if (entity.HasPicture && picture == null)
                {
                    throw new ArgumentNullException("Picture needed for Problem with id=" + entity.Id + ".");
                }
                if (!entity.HasPicture && picture != null)
                {
                    throw new ArgumentException("Problem with id=" + entity.Id + " should not have picture.");
                }
                if (!entity.HasSteps && steps != null)
                {
                    throw new ArgumentException("Problem with id=" + entity.Id + " should not have steps.");
                }
                if (entity.HasSteps && (steps == null || steps.Count < 1))
                {
                    throw new ArgumentNullException("Non empty List<AnswerStep> needed for problem with id=" + entity.Id + ".");
                }

                return 1;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<int> AddAsync(IQuestion entity, IQuestionPicture picture = null,
            List<IAnswerChoice> choices = null, List<IAnswerChoicePicture> choicePictures = null,
            List<IAnswerStep> steps = null, List<IAnswerStepPicture> stepPictures = null)
        {
            try
            {
                CheckProperties(entity, picture, choices, choicePictures, steps, stepPictures);

                if (picture == null && choices == null && steps == null)
                {
                    return await Repository.AddAsync<DALModel.Question>(Mapper.Map<DALModel.Question>(entity));
                }
                
                IUnitOfWork unitOfWork = CreateUnitOfWork();

                await unitOfWork.AddAsync<DALModel.Question>(Mapper.Map<DALModel.Question>(entity));

                if (entity.HasPicture)
                {
                    await unitOfWork.AddAsync<DALModel.QuestionPicture>(Mapper.Map<DALModel.QuestionPicture>(picture));
                }
                if (entity.HasSteps)
                {
                    await AddSteps(unitOfWork, steps, stepPictures);
                }
                if (choices != null && choices.Count > 0) // postaviti i uvjet: QuestionType!
                {
                    await AddChoices(unitOfWork, choices, choicePictures);
                }
                return await unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<int> UpdateAsync(IQuestion entity, IQuestionPicture picture = null,
            List<IAnswerChoice> choices = null, List<IAnswerChoicePicture> choicePictures = null,
            List<IAnswerStep> steps = null, List<IAnswerStepPicture> stepPictures = null)
        {
            try
            {
                CheckProperties(entity, picture, choices, choicePictures, steps, stepPictures);

                if (picture == null && choices == null && steps == null)
                {
                    return await Repository.UpdateAsync<DALModel.Question>(Mapper.Map<DALModel.Question>(entity));
                }

                IUnitOfWork unitOfWork = CreateUnitOfWork();

                await unitOfWork.UpdateAsync<DALModel.Question>(Mapper.Map<DALModel.Question>(entity));

                if (entity.HasPicture)
                {
                    await unitOfWork.UpdateAsync<DALModel.QuestionPicture>(Mapper.Map<DALModel.QuestionPicture>(picture));
                }
                if (entity.HasSteps)
                {
                    await UpdateSteps(unitOfWork, steps, stepPictures);
                }
                if (choices != null && choices.Count > 0) // postaviti i uvjet: QuestionType!
                {
                    await UpdateChoices(unitOfWork, choices, choicePictures);
                }
                return await unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> DeleteAsync(IQuestion entity)
        {
            try
            {
                IUnitOfWork unitOfWork = CreateUnitOfWork();

                if (entity.HasPicture)
                {
                    var picture = await Repository.WhereAsync<DALModel.QuestionPicture>()
                        .Where(p => entity.Id == p.QuestionId)
                        .SingleAsync();
                    await unitOfWork.DeleteAsync<DALModel.QuestionPicture>(picture);
                }

                if (entity.HasSteps) // paging?
                {
                    var steps = await Repository.WhereAsync<DALModel.AnswerStep>()
                        .Where(s => entity.Id == s.QuestionId)
                        .ToListAsync();

                    foreach (var step in steps)
                    {
                        if (step.HasPicture)
                        {
                            var stepPicture = await Repository.WhereAsync<DALModel.AnswerStepPicture>()
                                .Where(s => step.Id == s.AnswerStepId)
                                .SingleAsync();
                            await unitOfWork.DeleteAsync<DALModel.AnswerStepPicture>(stepPicture);
                        }
                        await unitOfWork.DeleteAsync<DALModel.AnswerStep>(step);
                    }
                }

                // ProblemType?
                var choices = await Repository.WhereAsync<DALModel.AnswerChoice>()
                    .Where(c => entity.Id == c.QuestionId)
                    .ToListAsync();

                if (choices != null)
                {
                    foreach (var choice in choices)
                    {
                        if (choice.HasPicture)
                        {
                            var stepPicture = await Repository.WhereAsync<DALModel.AnswerChoicePicture>()
                                .Where(s => choice.Id == s.AnswerChoiceId)
                                .SingleAsync();
                            await unitOfWork.DeleteAsync<DALModel.AnswerChoicePicture>(stepPicture);
                        }
                        await unitOfWork.DeleteAsync<DALModel.AnswerChoice>(choice);
                    }
                }

                await unitOfWork.DeleteAsync<DALModel.Question>(Mapper.Map<DALModel.Question>(entity));

                return await unitOfWork.CommitAsync();
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
                var problem = await Repository.SingleAsync<DALModel.Question>(id);
                return await DeleteAsync(Mapper.Map<ExamModel.Question>(problem));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }


        public async Task<List<IQuestion>> GetByTestingAreaIdAsync(Guid testingAreaId, QuestionFilter filter)
        {
            try
            {
                List<IQuestion> page = Mapper.Map<List<IQuestion>>(
                    await Repository.WhereAsync<DALModel.Question>()
                    .Where<DALModel.Question>(item => item.TestingAreaId == testingAreaId)
                    .OrderBy(filter.SortOrder)
                    .Skip<DALModel.Question>((filter.PageNumber - 1) * filter.PageSize)
                    .Take<DALModel.Question>(filter.PageSize)
                    .ToListAsync<DALModel.Question>()
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
                    await Repository.WhereAsync<DALModel.Question>()
                    .Where<DALModel.Question>(item => item.QuestionTypeId == typeId)
                    .OrderBy(filter.SortOrder)
                    .Skip<DALModel.Question>((filter.PageNumber - 1) * filter.PageSize)
                    .Take<DALModel.Question>(filter.PageSize)
                    .ToListAsync<DALModel.Question>()
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
                var pictures = await Repository.WhereAsync<DALModel.QuestionPicture>()
                        .Where<DALModel.QuestionPicture>(item => item.QuestionId == questionId)
                        .ToListAsync();

                return Mapper.Map<List<IQuestionPicture>>(pictures);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
