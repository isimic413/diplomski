using System;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Transactions;
using ExamPreparation.Repository.Common;
using ExamPreparation.DAL;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository
{
    public class Repository : IRepository // dodati i sinkrone metode kao alternative asinkronima?
    {
        protected IExamPreparationContext DbContext { get; set; }
        protected UnitOfWork UnitOfWork;

        public Repository(IExamPreparationContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("DbContext");
            }
            DbContext = dbContext;
        }

        //public void CreateUnitOfWork()
        //{
        //    using(TransactionScope scope = new TransactionScope())
        //    {
        //        UnitOfWork = new UnitOfWork(DbContext);
        //        scope.Complete();
        //    }
        //}

        public virtual IQueryable<T> GetAll<T>() where T: class
        {
            return DbContext.Set<T>();
        }

        public virtual T GetById<T>(Guid id) where T: class
        {
            return DbContext.Set<T>().Find(id);
        }

        public virtual async void Add<T>(T entity) where T : class
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
            await DbContext.SaveChangesAsync();
        }

        public virtual async void Update<T>(T entity) where T : class
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbContext.Set<T>().Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
        }

        public virtual async void Delete<T>(T entity) where T : class
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
            await DbContext.SaveChangesAsync();
        }

        public virtual async void Delete<T>(Guid id) where T : class
        {
            var entity = GetById<T>(id);
            if (entity == null)
            {
                return;
            }
            Delete<T>(entity);
            await DbContext.SaveChangesAsync();
        }
    }
}
