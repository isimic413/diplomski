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
    public class ExamProblemRepository: IExamProblemRepository
    {
        protected IRepository Repository { get; set; }

        public ExamProblemRepository(IRepository repository)
        {
            Repository = repository;
        }

        public virtual async Task<List<IExamProblem>> GetAsync(string sortOrder = "examId", int pageNumber = 0, int pageSize = 50)
        {
            try
            {
                List<DALModel.ExamProblem> page;
                pageSize = (pageSize > 50) ? 50 : pageSize;


                switch (sortOrder)
                {
                    case "problemId":
                        page = await Repository.WhereAsync<DALModel.ExamProblem>()
                            .OrderBy(item => item.ProblemId)
                            .Skip<DALModel.ExamProblem>((pageNumber - 1) * pageSize)
                            .Take<DALModel.ExamProblem>(pageSize)
                            .ToListAsync<DALModel.ExamProblem>();
                        break;
                    case "examId":
                        page = await Repository.WhereAsync<DALModel.ExamProblem>()
                            .OrderBy(item => item.ExamId)
                            .Skip<DALModel.ExamProblem>((pageNumber - 1) * pageSize)
                            .Take<DALModel.ExamProblem>(pageSize)
                            .ToListAsync<DALModel.ExamProblem>();
                        break;
                    case "examProblemId":
                        page = await Repository.WhereAsync<DALModel.ExamProblem>()
                            .OrderBy(item => item.Id)
                            .Skip<DALModel.ExamProblem>((pageNumber - 1) * pageSize)
                            .Take<DALModel.ExamProblem>(pageSize)
                            .ToListAsync<DALModel.ExamProblem>();
                        break;
                    default:
                        throw new ArgumentException("Invalid sortOrder.");
                }

                return new List<IExamProblem>(Mapper.Map<List<ExamModel.ExamProblem>>(page));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<IExamProblem> GetAsync(Guid id)
        {
            try
            {
                var dalExamProblem = await Repository.SingleAsync<DALModel.ExamProblem>(id);
                return Mapper.Map<ExamModel.ExamProblem>(dalExamProblem);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> AddAsync(IExamProblem entity)
        {
            try
            {
                return await Repository.AddAsync<DALModel.ExamProblem>(Mapper.Map<DALModel.ExamProblem>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> UpdateAsync(IExamProblem entity)
        {
            try
            {
                return await Repository.UpdateAsync<DALModel.ExamProblem>(Mapper.Map<DALModel.ExamProblem>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> DeleteAsync(IExamProblem entity)
        {
            try
            {
                return await Repository.DeleteAsync<DALModel.ExamProblem>(Mapper.Map<DALModel.ExamProblem>(entity));
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
                return await Repository.DeleteAsync<DALModel.ExamProblem>(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
