using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using ExamPreparation.Service.Common;

namespace ExamPreparation.Service
{
    public class ExamService: IExamService
    {
        protected IExamRepository Repository { get; set; }

        public ExamService(IExamRepository repository)
        {
            Repository = repository;
        }

        public async Task<List<IExam>> GetAsync(string sortOrder = "yearAsc", int pageNumber = 0, int pageSize = 50)
        {
            try
            {
                return await Repository.GetAsync(sortOrder, pageNumber, pageSize);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<IExam> GetAsync(Guid id)
        {
            try
            {
                return await Repository.GetAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<int> AddAsync(IExam entity)
        {
            try
            {
                return await Repository.AddAsync(entity);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<int> UpdateAsync(IExam entity)
        {
            try
            {
                return await Repository.UpdateAsync(entity);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<int> DeleteAsync(IExam entity)
        {
            try
            {
                return await Repository.DeleteAsync(entity);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            try
            {
                return await Repository.DeleteAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<List<IExam>> GetByYear(int year)
        {
            try
            {
                List<IExam> exams = await GetAsync(sortOrder: "yearAsc");
                return exams.Where(c => year == c.Year).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<List<IExam>> GetByTestingArea(Guid testingAreaId)
        {
            try
            {
                List<IExam> exams = await GetAsync(sortOrder: "testingAreaAsc");
                return exams.Where(c => testingAreaId == c.TestingAreaId).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
