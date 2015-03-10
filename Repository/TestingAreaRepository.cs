using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Repository;
using ExamPreparation.Repository.Common;
using ExamPreparation.Model.Common;

using DALModel = ExamPreparation.DAL.Models;
using ExamModel = ExamPreparation.Model;

namespace ExamPreparation.Repository
{
    public class TestingAreaRepository : ITestingAreaRepository
    {
        protected IRepository Repository { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }

        public TestingAreaRepository(IRepository repository)
        {
            Repository = repository;
        }

        public void CreateUnitOfWork()
        {
            UnitOfWork = Repository.CreateUnitOfWork();
        }

        public virtual Task<List<ITestingArea>> GetPageAsync(int pageSize=0, int pageNumber=0)
        {
            if (pageSize <= 0) return GetAllAsync();

            var dalPage = Repository.WhereAsync<DALModel.TestingArea>()
                .OrderBy(item => item.Abrv)
                .Skip<DALModel.TestingArea>((pageNumber - 1) * pageSize)
                .Take<DALModel.TestingArea>(pageSize)
                .ToListAsync<DALModel.TestingArea>()
                .Result;

            var testingAreas = Mapper.Map<List<DALModel.TestingArea>, List<ExamModel.TestingArea>>(dalPage);
            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.TestingArea>, List<ITestingArea>>(testingAreas));
        }

        public virtual Task<List<ITestingArea>> GetAllAsync()
        {
            var dalTestingAreas = Repository.WhereAsync<DALModel.TestingArea>().ToListAsync<DALModel.TestingArea>().Result;
            var testingAreas = Mapper.Map<List<DALModel.TestingArea>, List<ExamModel.TestingArea>>(dalTestingAreas);
            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.TestingArea>, List<ITestingArea>>(testingAreas));
        }

        public virtual Task<ITestingArea> GetByIdAsync(Guid id) 
        {
            var dalTestingArea = Repository.SingleAsync<DALModel.TestingArea>(id).Result;
            ITestingArea testingArea = Mapper.Map<DALModel.TestingArea, ExamModel.TestingArea>(dalTestingArea);
            return Task.Factory.StartNew(() => testingArea);
        }

        public virtual Task<int> AddAsync(ITestingArea entity)
        {
            try
            {
                var testingArea = Mapper.Map<ExamModel.TestingArea>(entity);
                var dalTestingArea = Mapper.Map<ExamModel.TestingArea, DALModel.TestingArea>(testingArea);
                return Repository.AddAsync<DALModel.TestingArea>(dalTestingArea);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            
        }

        public virtual Task<int> UpdateAsync(ITestingArea entity)
        {
            var testingArea = Mapper.Map<ITestingArea, ExamModel.TestingArea>(entity);
            var dalTestingArea = Mapper.Map<ExamModel.TestingArea, DALModel.TestingArea>(testingArea);
            try
            {
                return Repository.UpdateAsync<DALModel.TestingArea>(dalTestingArea);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual Task<int> DeleteAsync(ITestingArea entity)
        {
            var testingArea = Mapper.Map<ITestingArea, ExamModel.TestingArea>(entity);
            var dalTestingArea = Mapper.Map<ExamModel.TestingArea, DALModel.TestingArea>(testingArea);
            return Repository.DeleteAsync<DALModel.TestingArea>(dalTestingArea);
        }

        public virtual Task<int> DeleteAsync(Guid id)
        {
            return Repository.DeleteAsync<DALModel.TestingArea>(id);
        }

        public virtual void UnitOfWorkAdd(IUnitOfWork unitOfWork, ITestingArea entity)
        {
            var testingArea = Mapper.Map<ExamModel.TestingArea>(entity);
            var dalTestingArea = Mapper.Map<ExamModel.TestingArea, DALModel.TestingArea>(testingArea);
            unitOfWork.AddAsync<DALModel.TestingArea>(dalTestingArea);
        }
    }
}
