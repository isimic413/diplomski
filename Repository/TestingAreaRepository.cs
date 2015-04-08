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

        public TestingAreaRepository(IRepository repository)
        {
            Repository = repository;
        }

        public virtual async Task<List<ITestingArea>> GetAsync(string sortOrder = "areaId", int pageNumber = 0, int pageSize = 50)
        {
            List<DALModel.TestingArea> page;
            pageSize = (pageSize > 50) ? 50 : pageSize;

            switch (sortOrder)
            {
                case "title":
                    page = await Repository.WhereAsync<DALModel.TestingArea>()
                        .OrderBy(item => item.Title)
                        .Skip<DALModel.TestingArea>((pageNumber - 1) * pageSize)
                        .Take<DALModel.TestingArea>(pageSize)
                        .ToListAsync<DALModel.TestingArea>();
                    break;

                case "abrv":
                    page = await Repository.WhereAsync<DALModel.TestingArea>()
                        .OrderBy(item => item.Abrv)
                        .Skip<DALModel.TestingArea>((pageNumber - 1) * pageSize)
                        .Take<DALModel.TestingArea>(pageSize)
                        .ToListAsync<DALModel.TestingArea>();
                    break;

                default: // case "areaId"
                    page = await Repository.WhereAsync<DALModel.TestingArea>()
                        .OrderBy(item => item.Id)
                        .Skip<DALModel.TestingArea>((pageNumber - 1) * pageSize)
                        .Take<DALModel.TestingArea>(pageSize)
                        .ToListAsync<DALModel.TestingArea>();
                    break;
            }

            return new List<ITestingArea>(Mapper.Map<List<ExamModel.TestingArea>>(page));
        }

        public virtual async Task<ITestingArea> GetAsync(Guid id) 
        {
            var dalTestingArea = await Repository.SingleAsync<DALModel.TestingArea>(id);
            return Mapper.Map<ExamModel.TestingArea>(dalTestingArea);
        }

        public virtual async Task<int> AddAsync(ITestingArea entity)
        {
            try
            {
                return await Repository.AddAsync<DALModel.TestingArea>(Mapper.Map<DALModel.TestingArea>(entity));
            }
            catch
            {
                return 0;
            }
        }

        public virtual async Task<int> UpdateAsync(ITestingArea entity)
        {
            try
            {
                return await Repository.UpdateAsync<DALModel.TestingArea>(Mapper.Map<DALModel.TestingArea>(entity));
            }
            catch
            {
                return 0;
            }
        }

        public virtual async Task<int> DeleteAsync(ITestingArea entity)
        {
            return await Repository.DeleteAsync<DALModel.TestingArea>(Mapper.Map<DALModel.TestingArea>(entity));
        }

        public virtual async Task<int> DeleteAsync(Guid id)
        {
            return await Repository.DeleteAsync<DALModel.TestingArea>(id);
        }
    }
}
