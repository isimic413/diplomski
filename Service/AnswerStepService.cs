using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using ExamPreparation.Service.Common;

namespace ExamPreparation.Service
{
    public class AnswerStepService: IAnswerStepService
    {
        protected IAnswerStepRepository Repository { get; set; }

        public AnswerStepService(IAnswerStepRepository repository)
        {
            Repository = repository;
        }


        public async Task<List<IAnswerStep>> GetAsync(string sortOrder = "problemId", int pageNumber = 0, int pageSize = 50)
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

        public async Task<IAnswerStep> GetAsync(Guid id)
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

        public async Task<int> AddAsync(IAnswerStep entity, IAnswerStepPicture picture = null)
        {
            try
            {
                return await Repository.AddAsync(entity, picture);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<int> UpdateAsync(IAnswerStep entity, IAnswerStepPicture picture = null)
        {
            try
            {
                return await Repository.UpdateAsync(entity, picture);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<int> DeleteAsync(IAnswerStep entity)
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

        public async Task<List<IAnswerStep>> GetStepsAsync(Guid problemId)
        {
            try
            {
                List<IAnswerStep> result = await Repository.GetAsync();
                return result.Where(s => s.ProblemId == problemId).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
