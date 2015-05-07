using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.DAL.Models;
using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;

namespace ExamPreparation.Repository
{
    public class RoleRepository: IRoleRepository
    {
        #region Properties

        protected IRepository Repository { get; private set; }

        #endregion Properties

        #region Constructors

        public RoleRepository(IRepository repository)
        {
            Repository = repository;
        }

        #endregion Constructors

        #region Methods

        #region Get

        public virtual async Task<List<IRole>> GetAsync(RoleFilter filter)
        {
            try
            {
                return Mapper.Map<List<IRole>>(
                    await Repository.WhereAsync<Role>()
                            .OrderBy(filter.SortOrder)
                            .Skip<Role>((filter.PageNumber - 1) * filter.PageSize)
                            .Take<Role>(filter.PageSize)
                            .ToListAsync<Role>()
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
                return Mapper.Map<IRole>(await Repository.SingleAsync<Role>(id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Get

        #region Insert

        public virtual Task<int> InsertAsync(IRole entity)
        {
            try
            {
                return Repository.InsertAsync<Role>(Mapper.Map<Role>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Insert

        #region Update

        public virtual Task<int> UpdateAsync(IRole entity)
        {
            try
            {
                return Repository.UpdateAsync<Role>(Mapper.Map<Role>(entity));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Update

        #region Delete

        public virtual Task<int> DeleteAsync(IRole entity)
        {
            try
            {
                return Repository.DeleteAsync<Role>(Mapper.Map<Role>(entity));
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
                return Repository.DeleteAsync<Role>(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        #endregion Delete

        #endregion Methods
    }
}
