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
    public class AnswerChoiceService: IAnswerChoiceService
    {
        #region Properties

        protected IAnswerChoiceRepository Repository { get; private set; }
        protected IAnswerChoicePictureRepository PictureRepository { get; private set; }

        #endregion Properties

        #region Constructors

        public AnswerChoiceService(IAnswerChoiceRepository repository, IAnswerChoicePictureRepository pictureRepository)
        {
            Repository = repository;
            PictureRepository = pictureRepository;
        }

        #endregion Constructors

        #region Methods

        public Task<List<IAnswerChoice>> GetAsync(AnswerChoiceFilter filter = null)
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

        public Task<IAnswerChoice> GetAsync(Guid id)
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

        public Task<List<IAnswerChoice>> GetCorrectAnswersAsync(Guid questionId)
        {
            try
            {
                return Repository.GetCorrectAnswersAsync(questionId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<List<IAnswerChoice>> GetChoicesAsync(Guid questionId)
        {
            try
            {
                return Repository.GetChoicesAsync(questionId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> InsertAsync(IAnswerChoice entity, IAnswerChoicePicture picture = null)
        {
            try
            {
                if (picture != null)
                {
                    var unitOfWork = await Repository.CreateUnitOfWork();
                    await Repository.AddAsync(unitOfWork, entity);
                    await PictureRepository.AddAsync(unitOfWork, picture);
                    return await unitOfWork.CommitAsync();
                }
                else
                {
                    return await Repository.InsertAsync(entity);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> UpdateAsync(IAnswerChoice entity, IAnswerChoicePicture picture = null)
        {
            try
            {
                if (picture != null)
                {
                    var unitOfWork = await Repository.CreateUnitOfWork();
                    await Repository.UpdateAsync(unitOfWork, entity);
                    await PictureRepository.UpdateAsync(unitOfWork, picture);
                    return await unitOfWork.CommitAsync();
                }
                else
                {
                    return await Repository.UpdateAsync(entity);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<int> DeleteAsync(IAnswerChoice entity)
        {
            try
            {
                return Repository.DeleteAsync(entity);
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

        public Task<int> DeleteAsync(Guid id)
        {
            try
            {
                return Repository.DeleteAsync(id);
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

        #endregion Methods
    }
}
