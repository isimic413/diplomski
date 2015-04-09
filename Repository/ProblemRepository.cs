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
    public class ProblemRepository : IProblemRepository
    {
        protected IRepository Repository { get; set; }

        public ProblemRepository(IRepository repository)
        {
            Repository = repository;
        }

        protected IUnitOfWork CreateUnitOfWork()
        {
            return Repository.CreateUnitOfWork();
        }

        public virtual async Task<List<IProblem>> GetAsync(string sortOrder = "problemId", int pageNumber = 0, int pageSize = 50)
        {
            try
            {
                List<DALModel.Problem> page;
                pageSize = (pageSize > 50) ? 50 : pageSize;

                switch (sortOrder)
                { 
                    case "problemId":
                        page = await Repository.WhereAsync<DALModel.Problem>()
                            .OrderBy(item => item.Id)
                            .Skip<DALModel.Problem>((pageNumber - 1) * pageSize)
                            .Take<DALModel.Problem>(pageSize)
                            .ToListAsync<DALModel.Problem>();
                        break;
                    case "type":
                        page = await Repository.WhereAsync<DALModel.Problem>()
                            .OrderBy(item => item.ProblemTypeId)
                            .Skip<DALModel.Problem>((pageNumber - 1) * pageSize)
                            .Take<DALModel.Problem>(pageSize)
                            .ToListAsync<DALModel.Problem>();
                        break;
                    case "testingArea": 
                         page = await Repository.WhereAsync<DALModel.Problem>()
                             .OrderBy(item => item.TestingAreaId)
                             .Skip<DALModel.Problem>((pageNumber - 1) * pageSize)
                             .Take<DALModel.Problem>(pageSize)
                             .ToListAsync<DALModel.Problem>();
                        break;
                    default:
                        throw new ArgumentException("Invalid sortOrder.");
                }

                return new List<IProblem>(Mapper.Map<List<ExamModel.Problem>>(page));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<IProblem> GetAsync(Guid id)
        {
            try
            {
                var dalProblem = await Repository.SingleAsync<DALModel.Problem>(id);
                return Mapper.Map<ExamModel.Problem>(dalProblem);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }


        public virtual Task<int> AddAsync(IProblem entity, IProblemPicture picture = null,
            List<IAnswerChoice> choices = null, List<IAnswerChoicePicture> choicePictures = null);
        public virtual Task<int> UpdateAsync(IProblem entity);
        public virtual Task<int> DeleteAsync(IProblem entity);
        public virtual Task<int> DeleteAsync(Guid id);
    }
}
