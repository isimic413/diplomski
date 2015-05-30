using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using ExamPreparation.Service.Common;

namespace ExamPreparation.Service
{
    public class AnswerStepService: IAnswerStepService
    {
        #region Properties

        protected IAnswerStepRepository Repository { get; private set; }
        protected IAnswerStepPictureRepository PictureRepository { get; private set; }

        #endregion Properties

        #region Constructors

        public AnswerStepService(IAnswerStepRepository repository, IAnswerStepPictureRepository pictureRepository)
        {
            Repository = repository;
            PictureRepository = pictureRepository;
        }

        #endregion Constructors

        #region Methods

        public Task<List<IAnswerStep>> GetAsync(AnswerStepFilter filter = null)
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

        public Task<IAnswerStep> GetAsync(Guid id)
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

        public Task<List<IAnswerStep>> GetStepsAsync(Guid questionId)
        {
            try
            {
                return Repository.GetStepsAsync(questionId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> InsertAsync(IAnswerStep entity, IAnswerStepPicture picture = null)
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

        public async Task<int> UpdateAsync(IAnswerStep entity, IAnswerStepPicture picture = null)
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

        public Task<int> DeleteAsync(IAnswerStep entity)
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

        #endregion Methods
    }
}
