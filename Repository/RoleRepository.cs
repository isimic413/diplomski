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
    public class RoleRepository: IRoleRepository
    {
        protected IRepository Repository { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }

        public RoleRepository(IRepository repository)
        {
            Repository = repository;
        }

        public void CreateUnitOfWork()
        {
            UnitOfWork = Repository.CreateUnitOfWork();
        }

        public virtual Task<List<IRole>> GetPageAsync(int pageSize = 0, int pageNumber = 0)
        {
            if (pageSize <= 0) return GetAllAsync();

            var dalPage = Repository.WhereAsync<DALModel.Role>()
                .OrderBy(item => item.Abrv)
                .Skip<DALModel.Role>((pageNumber - 1) * pageSize)
                .Take<DALModel.Role>(pageSize)
                .ToListAsync<DALModel.Role>()
                .Result;

            var roles = Mapper.Map<List<DALModel.Role>, List<ExamModel.Role>>(dalPage);
            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.Role>, List<IRole>>(roles));
        }

        public virtual Task<List<IRole>> GetAllAsync()
        {
            var dalRoles = Repository.WhereAsync<DALModel.Role>().ToListAsync<DALModel.Role>().Result;
            var roles = Mapper.Map<List<DALModel.Role>, List<ExamModel.Role>>(dalRoles);
            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.Role>, List<IRole>>(roles));
        }

        public virtual Task<IRole> GetByIdAsync(Guid id)
        {
            var dalRole = Repository.SingleAsync<DALModel.Role>(id).Result;
            IRole role = Mapper.Map<DALModel.Role, ExamModel.Role>(dalRole);
            return Task.Factory.StartNew(() => role);
        }

        public virtual Task<int> AddAsync(IRole entity)
        {
            try
            {
                var role = Mapper.Map<ExamModel.Role>(entity);
                var dalRole = Mapper.Map<ExamModel.Role, DALModel.Role>(role);
                return Repository.AddAsync<DALModel.Role>(dalRole);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }

        }

        public virtual Task<int> UpdateAsync(IRole entity)
        {
            var role = Mapper.Map<IRole, ExamModel.Role>(entity);
            var dalRole = Mapper.Map<ExamModel.Role, DALModel.Role>(role);
            try
            {
                return Repository.UpdateAsync<DALModel.Role>(dalRole);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual Task<int> DeleteAsync(IRole entity)
        {
            var role = Mapper.Map<IRole, ExamModel.Role>(entity);
            var dalRole = Mapper.Map<ExamModel.Role, DALModel.Role>(role);
            return Repository.DeleteAsync<DALModel.Role>(dalRole);
        }

        public virtual Task<int> DeleteAsync(Guid id)
        {
            return Repository.DeleteAsync<DALModel.Role>(id);
        }

        public virtual Task<int> AddAsync(IUnitOfWork unitOfWork, IRole entity)
        {
            var role = Mapper.Map<ExamModel.Role>(entity);
            var dalRole = Mapper.Map<ExamModel.Role, DALModel.Role>(role);
            return unitOfWork.AddAsync<DALModel.Role>(dalRole);
        }
    }
}
