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
    public class AnswerChoiceRepository: IAnswerChoiceRepository
    {
        protected IRepository Repository { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }

        public AnswerChoiceRepository(IRepository repository)
        {
            Repository = repository;
        }

        public void CreateUnitOfWork()
        {
            UnitOfWork = Repository.CreateUnitOfWork();
        }

        public virtual Task<List<IAnswerChoice>> GetPageAsync(int pageSize = 0, int pageNumber = 0)
        {
            if (pageSize <= 0) return GetAllAsync();

            var dalPage = Repository.WhereAsync<DALModel.AnswerChoice>()
                .OrderBy(item => item.ProblemId)
                .Skip<DALModel.AnswerChoice>((pageNumber - 1) * pageSize)
                .Take<DALModel.AnswerChoice>(pageSize)
                .ToListAsync<DALModel.AnswerChoice>()
                .Result;

            var answerChoices = Mapper.Map<List<DALModel.AnswerChoice>, List<ExamModel.AnswerChoice>>(dalPage);
            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.AnswerChoice>, List<IAnswerChoice>>(answerChoices));
        }

        public virtual Task<List<IAnswerChoice>> GetAllAsync()
        {
            var dalAnswerChoices = Repository.WhereAsync<DALModel.AnswerChoice>().ToListAsync<DALModel.AnswerChoice>().Result;
            var answerChoices = Mapper.Map<List<DALModel.AnswerChoice>, List<ExamModel.AnswerChoice>>(dalAnswerChoices);
            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.AnswerChoice>, List<IAnswerChoice>>(answerChoices));
        }

        public virtual Task<IAnswerChoice> GetByIdAsync(Guid id)
        {
            var dalAnswerChoice = Repository.SingleAsync<DALModel.AnswerChoice>(id).Result;
            IAnswerChoice answerChoice = Mapper.Map<DALModel.AnswerChoice, ExamModel.AnswerChoice>(dalAnswerChoice);
            return Task.Factory.StartNew(() => answerChoice);
        }

        public virtual Task<int> AddAsync(IAnswerChoice entity)
        {
            try
            {
                var answerChoice = Mapper.Map<ExamModel.AnswerChoice>(entity);
                var dalAnswerChoice = Mapper.Map<ExamModel.AnswerChoice, DALModel.AnswerChoice>(answerChoice);
                return Repository.AddAsync<DALModel.AnswerChoice>(dalAnswerChoice);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }

        }

        public virtual Task<int> UpdateAsync(IAnswerChoice entity)
        {
            var answerChoice = Mapper.Map<IAnswerChoice, ExamModel.AnswerChoice>(entity);
            var dalAnswerChoice = Mapper.Map<ExamModel.AnswerChoice, DALModel.AnswerChoice>(answerChoice);
            try
            {
                return Repository.UpdateAsync<DALModel.AnswerChoice>(dalAnswerChoice);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual Task<int> DeleteAsync(IAnswerChoice entity)
        {
            var answerChoice = Mapper.Map<IAnswerChoice, ExamModel.AnswerChoice>(entity);
            var dalAnswerChoice = Mapper.Map<ExamModel.AnswerChoice, DALModel.AnswerChoice>(answerChoice);
            return Repository.DeleteAsync<DALModel.AnswerChoice>(dalAnswerChoice);
        }

        public virtual Task<int> DeleteAsync(Guid id)
        {
            return Repository.DeleteAsync<DALModel.AnswerChoice>(id);
        }

        public virtual Task<int> AddAsync(IUnitOfWork unitOfWork, IAnswerChoice entity)
        {
            var answerChoice = Mapper.Map<ExamModel.AnswerChoice>(entity);
            var dalAnswerChoice = Mapper.Map<ExamModel.AnswerChoice, DALModel.AnswerChoice>(answerChoice);
            return unitOfWork.AddAsync<DALModel.AnswerChoice>(dalAnswerChoice);
        }
    }
}
