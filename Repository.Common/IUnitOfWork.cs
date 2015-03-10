using System;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPreparation.Repository.Common
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
        void DisposeAsync();

        void AddAsync<T>(T entity) where T : class;
        void UpdateAsync<T>(T entity) where T : class;
        void DeleteAsync<T>(T entity) where T : class;
        void DeleteAsync<T>(Guid id) where T : class;
    }
}
