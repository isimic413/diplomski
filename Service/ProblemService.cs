using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using ExamPreparation.Service.Common;

namespace ExamPreparation.Service
{
    public class ProblemService : IProblemService
    {
        protected IProblemRepository Repository { get; set; }

        public ProblemService(IProblemRepository repository)
        {
            Repository = repository;
        }

        public async Task<List<IProblem>> GetAsync(string sortOrder = "problemId", int pageNumber = 0, int pageSize = 50)
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

        public async Task<IProblem> GetAsync(Guid id)
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

        public async Task<int> AddAsync(IProblem entity, IProblemPicture picture = null,
            List<IAnswerChoice> choices = null, List<IAnswerChoicePicture> choicePictures = null,
            List<IAnswerStep> steps = null, List<IAnswerStepPicture> stepPictures = null)
        {
            try
            {
                return await Repository.AddAsync(entity, picture, choices, choicePictures, steps, stepPictures);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<int> UpdateAsync(IProblem entity, IProblemPicture picture = null,
            List<IAnswerChoice> choices = null, List<IAnswerChoicePicture> choicePictures = null,
            List<IAnswerStep> steps = null, List<IAnswerStepPicture> stepPictures = null)
        {
            try
            {
                return await Repository.UpdateAsync(entity, picture, choices, choicePictures, steps, stepPictures);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<int> DeleteAsync(IProblem entity)
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

        public /*async*/ Task<int> GetByExam(Guid examId)
        {
            throw new Exception("Not implemented.");
        }

        public async Task<List<IProblem>> GetByTestingArea(Guid testingAreaId)
        {
            try
            {
                List<IProblem> problems = await GetAsync(sortOrder:"testingArea");
                return problems.Where(p => testingAreaId == p.TestingAreaId).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
