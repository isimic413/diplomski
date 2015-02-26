using System;
using System.Collections.Generic;
using System.Linq;

using ExamPreparation.DAL;
using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

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

        public virtual IQueryable<T> GetAll<T>() where T: class
        {
            return DbContext.Set<T>();
        }

        public virtual T GetById<T>(Guid id) where T: class
        {
            return DbContext.Set<T>().Find(id);
        }

        public virtual void Add<T>(T entity) where T : class
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
        }

        public virtual void Update<T>(T entity) where T : class
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbContext.Set<T>().Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete<T>(T entity) where T : class
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
        }

        public virtual void Delete<T>(Guid id) where T : class
        {
            var entity = GetById<T>(id);
            if (entity == null)
            {
                return;
            }
            Delete<T>(entity);
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
