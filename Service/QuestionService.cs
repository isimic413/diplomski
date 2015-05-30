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
        protected IQuestionPictureRepository PictureRepository { get; private set; }
        protected IAnswerStepRepository StepRepository { get; private set; }
        protected IAnswerChoiceRepository ChoiceRepository { get; private set; }

        #endregion Properties

        #region Constructor

        public QuestionService(
            IQuestionRepository repository, 
            IAnswerStepRepository stepRepository,
            IQuestionPictureRepository pictureRepository,
            IAnswerChoiceRepository choiceRepository)
        {
            Repository = repository;
            StepRepository = stepRepository;
            PictureRepository = pictureRepository;
            ChoiceRepository = choiceRepository;
        }

        #endregion Constructors

        #region Methods

        public Task<List<IQuestion>> GetAsync(QuestionFilter filter = null)
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

        public Task<List<IQuestion>> GetByTestingAreaIdAsync(Guid testingAreaId, QuestionFilter filter = null)
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

        public Task<List<IQuestion>> GetByTypeIdAsync(Guid typeId, QuestionFilter filter = null)
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

        public async Task<int> InsertAsync(IQuestion entity, List<IAnswerChoice> choices,
            IQuestionPicture picture = null, List<IAnswerChoicePicture> choicePictures = null,
            List<IAnswerStep> steps = null, List<IAnswerStepPicture> stepPictures = null)
        {
            try
            {
                if (entity.Points < 1)
                {
                    throw new ArgumentException("Points < 1");
                }
                if (steps == null && stepPictures != null)
                {
                    throw new ArgumentNullException("Null List<IAnswerStep>, not null List<IAnswerStepPicture>");
                }

                var unitOfWork = await Repository.CreateUnitOfWork();

                await Repository.AddAsync(unitOfWork, entity);
                await ChoiceRepository.AddAsync(unitOfWork, choices, choicePictures);

                if (picture != null)
                {
                    await PictureRepository.AddAsync(unitOfWork, picture);
                }

                if (steps != null)
                {
                    await StepRepository.AddAsync(unitOfWork, steps, stepPictures);
                }

                return await unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<int> UpdateAsync(IQuestion entity)
        {
            try
            {
                if (entity.Points < 1)
                {
                    throw new ArgumentException("Points < 1");
                }
                return Repository.UpdateAsync(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> DeleteAsync(IQuestion entity)
        {
            try
            {
                var unitOfWork = await Repository.CreateUnitOfWork();
                await ChoiceRepository.DeleteAsync(unitOfWork, entity.Id);
                await StepRepository.DeleteAsync(unitOfWork, entity.Id);
                await Repository.DeleteAsync(unitOfWork, entity);

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
                return await this.DeleteAsync(await this.GetAsync(id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Methods
    }
}
