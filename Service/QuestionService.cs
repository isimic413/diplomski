using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using ExamPreparation.Service.Common;

namespace ExamPreparation.Service
{
    public class QuestionService : IQuestionService
    {
        #region Properties

        protected IQuestionRepository Repository { get; private set; }
        protected IAnswerStepRepository StepRepository { get; private set; }
        protected IQuestionPictureRepository PictureRepository { get; private set; }

        #endregion Properties

        #region Constructor

        public QuestionService(
            IQuestionRepository repository, 
            IAnswerStepRepository stepRepository,
            IQuestionPictureRepository pictureRepository)
        {
            Repository = repository;
            StepRepository = stepRepository;
            PictureRepository = pictureRepository;
        }

        #endregion Constructors

        #region Methods

        #region Get

        public Task<List<IQuestion>> GetAsync(QuestionFilter filter)
        {
            try
            {
                return Repository.GetAsync(filter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<IQuestion> GetAsync(Guid id)
        {
            try
            {
                return Repository.GetAsync(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<List<IQuestion>> GetByTestingAreaIdAsync(Guid testingAreaId, QuestionFilter filter)
        {
            try
            {
                return Repository.GetByTestingAreaIdAsync(testingAreaId, filter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<List<IQuestion>> GetByTypeIdAsync(Guid typeId, QuestionFilter filter)
        {
            try
            {
                return Repository.GetByTypeIdAsync(typeId, filter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Get

        #region Insert

        public async Task<int> InsertAsync(IQuestion entity, List<IAnswerChoice> choices,
            IQuestionPicture picture = null, List<IAnswerChoicePicture> choicePictures = null,
            List<IAnswerStep> steps = null, List<IAnswerStepPicture> stepPictures = null)
        {
            try
            {
                if (choices.Count < choicePictures.Count)
                {
                    throw new ArgumentException("List<IAnswerChoicePicture>");
                }
                if(steps.Count < stepPictures.Count)
                {
                    throw new ArgumentException("List<IAnswerStepPicture>");
                }

                var HasCorrectAnswers = (choices.FindAll(item => item.IsCorrect).Count > 1);
                if (!HasCorrectAnswers)
                {
                    throw new ArgumentException("There must be at least one correct answer in List<IAnswerChoice>.");
                }

                var result = await Repository.InsertAsync(entity, choices, picture, choicePictures);

                if(entity.HasSteps)
                {
                    IUnitOfWork unitOfWork = await Repository.CreateUnitOfWork();

                    var sortedSteps = steps.AsQueryable().OrderBy(item => item.StepNumber).ToArray();

                    for (int i = 0; i < sortedSteps.Length; i++)
                    {
                        if (sortedSteps[i].HasPicture)
                        {
                            var stepPicture = stepPictures.FindAll(item => item.AnswerStepId == sortedSteps[i].Id);
                            if (stepPicture.Count != 1)
                            {
                                throw new ArgumentException("List<IAnswerStepPicture>");
                            }

                            await StepRepository.AddAsync(unitOfWork, sortedSteps[i], stepPicture.First());
                        }
                    }

                    return await unitOfWork.CommitAsync();
                }

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Insert

        #region Update

        public async Task<int> UpdateAsync(IQuestion entity, IQuestionPicture picture = null,
            List<IAnswerStep> steps = null, List<IAnswerStepPicture> stepPictures = null)
        {
            try
            {
                if (steps != null && stepPictures != null)
                {
                    if (steps.Count < stepPictures.Count)
                    {
                        throw new ArgumentException("List<IAnswerStepPicture>");
                    }
                }
                if (steps == null && stepPictures != null)
                {
                    throw new ArgumentException("List<IAnswerStepPicture>");
                }
                if(entity.HasSteps && steps == null)
                {
                    throw new ArgumentNullException("List<IAnswerStep>");
                }

                IUnitOfWork unitOfWork = await Repository.CreateUnitOfWork();
                await Repository.UnitOfWorkUpdateAsync(unitOfWork, entity, picture);

                var updateSteps = (await Repository.GetAsync(entity.Id)).HasSteps;

                if (updateSteps) // entity in DB has steps
                {
                    if (!entity.HasSteps) // steps should be deleted
                    {
                        var dbSteps = await StepRepository.GetStepsAsync(entity.Id);
                        foreach (var step in dbSteps)
                        {
                            await StepRepository.UnitOfWorkDeleteAsync(unitOfWork, step);
                        }
                    }
                    else // update steps if needed
                    {
                        if (steps != null)
                        {
                            foreach (var step in steps)
                            {
                                await StepRepository.UnitOfWorkUpdateAsync(unitOfWork, step);
                            }
                        }
                    }
                }
                else // entity in DB does not have steps
                {
                    if (entity.HasSteps) // steps should be added in DB
                    {
                        foreach (var step in steps)
                        {
                            await StepRepository.AddAsync(unitOfWork, step);
                        }
                    }
                }

                return await unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<int> UpdatePictureAsync(IQuestionPicture picture)
        {
            try
            {
                return PictureRepository.UpdateAsync(picture);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Update

        #region Delete

        public Task<int> DeleteAsync(IQuestion entity)
        {
            try
            {
                return Repository.DeleteAsync(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<int> DeleteAsync(Guid id)
        {
            try
            {
                return Repository.DeleteAsync(id);
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
