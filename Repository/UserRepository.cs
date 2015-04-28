using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using DALModel = ExamPreparation.DAL.Models;
using ExamModel = ExamPreparation.Model;

namespace ExamPreparation.Repository
{
    public class UserRepository : IUserRepository
    {
        protected IRepository Repository { get; private set; }
        public IUnitOfWork UnitOfWork { get; private set; }

        public UserRepository(IRepository repository)
        {
            Repository = repository;
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            return Repository.CreateUnitOfWork();
        }

        public virtual Task<List<IUser>> GetAsync(string sortOrder = "email", int pageNumber = 0, int pageSize = 50)
        {
            throw new Exception("Not implemented!");
        }

        public virtual Task<IUser> GetAsync(Guid id)
        {
            throw new Exception("Not implemented!");
        }

        public virtual Task<int> AddAsync(IUser entity, List<IRole> roles = null)
        {
            throw new Exception("Not implemented!");
        }

        public virtual Task<int> UpdateAsync(IUser entity)
        {
            throw new Exception("Not implemented!");
        }

        public virtual Task<int> DeleteAsync(IUser entity)
        {
            throw new Exception("Not implemented!");
        }

        public virtual Task<int> DeleteAsync(Guid id)
        {
            throw new Exception("Not implemented!");
        }
    }
}
