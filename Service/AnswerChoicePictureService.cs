using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using ExamPreparation.Service.Common;

namespace ExamPreparation.Service
{
    public class AnswerChoicePictureService: IAnswerChoicePictureService
    {
        #region Properties

        protected IAnswerChoicePictureRepository Repository { get; private set; }

        #endregion Properties

        #region Constructors

        public AnswerChoicePictureService(IAnswerChoicePictureRepository repository)
        {
            Repository = repository;
        }

        #endregion Constructors

        #region Methods

        public Task<IAnswerChoicePicture> GetAsync(Guid id)
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

        public Task<int> UpdateAsync(IAnswerChoicePicture entity)
        {
            try
            {
                return Repository.UpdateAsync(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Methods
    }
}
