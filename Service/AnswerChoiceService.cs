using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using ExamPreparation.Service.Common;

namespace ExamPreparation.Service
{
    public class AnswerChoiceService: IAnswerChoiceService
    {
        protected IAnswerChoiceRepository Repository { get; set; }

        public AnswerChoiceService(IAnswerChoiceRepository repository)
        {
            Repository = repository;
        }

        public async Task<List<IAnswerChoice>> GetAsync(string sortOrder = "problemId", int pageNumber = 0, int pageSize = 50)
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

        public async Task<IAnswerChoice> GetAsync(Guid id)
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

        public async Task<int> AddAsync(IAnswerChoice entity, IAnswerChoicePicture picture = null)
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

        public async Task<int> UpdateAsync(IAnswerChoice entity, IAnswerChoicePicture picture = null)
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

        public async Task<int> DeleteAsync(IAnswerChoice entity)
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

        public async Task<IAnswerChoice> GetCorrectAnswerAsync(Guid problemId)
        {
            try
            {
                List<IAnswerChoice> choices = await GetChoicesAsync(problemId);
                return choices.Find(c => c.IsCorrect);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
        public async Task<List<IAnswerChoice>> GetChoicesAsync(Guid problemId)
        {
            try
            {
                List<IAnswerChoice> choices = await GetAsync(sortOrder:"problemId");
                return choices.Where(c => problemId == c.ProblemId).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            } 
        }
    }
}
