using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public ExamRepository(IRepository repository)
        {
            Repository = repository;
        }

        public virtual async Task<List<IExam>> GetAsync(string sortOrder = "yearAsc", int pageNumber = 0, int pageSize = 50)
        {
            try
            {
                List<DALModel.Exam> page;
                pageSize = (pageSize > 50) ? 50 : pageSize;

                switch (sortOrder)
                {
                    case "yearAsc":
                        page = await Repository.WhereAsync<DALModel.Exam>()
                            .OrderBy(item => item.Year)
                            .ThenBy(item => item.Month)
                            .ThenBy(item => item.TestingArea.Title)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();
                        break;
                    case "yearDesc":
                        page = await Repository.WhereAsync<DALModel.Exam>()
                            .OrderByDescending(item => item.Year)
                            .ThenBy(item => item.Month)
                            .ThenBy(item => item.TestingArea.Title)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();
                        break;
                    case "testingAreaAcs":
                        page = await Repository.WhereAsync<DALModel.Exam>()
                            .OrderBy(item => item.TestingArea.Title)
                            .ThenBy(item => item.Year)
                            .ThenBy(item => item.Month)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();
                        break;
                    case "testingAreaDesc":
                        page = await Repository.WhereAsync<DALModel.Exam>()
                            .OrderByDescending(item => item.TestingArea.Title)
                            .ThenBy(item => item.Year)
                            .ThenBy(item => item.Month)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();
                        break;
                    default:
                        throw new ArgumentException("Invalid sortOrder.");
                }

                return new List<IExam>(Mapper.Map<List<ExamModel.Exam>>(page));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<IExam> GetAsync(Guid id)
        {
            try
            {
                var dalExam = await Repository.SingleAsync<DALModel.Exam>(id);
                return Mapper.Map<ExamModel.Exam>(dalExam);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> AddAsync(IExam entity)
        {
            try
            {
                return await Repository.AddAsync<DALModel.Exam>(Mapper.Map<DALModel.Exam>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> UpdateAsync(IExam entity)
        {
            try
            {
                return await Repository.UpdateAsync<DALModel.Exam>(Mapper.Map<DALModel.Exam>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> DeleteAsync(IExam entity)
        {
            try
            {
                return await Repository.DeleteAsync<DALModel.Exam>(Mapper.Map<DALModel.Exam>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> DeleteAsync(Guid id)
        {
            try
            {
                return await Repository.DeleteAsync<DALModel.Exam>(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
