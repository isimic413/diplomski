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

        public virtual Task<int> AddAsync<T>(T entity) where T : class
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
                return Task<int>.Factory.StartNew(() =>
                {
                    return 1;
                });
            }
            catch (Exception e)
            {
                return Task<int>.Factory.StartNew(() =>
                {
                    return 0;
                });
            }
            
        }

        public virtual Task<int> UpdateAsync<T>(T entity) where T : class
        {
            try
            {
                DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
                if (dbEntityEntry.State == EntityState.Detached)
                {
                    DbContext.Set<T>().Attach(entity);
                }
                dbEntityEntry.State = EntityState.Modified;
                return Task<int>.Factory.StartNew(() =>
                {
                    return 1;
                });
            }
            catch (Exception e)
            {
                return Task<int>.Factory.StartNew(() =>
                {
                    return 0;
                });
            }
            
        }

        public virtual Task<int> DeleteAsync<T>(T entity) where T : class
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
                return Task<int>.Factory.StartNew(() =>
                {
                    return 1;
                });
            }
            catch (Exception e)
            {
                return Task<int>.Factory.StartNew(() =>
                {
                    return 0;
                });
            }
            
        }

        public virtual Task<int> DeleteAsync<T>(Guid id) where T : class
        {
            var entity = DbContext.Set<T>().Find(id);
            if (entity == null)
            {
                return Task<int>.Factory.StartNew(() =>
                {
                    return 0;
                });
            }
            return DeleteAsync<T>(entity);
        }

        public Task<int> CommitAsync()
        {
            return DbContext.SaveChangesAsync();
        }

        public void DisposeAsync()
        {
            DbContext.Dispose();
        }
    }
}
