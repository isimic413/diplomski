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
    public class AnswerChoicePictureRepository: IAnswerChoicePictureRepository
    {
        protected IRepository Repository { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }

        public AnswerChoicePictureRepository(IRepository repository)
        {
            Repository = repository;
        }

        public void CreateUnitOfWork()
        {
            UnitOfWork = Repository.CreateUnitOfWork();
        }

        public virtual Task<List<IAnswerChoicePicture>> GetPageAsync(int pageSize = 0, int pageNumber = 0)
        {
            if (pageSize <= 0) return GetAllAsync();

            var dalPage = Repository.WhereAsync<DALModel.AnswerChoicePicture>()
                .OrderBy(item => item.AnswerChoiceId)
                .Skip<DALModel.AnswerChoicePicture>((pageNumber - 1) * pageSize)
                .Take<DALModel.AnswerChoicePicture>(pageSize)
                .ToListAsync<DALModel.AnswerChoicePicture>()
                .Result;

            var answerChoicePictures = Mapper.Map<List<DALModel.AnswerChoicePicture>, List<ExamModel.AnswerChoicePicture>>(dalPage);
            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.AnswerChoicePicture>, List<IAnswerChoicePicture>>(answerChoicePictures));
        }

        public virtual Task<List<IAnswerChoicePicture>> GetAllAsync()
        {
            var dalAnswerChoicePictures = Repository.WhereAsync<DALModel.AnswerChoicePicture>().ToListAsync<DALModel.AnswerChoicePicture>().Result;
            var answerChoicePictures = Mapper.Map<List<DALModel.AnswerChoicePicture>, List<ExamModel.AnswerChoicePicture>>(dalAnswerChoicePictures);
            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.AnswerChoicePicture>, List<IAnswerChoicePicture>>(answerChoicePictures));
        }

        public virtual Task<IAnswerChoicePicture> GetByIdAsync(Guid id)
        {
            var dalAnswerChoicePicture = Repository.SingleAsync<DALModel.AnswerChoicePicture>(id).Result;
            IAnswerChoicePicture answerChoicePicture = Mapper.Map<DALModel.AnswerChoicePicture, ExamModel.AnswerChoicePicture>(dalAnswerChoicePicture);
            return Task.Factory.StartNew(() => answerChoicePicture);
        }

        public virtual Task<int> AddAsync(IAnswerChoicePicture entity)
        {
            try
            {
                var answerChoicePicture = Mapper.Map<ExamModel.AnswerChoicePicture>(entity);
                var dalAnswerChoicePicture = Mapper.Map<ExamModel.AnswerChoicePicture, DALModel.AnswerChoicePicture>(answerChoicePicture);
                return Repository.AddAsync<DALModel.AnswerChoicePicture>(dalAnswerChoicePicture);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }

        }

        public virtual Task<int> UpdateAsync(IAnswerChoicePicture entity)
        {
            var answerChoicePicture = Mapper.Map<IAnswerChoicePicture, ExamModel.AnswerChoicePicture>(entity);
            var dalAnswerChoicePicture = Mapper.Map<ExamModel.AnswerChoicePicture, DALModel.AnswerChoicePicture>(answerChoicePicture);
            try
            {
                return Repository.UpdateAsync<DALModel.AnswerChoicePicture>(dalAnswerChoicePicture);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual Task<int> DeleteAsync(IAnswerChoicePicture entity)
        {
            var answerChoicePicture = Mapper.Map<IAnswerChoicePicture, ExamModel.AnswerChoicePicture>(entity);
            var dalAnswerChoicePicture = Mapper.Map<ExamModel.AnswerChoicePicture, DALModel.AnswerChoicePicture>(answerChoicePicture);
            return Repository.DeleteAsync<DALModel.AnswerChoicePicture>(dalAnswerChoicePicture);
        }

        public virtual Task<int> DeleteAsync(Guid id)
        {
            return Repository.DeleteAsync<DALModel.AnswerChoicePicture>(id);
        }

        public virtual Task<int> AddAsync(IUnitOfWork unitOfWork, IAnswerChoicePicture entity)
        {
            var answerChoicePicture = Mapper.Map<ExamModel.AnswerChoicePicture>(entity);
            var dalAnswerChoicePicture = Mapper.Map<ExamModel.AnswerChoicePicture, DALModel.AnswerChoicePicture>(answerChoicePicture);
            return unitOfWork.AddAsync<DALModel.AnswerChoicePicture>(dalAnswerChoicePicture);
        }
    }
}
