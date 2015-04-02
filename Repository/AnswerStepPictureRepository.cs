using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using DALModel = ExamPreparation.DAL.Models;
using ExamModel = ExamPreparation.Model;

namespace ExamPreparation.Repository
{
    public class AnswerStepPictureRepository: IAnswerStepPictureRepository
    {
        protected IRepository Repository { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }

        public AnswerStepPictureRepository(IRepository repository)
        {
            Repository = repository;
        }

        public void CreateUnitOfWork()
        {
            UnitOfWork = Repository.CreateUnitOfWork();
        }

        public virtual Task<List<IAnswerStepPicture>> GetPageAsync(int pageSize = 0, int pageNumber = 0)
        {
            if (pageSize <= 0) return GetAllAsync();

            var dalPage = Repository.WhereAsync<DALModel.AnswerStepPicture>()
                .OrderBy(item => item.AnswerStepId)
                .Skip<DALModel.AnswerStepPicture>((pageNumber - 1) * pageSize)
                .Take<DALModel.AnswerStepPicture>(pageSize)
                .ToListAsync<DALModel.AnswerStepPicture>()
                .Result;

            var answerStepPictures = Mapper.Map<List<DALModel.AnswerStepPicture>, List<ExamModel.AnswerStepPicture>>(dalPage);
            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.AnswerStepPicture>, List<IAnswerStepPicture>>(answerStepPictures));
        }

        public virtual Task<List<IAnswerStepPicture>> GetAllAsync()
        {
            var dalAnswerStepPictures = Repository.WhereAsync<DALModel.AnswerStepPicture>().ToListAsync<DALModel.AnswerStepPicture>().Result;
            var answerStepPictures = Mapper.Map<List<DALModel.AnswerStepPicture>, List<ExamModel.AnswerStepPicture>>(dalAnswerStepPictures);
            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.AnswerStepPicture>, List<IAnswerStepPicture>>(answerStepPictures));
        }

        public virtual Task<IAnswerStepPicture> GetByIdAsync(Guid id)
        {
            var dalAnswerStepPicture = Repository.SingleAsync<DALModel.AnswerStepPicture>(id).Result;
            IAnswerStepPicture answerStepPicture = Mapper.Map<DALModel.AnswerStepPicture, ExamModel.AnswerStepPicture>(dalAnswerStepPicture);
            return Task.Factory.StartNew(() => answerStepPicture);
        }

        public virtual Task<int> AddAsync(IAnswerStepPicture entity)
        {
            try
            {
                var answerStepPicture = Mapper.Map<ExamModel.AnswerStepPicture>(entity);
                var dalAnswerStepPicture = Mapper.Map<ExamModel.AnswerStepPicture, DALModel.AnswerStepPicture>(answerStepPicture);
                return Repository.AddAsync<DALModel.AnswerStepPicture>(dalAnswerStepPicture);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }

        }

        public virtual Task<int> UpdateAsync(IAnswerStepPicture entity)
        {
            var answerStepPicture = Mapper.Map<IAnswerStepPicture, ExamModel.AnswerStepPicture>(entity);
            var dalAnswerStepPicture = Mapper.Map<ExamModel.AnswerStepPicture, DALModel.AnswerStepPicture>(answerStepPicture);
            try
            {
                return Repository.UpdateAsync<DALModel.AnswerStepPicture>(dalAnswerStepPicture);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual Task<int> DeleteAsync(IAnswerStepPicture entity)
        {
            var answerStepPicture = Mapper.Map<IAnswerStepPicture, ExamModel.AnswerStepPicture>(entity);
            var dalAnswerStepPicture = Mapper.Map<ExamModel.AnswerStepPicture, DALModel.AnswerStepPicture>(answerStepPicture);
            return Repository.DeleteAsync<DALModel.AnswerStepPicture>(dalAnswerStepPicture);
        }

        public virtual Task<int> DeleteAsync(Guid id)
        {
            return Repository.DeleteAsync<DALModel.AnswerStepPicture>(id);
        }

        public virtual Task<int> AddAsync(IUnitOfWork unitOfWork, IAnswerStepPicture entity)
        {
            var answerStepPicture = Mapper.Map<ExamModel.AnswerStepPicture>(entity);
            var dalAnswerStepPicture = Mapper.Map<ExamModel.AnswerStepPicture, DALModel.AnswerStepPicture>(answerStepPicture);
            return unitOfWork.AddAsync<DALModel.AnswerStepPicture>(dalAnswerStepPicture);
        }
    }
}
