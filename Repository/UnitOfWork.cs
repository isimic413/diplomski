using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

using ExamPreparation.DAL.Models;
using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;

namespace ExamPreparation.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        protected IExamPreparationContext DbContext { get; set; }

        public UnitOfWork(IExamPreparationContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("DbContext");
            }
            DbContext = dbContext;
        }

        public virtual async Task<int> AddAsync<T>(T entity) where T : class
        {
            try
            {
                DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
                if (dbEntityEntry.State != EntityState.Detached)
                {
                    dbEntityEntry.State = EntityState.Added;
                }
                else
                {
                    DbContext.Set<T>().Add(entity);
                }
                return 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            
        }

        public virtual async Task<int> UpdateAsync<T>(T entity) where T : class
        {
            try
            {
                DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
                if (dbEntityEntry.State == EntityState.Detached)
                {
                    DbContext.Set<T>().Attach(entity);
                }
                dbEntityEntry.State = EntityState.Modified;
                return 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            
        }

        public virtual async Task<int> DeleteAsync<T>(T entity) where T : class
        {
            try
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
                return 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            
        }

        public virtual async Task<int> DeleteAsync<T>(Guid id) where T : class
        {
            var entity = DbContext.Set<T>().Find(id);
            if (entity == null)
            {
                return 0;
            }
            return await DeleteAsync<T>(entity);
        }

        public async Task<int> CommitAsync()
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await DbContext.SaveChangesAsync();
                scope.Complete();
            }
            return result;
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
