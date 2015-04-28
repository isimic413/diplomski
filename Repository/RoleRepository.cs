using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using DALModel = ExamPreparation.DAL.Models;
using ExamModel = ExamPreparation.Model;

namespace ExamPreparation.Repository
{
    public class RoleRepository: IRoleRepository
    {
        protected IRepository Repository { get; private set; }

        public RoleRepository(IRepository repository)
        {
            Repository = repository;
        }

        public virtual async Task<List<IRole>> GetAsync(RoleFilter filter)
        {
            try
            {
                return Mapper.Map<List<IRole>>(
                    await Repository.WhereAsync<DALModel.Role>()
                            .OrderBy(filter.SortOrder)
                            .Skip<DALModel.Role>((filter.PageNumber - 1) * filter.PageSize)
                            .Take<DALModel.Role>(filter.PageSize)
                            .ToListAsync<DALModel.Role>()
                    );
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<IRole> GetAsync(Guid id)
        {
            try
            {
                return Mapper.Map<ExamModel.Role>(await Repository.SingleAsync<DALModel.Role>(id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual Task<int> AddAsync(IRole entity)
        {
            try
            {
                return Repository.AddAsync<DALModel.Role>(Mapper.Map<DALModel.Role>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual Task<int> UpdateAsync(IRole entity)
        {
            try
            {
                return Repository.UpdateAsync<DALModel.Role>(Mapper.Map<DALModel.Role>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual Task<int> DeleteAsync(IRole entity)
        {
            try
            {
                return Repository.DeleteAsync<DALModel.Role>(Mapper.Map<DALModel.Role>(entity));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual Task<int> DeleteAsync(Guid id)
        {
            try
            {
                return Repository.DeleteAsync<DALModel.Role>(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
