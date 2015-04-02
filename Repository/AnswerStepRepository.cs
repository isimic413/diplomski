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
    public class AnswerStepRepository: IAnswerStepRepository
    {
        protected IRepository Repository { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }

        public AnswerStepRepository(IRepository repository)
        {
            Repository = repository;
        }

        public void CreateUnitOfWork()
        {
            UnitOfWork = Repository.CreateUnitOfWork();
        }

        public virtual Task<List<IAnswerStep>> GetPageAsync(int pageSize = 0, int pageNumber = 0)
        {
            if (pageSize <= 0) return GetAllAsync();

            var dalPage = Repository.WhereAsync<DALModel.AnswerStep>()
                .OrderBy(item => item.ProblemId)
                .Skip<DALModel.AnswerStep>((pageNumber - 1) * pageSize)
                .Take<DALModel.AnswerStep>(pageSize)
                .ToListAsync<DALModel.AnswerStep>()
                .Result;

            var answerSteps = Mapper.Map<List<DALModel.AnswerStep>, List<ExamModel.AnswerStep>>(dalPage);
            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.AnswerStep>, List<IAnswerStep>>(answerSteps));
        }

        public virtual Task<List<IAnswerStep>> GetAllAsync()
        {
            var dalAnswerSteps = Repository.WhereAsync<DALModel.AnswerStep>().ToListAsync<DALModel.AnswerStep>().Result;
            var answerSteps = Mapper.Map<List<DALModel.AnswerStep>, List<ExamModel.AnswerStep>>(dalAnswerSteps);
            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.AnswerStep>, List<IAnswerStep>>(answerSteps));
        }

        public virtual Task<IAnswerStep> GetByIdAsync(Guid id)
        {
            var dalAnswerStep = Repository.SingleAsync<DALModel.AnswerStep>(id).Result;
            IAnswerStep answerStep = Mapper.Map<DALModel.AnswerStep, ExamModel.AnswerStep>(dalAnswerStep);
            return Task.Factory.StartNew(() => answerStep);
        }

        public virtual Task<int> AddAsync(IAnswerStep entity)
        {
            try
            {
                var answerStep = Mapper.Map<ExamModel.AnswerStep>(entity);
                var dalAnswerStep = Mapper.Map<ExamModel.AnswerStep, DALModel.AnswerStep>(answerStep);
                return Repository.AddAsync<DALModel.AnswerStep>(dalAnswerStep);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }

        }

        public virtual Task<int> UpdateAsync(IAnswerStep entity)
        {
            var answerStep = Mapper.Map<IAnswerStep, ExamModel.AnswerStep>(entity);
            var dalAnswerStep = Mapper.Map<ExamModel.AnswerStep, DALModel.AnswerStep>(answerStep);
            try
            {
                return Repository.UpdateAsync<DALModel.AnswerStep>(dalAnswerStep);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual Task<int> DeleteAsync(IAnswerStep entity)
        {
            var answerStep = Mapper.Map<IAnswerStep, ExamModel.AnswerStep>(entity);
            var dalAnswerStep = Mapper.Map<ExamModel.AnswerStep, DALModel.AnswerStep>(answerStep);
            return Repository.DeleteAsync<DALModel.AnswerStep>(dalAnswerStep);
        }

        public virtual Task<int> DeleteAsync(Guid id)
        {
            return Repository.DeleteAsync<DALModel.AnswerStep>(id);
        }

        public virtual Task<int> AddAsync(IUnitOfWork unitOfWork, IAnswerStep entity)
        {
            var answerStep = Mapper.Map<ExamModel.AnswerStep>(entity);
            var dalAnswerStep = Mapper.Map<ExamModel.AnswerStep, DALModel.AnswerStep>(answerStep);
            return unitOfWork.AddAsync<DALModel.AnswerStep>(dalAnswerStep);
        }
    }
}
