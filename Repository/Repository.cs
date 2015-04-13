using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading.Tasks;

using ExamPreparation.DAL.Models;
using ExamPreparation.Repository.Common;

namespace ExamPreparation.Repository
{
    public class Repository : IRepository 
    {
        protected IExamPreparationContext DbContext { get; set; }
        protected IUnitOfWorkFactory UnitOfWorkFactory {get; set; }

        public Repository(IExamPreparationContext dbContext, IUnitOfWorkFactory unitOfWorkFactory)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("DbContext");
            }
            DbContext = dbContext;
            UnitOfWorkFactory = unitOfWorkFactory;
        }

        public IUnitOfWork CreateUnitOfWork() 
        {
            return UnitOfWorkFactory.CreateUnitOfWork();
        }

        public virtual IQueryable<T> WhereAsync<T>() where T : class
        {
            return DbContext.Set<T>().AsNoTracking();
        }

        public virtual async Task<T> SingleAsync<T>(Guid id) where T : class
        {
            return await DbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<int> AddAsync<T>(T entity) where T : class
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if(dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbContext.Set<T>().Add(entity);
            }

            try
            {
                return await DbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> UpdateAsync<T>(T entity) where T : class
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbContext.Set<T>().Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;

            try
            {
                return await DbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            
        }

        public virtual async Task<int> DeleteAsync<T>(T entity) where T : class
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbContext.Set<T>().Attach(entity);
                DbContext.Set<T>().Remove(entity);
            }

            try
            {
                return await DbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual async Task<int> DeleteAsync<T>(Guid id) where T : class
        {
            var entity = await SingleAsync<T>(id);
            if (entity == null)
            {
                throw new DllNotFoundException();
            }
            return await DeleteAsync<T>(entity);
        }
    }
}
