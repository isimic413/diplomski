using System;
using System.Collections.Generic;
using System.Linq;
using ExamPreparation.Repository;
using ExamPreparation.Repository.Common;
using ExamPreparation.Model.Common;
using ExamPreparation.Model.Mapping;

using DALModel = ExamPreparation.DAL.Models;
using ModelCommon = ExamPreparation.Model;

namespace ExamPreparation.Repository
{
    public class TestingAreaRepository : ITestingAreaRepository
    {
        protected IRepository Repository { get; set; }

        public TestingAreaRepository(IRepository repository)
        {
            Repository = repository;
        }

        public virtual IQueryable<ITestingArea> GetAll()
        {
            var dalTestingAreas = Repository.GetAll<DALModel.TestingArea>();
            var testingAreas = dalTestingAreas.ConvertToModelCommon();
            return testingAreas;
        }

        public virtual ITestingArea GetById(Guid id)
        {
            var dalTestingArea = Repository.GetById<DALModel.TestingArea>(id);
            ITestingArea testingArea = dalTestingArea.ConvertToModelCommon();
            return testingArea;

        }

        public virtual void Add(ITestingArea entity)
        {
            var testingArea = AutoMapper.Mapper.Map<ModelCommon.TestingArea>(entity);
            Repository.Add<DALModel.TestingArea>(testingArea.ConvertToDalModel());
        }

        public virtual void Update(ITestingArea entity)
        {

            var testingArea = AutoMapper.Mapper.Map<ModelCommon.TestingArea>(entity);
            Repository.Update<DALModel.TestingArea>(testingArea.ConvertToDalModel());
        }

        public virtual void Delete(ITestingArea entity)
        {
            var testingArea = AutoMapper.Mapper.Map<ModelCommon.TestingArea>(entity);
            Repository.Delete<DALModel.TestingArea>(testingArea.ConvertToDalModel());
        }

        public virtual void Delete(Guid id)
        {
            Repository.Delete<DALModel.TestingArea>(id);
        }
    }
}
