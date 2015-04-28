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
    public class UserRoleRepository: IUserRoleRepository
    {
        protected IRepository Repository { get; private set; }
        public IUnitOfWork UnitOfWork { get; set; }

        public UserRoleRepository(IRepository repository)
        {
            Repository = repository;
        }

        public void CreateUnitOfWork()
        {
            UnitOfWork = Repository.CreateUnitOfWork();
        }

        public virtual Task<List<IUserRole>> GetPageAsync(int pageSize = 0, int pageNumber = 0)
        {
            if (pageSize <= 0) return GetAllAsync();

            var dalPage = Repository.WhereAsync<DALModel.UserRole>()
                .OrderBy(item => item.RoleId)
                .Skip<DALModel.UserRole>((pageNumber - 1) * pageSize)
                .Take<DALModel.UserRole>(pageSize)
                .ToListAsync<DALModel.UserRole>()
                .Result;

            var userRoles = Mapper.Map<List<DALModel.UserRole>, List<ExamModel.UserRole>>(dalPage);
            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.UserRole>, List<IUserRole>>(userRoles));
        }

        public virtual Task<List<IUserRole>> GetAllAsync()
        {
            var dalUserRoles = Repository.WhereAsync<DALModel.UserRole>().ToListAsync<DALModel.UserRole>().Result;
            var userRoles = Mapper.Map<List<DALModel.UserRole>, List<ExamModel.UserRole>>(dalUserRoles);
            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.UserRole>, List<IUserRole>>(userRoles));
        }

        public virtual Task<IUserRole> GetByIdAsync(Guid id)
        {
            var dalUserRole = Repository.SingleAsync<DALModel.UserRole>(id).Result;
            IUserRole userRole = Mapper.Map<DALModel.UserRole, ExamModel.UserRole>(dalUserRole);
            return Task.Factory.StartNew(() => userRole);
        }

        public virtual Task<int> AddAsync(IUserRole entity)
        {
            try
            {
                var userRole = Mapper.Map<ExamModel.UserRole>(entity);
                var dalUserRole = Mapper.Map<ExamModel.UserRole, DALModel.UserRole>(userRole);
                return Repository.AddAsync<DALModel.UserRole>(dalUserRole);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }

        }

        public virtual Task<int> UpdateAsync(IUserRole entity)
        {
            var userRole = Mapper.Map<IUserRole, ExamModel.UserRole>(entity);
            var dalUserRole = Mapper.Map<ExamModel.UserRole, DALModel.UserRole>(userRole);
            try
            {
                return Repository.UpdateAsync<DALModel.UserRole>(dalUserRole);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual Task<int> DeleteAsync(IUserRole entity)
        {
            var userRole = Mapper.Map<IUserRole, ExamModel.UserRole>(entity);
            var dalUserRole = Mapper.Map<ExamModel.UserRole, DALModel.UserRole>(userRole);
            return Repository.DeleteAsync<DALModel.UserRole>(dalUserRole);
        }

        public virtual Task<int> DeleteAsync(Guid id)
        {
            return Repository.DeleteAsync<DALModel.UserRole>(id);
        }

        public virtual Task<int> AddAsync(IUnitOfWork unitOfWork, IUserRole entity)
        {
            var userRole = Mapper.Map<ExamModel.UserRole>(entity);
            var dalUserRole = Mapper.Map<ExamModel.UserRole, DALModel.UserRole>(userRole);
            return unitOfWork.AddAsync<DALModel.UserRole>(dalUserRole);
        }
    }
}
