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
    public class ExamRepository: IExamRepository
    {
        protected IRepository Repository { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }

        public ExamRepository(IRepository repository)
        {
            Repository = repository;
        }

        public void CreateUnitOfWork()
        {
            UnitOfWork = Repository.CreateUnitOfWork();
        }

        public virtual Task<List<IExam>> GetPageAsync(int pageSize = 0, int pageNumber = 0)
        {
            if (pageSize <= 0) return GetAllAsync();

            var dalPage = Repository.WhereAsync<DALModel.Exam>()
                .OrderBy(item => item.Year)
                .Skip<DALModel.Exam>((pageNumber - 1) * pageSize)
                .Take<DALModel.Exam>(pageSize)
                .ToListAsync<DALModel.Exam>()
                .Result;

            var exams = Mapper.Map<List<DALModel.Exam>, List<ExamModel.Exam>>(dalPage);
            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.Exam>, List<IExam>>(exams));
        }

        public virtual Task<List<IExam>> GetAllAsync()
        {
            var dalExams = Repository.WhereAsync<DALModel.Exam>().ToListAsync<DALModel.Exam>().Result;
            var exams = Mapper.Map<List<DALModel.Exam>, List<ExamModel.Exam>>(dalExams);
            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.Exam>, List<IExam>>(exams));
        }

        public virtual Task<IExam> GetByIdAsync(Guid id)
        {
            var dalExam = Repository.SingleAsync<DALModel.Exam>(id).Result;
            IExam exam = Mapper.Map<DALModel.Exam, ExamModel.Exam>(dalExam);
            return Task.Factory.StartNew(() => exam);
        }

        public virtual Task<int> AddAsync(IExam entity)
        {
            try
            {
                var exam = Mapper.Map<ExamModel.Exam>(entity);
                var dalExam = Mapper.Map<ExamModel.Exam, DALModel.Exam>(exam);
                return Repository.AddAsync<DALModel.Exam>(dalExam);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }

        }

        public virtual Task<int> UpdateAsync(IExam entity)
        {
            var exam = Mapper.Map<IExam, ExamModel.Exam>(entity);
            var dalExam = Mapper.Map<ExamModel.Exam, DALModel.Exam>(exam);
            try
            {
                return Repository.UpdateAsync<DALModel.Exam>(dalExam);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual Task<int> DeleteAsync(IExam entity)
        {
            var exam = Mapper.Map<IExam, ExamModel.Exam>(entity);
            var dalExam = Mapper.Map<ExamModel.Exam, DALModel.Exam>(exam);
            return Repository.DeleteAsync<DALModel.Exam>(dalExam);
        }

        public virtual Task<int> DeleteAsync(Guid id)
        {
            return Repository.DeleteAsync<DALModel.Exam>(id);
        }

        public virtual Task<int> AddAsync(IUnitOfWork unitOfWork, IExam entity)
        {
            var exam = Mapper.Map<ExamModel.Exam>(entity);
            var dalExam = Mapper.Map<ExamModel.Exam, DALModel.Exam>(exam);
            return unitOfWork.AddAsync<DALModel.Exam>(dalExam);
        }
    }
}
