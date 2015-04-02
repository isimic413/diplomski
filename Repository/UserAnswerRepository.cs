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
    public class UserAnswerRepository: IUserAnswerRepository
    {
        protected IRepository Repository { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }

        public UserAnswerRepository(IRepository repository)
        {
            Repository = repository;
        }

        public void CreateUnitOfWork()
        {
            UnitOfWork = Repository.CreateUnitOfWork();
        }

        public virtual Task<List<IUserAnswer>> GetPageAsync(int pageSize = 0, int pageNumber = 0)
        {
            if (pageSize <= 0) return GetAllAsync();

            var dalPage = Repository.WhereAsync<DALModel.UserAnswer>()
                .OrderBy(item => item.UserId)
                .Skip<DALModel.UserAnswer>((pageNumber - 1) * pageSize)
                .Take<DALModel.UserAnswer>(pageSize)
                .ToListAsync<DALModel.UserAnswer>()
                .Result;

            var userAnswers = Mapper.Map<List<DALModel.UserAnswer>, List<ExamModel.UserAnswer>>(dalPage);
            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.UserAnswer>, List<IUserAnswer>>(userAnswers));
        }

        public virtual Task<List<IUserAnswer>> GetAllAsync()
        {
            var dalUserAnswers = Repository.WhereAsync<DALModel.UserAnswer>().ToListAsync<DALModel.UserAnswer>().Result;
            var userAnswers = Mapper.Map<List<DALModel.UserAnswer>, List<ExamModel.UserAnswer>>(dalUserAnswers);
            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.UserAnswer>, List<IUserAnswer>>(userAnswers));
        }

        public virtual Task<IUserAnswer> GetByIdAsync(Guid id)
        {
            var dalUserAnswer = Repository.SingleAsync<DALModel.UserAnswer>(id).Result;
            IUserAnswer userAnswer = Mapper.Map<DALModel.UserAnswer, ExamModel.UserAnswer>(dalUserAnswer);
            return Task.Factory.StartNew(() => userAnswer);
        }

        public virtual Task<int> AddAsync(IUserAnswer entity)
        {
            try
            {
                var userAnswer = Mapper.Map<ExamModel.UserAnswer>(entity);
                var dalUserAnswer = Mapper.Map<ExamModel.UserAnswer, DALModel.UserAnswer>(userAnswer);
                return Repository.AddAsync<DALModel.UserAnswer>(dalUserAnswer);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }

        }

        public virtual Task<int> UpdateAsync(IUserAnswer entity)
        {
            var userAnswer = Mapper.Map<IUserAnswer, ExamModel.UserAnswer>(entity);
            var dalUserAnswer = Mapper.Map<ExamModel.UserAnswer, DALModel.UserAnswer>(userAnswer);
            try
            {
                return Repository.UpdateAsync<DALModel.UserAnswer>(dalUserAnswer);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual Task<int> DeleteAsync(IUserAnswer entity)
        {
            var userAnswer = Mapper.Map<IUserAnswer, ExamModel.UserAnswer>(entity);
            var dalUserAnswer = Mapper.Map<ExamModel.UserAnswer, DALModel.UserAnswer>(userAnswer);
            return Repository.DeleteAsync <DALModel.UserAnswer>(dalUserAnswer);
        }

        public virtual Task<int> DeleteAsync(Guid id)
        {
            return Repository.DeleteAsync<DALModel.UserAnswer>(id);
        }

        public virtual Task<int> AddAsync(IUnitOfWork unitOfWork, IUserAnswer entity)
        {
            var userAnswer = Mapper.Map<ExamModel.UserAnswer>(entity);
            var dalUserAnswer = Mapper.Map<ExamModel.UserAnswer, DALModel.UserAnswer>(userAnswer);
            return unitOfWork.AddAsync<DALModel.UserAnswer>(dalUserAnswer);
        }
    }
}
