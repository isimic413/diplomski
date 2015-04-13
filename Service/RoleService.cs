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

        public RoleService(IRoleRepository repository)
        {
            Repository = repository;
        }

        public async Task<List<IRole>> GetAsync(string sortOrder = "roleId", int pageNumber = 0, int pageSize = 50)
        {
            try
            {
                return await Repository.GetAsync(sortOrder, pageNumber, pageSize);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<IRole> GetAsync(Guid id)
        {
            try
            {
                return await Repository.GetAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<int> AddAsync(IRole entity)
        {
            try
            {
                return await Repository.AddAsync(entity);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<int> UpdateAsync(IRole entity)
        {
            try
            {
                return await Repository.UpdateAsync(entity);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<int> DeleteAsync(IRole entity)
        {
            try
            {
                return await Repository.DeleteAsync(entity);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            try
            {
                return await Repository.DeleteAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
