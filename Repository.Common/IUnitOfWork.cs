using System;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPreparation.Repository.Common
{
    public interface IUnitOfWork: IDisposable
    {
        Task<int> CommitAsync();
        void Dispose();

        Task<int> AddAsync<T>(T entity) where T : class;
        Task<int> UpdateAsync<T>(T entity) where T : class;
        Task<int> DeleteAsync<T>(T entity) where T : class;
        Task<int> DeleteAsync<T>(Guid id) where T : class;
    }
}
