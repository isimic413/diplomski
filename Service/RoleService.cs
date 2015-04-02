using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using ExamPreparation.Service.Common;

namespace ExamPreparation.Service
{
    public class RoleService: IRoleService
    {
        protected IRoleRepository Repository { get; set; }
        protected IUnitOfWork UnitOfWork;

        public RoleService(IRoleRepository repository)
        {
            Repository = repository;
        }

        public Task<List<IRole>> GetPageAsync(int pageSize, int pageNumber)
        {
            return Repository.GetPageAsync(pageSize, pageNumber);
        }

        public Task<List<IRole>> GetAllAsync()
        {
            return Repository.GetAllAsync();
        }

        public Task<IRole> GetByIdAsync(Guid id)
        {
            return Repository.GetByIdAsync(id);
        }

        public Task<int> AddAsync(IRole entity)
        {
            return Repository.AddAsync(entity);
        }

        public Task<int> UpdateAsync(IRole entity)
        {
            try
            {
                return Repository.UpdateAsync(entity);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public Task<int> DeleteAsync(IRole entity)
        {
            return Repository.DeleteAsync(entity);
        }

        public Task<int> DeleteAsync(Guid id)
        {
            return Repository.DeleteAsync(id);
        }

        public Task<int> AddUoWAsync(IRole entity)
        {
            using(TransactionScope scope = new TransactionScope())
            {
                Repository.CreateUnitOfWork();
                UnitOfWork = Repository.UnitOfWork;

                Repository.AddAsync(UnitOfWork, entity); 
                var result = UnitOfWork.CommitAsync();
                
                if(result.Result == 1)
                {
                    scope.Complete();
                }
                
                scope.Dispose();
                return result;
            }
        }
    }
}
