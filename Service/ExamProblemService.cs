using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using ExamPreparation.Service.Common;

namespace ExamPreparation.Service
{
    public class ExamProblemService: IExamProblemService
    {
        protected IExamProblemRepository Repository { get; set; }

        public ExamProblemService(IExamProblemRepository repository)
        {
            Repository = repository;
        }


        public async Task<List<IExamProblem>> GetAsync(string sortOrder = "examProblemId", int pageNumber = 0, int pageSize = 50)
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

        public async Task<IExamProblem> GetAsync(Guid id)
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

        public async Task<int> AddAsync(IExamProblem entity)
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

        public async Task<int> UpdateAsync(IExamProblem entity)
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

        public async Task<int> DeleteAsync(IExamProblem entity)
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

        public async Task<List<IExamProblem>> GetByExamAsync(Guid examId)
        {
            try
            {
                List<IExamProblem> result = await Repository.GetAsync(sortOrder: "examId");
                return result.Where(p => p.ExamId == examId).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
