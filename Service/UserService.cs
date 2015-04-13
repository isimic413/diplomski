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
    public class UserService: IUserService
    {
        protected IUserRepository Repository { get; set; }
        protected IUnitOfWork UnitOfWork;

        public UserService(IUserRepository repository)
        {
            Repository = repository;
        }

        public Task<List<IUser>> GetPageAsync(int pageSize, int pageNumber)
        {
            throw new Exception("Not implemented!");
        }

        public Task<List<IUser>> GetAllAsync()
        {
            throw new Exception("Not implemented!");
        }

        public Task<IUser> GetByIdAsync(Guid id)
        {
            throw new Exception("Not implemented!");
        }

        public Task<int> AddAsync(IUser entity)
        {
            throw new Exception("Not implemented!");
        }

        public Task<int> UpdateAsync(IUser entity)
        {
            throw new Exception("Not implemented!");
        }

        public Task<int> DeleteAsync(IUser entity)
        {
            throw new Exception("Not implemented!");
        }

        public Task<int> DeleteAsync(Guid id)
        {
            throw new Exception("Not implemented!");
        }

        public Task<int> AddUoWAsync(IUser entity)
        {
            throw new Exception("Not implemented!");
        }
    }
}
