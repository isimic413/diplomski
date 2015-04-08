﻿using AutoMapper;
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
    public class ProblemTypeRepository: IProblemTypeRepository
    {
        protected IRepository Repository { get; set; }

        public ProblemTypeRepository(IRepository repository)
        {
            Repository = repository;
        }

        public virtual async Task<List<IProblemType>> GetAsync(string sortOrder = "typeId", int pageNumber = 0, int pageSize = 50)
        {
            List<DALModel.ProblemType> page;
            pageSize = (pageSize > 50) ? 50 : pageSize;

            switch (sortOrder)
            {
                case "title":
                    page = await Repository.WhereAsync<DALModel.ProblemType>()
                        .OrderBy(item => item.Title)
                        .Skip<DALModel.ProblemType>((pageNumber - 1) * pageSize)
                        .Take<DALModel.ProblemType>(pageSize)
                        .ToListAsync<DALModel.ProblemType>();
                    break;

                case "abrv":
                    page = await Repository.WhereAsync<DALModel.ProblemType>()
                        .OrderBy(item => item.Abrv)
                        .Skip<DALModel.ProblemType>((pageNumber - 1) * pageSize)
                        .Take<DALModel.ProblemType>(pageSize)
                        .ToListAsync<DALModel.ProblemType>();
                    break;

                default: // case "typeId"
                    page = await Repository.WhereAsync<DALModel.ProblemType>()
                        .OrderBy(item => item.Id)
                        .Skip<DALModel.ProblemType>((pageNumber - 1) * pageSize)
                        .Take<DALModel.ProblemType>(pageSize)
                        .ToListAsync<DALModel.ProblemType>();
                    break;
            }

            return new List<IProblemType>(Mapper.Map<List<ExamModel.ProblemType>>(page));
        }

        public virtual async Task<IProblemType> GetAsync(Guid id)
        {
            var dalProblemType = await Repository.SingleAsync<DALModel.ProblemType>(id);
            return Mapper.Map<ExamModel.ProblemType>(dalProblemType);
        }

        public virtual async Task<int> AddAsync(IProblemType entity)
        {
            try
            {
                return await Repository.AddAsync<DALModel.ProblemType>(Mapper.Map<DALModel.ProblemType>(entity));
            }
            catch
            {
                return 0;
            }
        }

        public virtual async Task<int> UpdateAsync(IProblemType entity)
        {
            try
            {
                return await Repository.UpdateAsync<DALModel.ProblemType>(Mapper.Map<DALModel.ProblemType>(entity));
            }
            catch
            {
                return 0;
            }
        }

        public virtual async Task<int> DeleteAsync(IProblemType entity)
        {
            return await Repository.DeleteAsync<DALModel.ProblemType>(Mapper.Map<DALModel.ProblemType>(entity));
        }

        public virtual async Task<int> DeleteAsync(Guid id)
        {
            return await Repository.DeleteAsync<DALModel.ProblemType>(id);
        }
    }
}