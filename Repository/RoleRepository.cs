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
    public class RoleRepository: IRoleRepository
    {
        protected IRepository Repository { get; set; }

        public RoleRepository(IRepository repository)
        {
            Repository = repository;
        }

        public virtual async Task<List<IRole>> GetAsync(string sortOrder = "roleId", int pageNumber = 0, int pageSize = 50)
        {
            List<DALModel.Role> page;
            pageSize = (pageSize > 50) ? 50 : pageSize;

            switch (sortOrder)
            {
                case "title":
                    page = await Repository.WhereAsync<DALModel.Role>()
                        .OrderBy(item => item.Title)
                        .Skip<DALModel.Role>((pageNumber - 1) * pageSize)
                        .Take<DALModel.Role>(pageSize)
                        .ToListAsync<DALModel.Role>();
                    break;

                case "abrv":
                    page = await Repository.WhereAsync<DALModel.Role>()
                        .OrderBy(item => item.Abrv)
                        .Skip<DALModel.Role>((pageNumber - 1) * pageSize)
                        .Take<DALModel.Role>(pageSize)
                        .ToListAsync<DALModel.Role>();
                    break;

                default: // case "roleId"
                    page = await Repository.WhereAsync<DALModel.Role>()
                        .OrderBy(item => item.Id)
                        .Skip<DALModel.Role>((pageNumber - 1) * pageSize)
                        .Take<DALModel.Role>(pageSize)
                        .ToListAsync<DALModel.Role>();
                    break;
            }

            return new List<IRole>(Mapper.Map<List<ExamModel.Role>>(page));
        }

        public virtual async Task<IRole> GetAsync(Guid id)
        {
            var dalProblemType = await Repository.SingleAsync<DALModel.Role>(id);
            return Mapper.Map<ExamModel.Role>(dalProblemType);
        }

        public virtual async Task<int> AddAsync(IRole entity)
        {
            try
            {
                return await Repository.AddAsync<DALModel.Role>(Mapper.Map<DALModel.Role>(entity));
            }
            catch
            {
                return 0;
            }
        }

        public virtual async Task<int> UpdateAsync(IRole entity)
        {
            try
            {
                return await Repository.UpdateAsync<DALModel.Role>(Mapper.Map<DALModel.Role>(entity));
            }
            catch
            {
                return 0;
            }
        }

        public virtual async Task<int> DeleteAsync(IRole entity)
        {
            return await Repository.DeleteAsync<DALModel.Role>(Mapper.Map<DALModel.Role>(entity));
        }

        public virtual async Task<int> DeleteAsync(Guid id)
        {
            return await Repository.DeleteAsync<DALModel.Role>(id);
        }
    }
}
