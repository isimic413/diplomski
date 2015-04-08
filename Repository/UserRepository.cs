//using AutoMapper;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using ExamPreparation.Model.Common;
//using ExamPreparation.Repository.Common;
//using DALModel = ExamPreparation.DAL.Models;
//using ExamModel = ExamPreparation.Model;

//namespace ExamPreparation.Repository
//{
//    public class UserRepository: IUserRepository
//    {
//        protected IRepository Repository { get; set; }
//        public IUnitOfWork UnitOfWork { get; set; }

//        public UserRepository(IRepository repository)
//        {
//            Repository = repository;
//        }

//        public void CreateUnitOfWork()
//        {
//            UnitOfWork = Repository.CreateUnitOfWork();
//        }

//        public virtual Task<List<IUser>> GetPageAsync(int pageSize = 0, int pageNumber = 0)
//        {
//            if (pageSize <= 0) return GetAllAsync();

//            var dalPage = Repository.WhereAsync<DALModel.User>()
//                .OrderBy(item => item.Email)
//                .Skip<DALModel.User>((pageNumber - 1) * pageSize)
//                .Take<DALModel.User>(pageSize)
//                .ToListAsync<DALModel.User>()
//                .Result;

//            var users = Mapper.Map<List<DALModel.User>, List<ExamModel.User>>(dalPage);
//            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.User>, List<IUser>>(users));
//        }

//        public virtual Task<List<IUser>> GetAllAsync()
//        {
//            var dalUsers = Repository.WhereAsync<DALModel.User>().ToListAsync<DALModel.User>().Result;
//            var users = Mapper.Map<List<DALModel.User>, List<ExamModel.User>>(dalUsers);
//            return Task.Factory.StartNew(() => Mapper.Map<List<ExamModel.User>, List<IUser>>(users));
//        }

//        public virtual Task<IUser> GetByIdAsync(Guid id)
//        {
//            var dalUser = Repository.SingleAsync<DALModel.User>(id).Result;
//            IUser user = Mapper.Map<DALModel.User, ExamModel.User>(dalUser);
//            return Task.Factory.StartNew(() => user);
//        }

//        public virtual Task<int> AddAsync(IUser entity)
//        {
//            try
//            {
//                var user = Mapper.Map<ExamModel.User>(entity);
//                var dalUser = Mapper.Map<ExamModel.User, DALModel.User>(user);
//                return Repository.AddAsync<DALModel.User>(dalUser);
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.ToString());
//            }

//        }

//        public virtual Task<int> UpdateAsync(IUser entity)
//        {
//            var user = Mapper.Map<IUser, ExamModel.User>(entity);
//            var dalUser = Mapper.Map<ExamModel.User, DALModel.User>(user);
//            try
//            {
//                return Repository.UpdateAsync<DALModel.User>(dalUser);
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.ToString());
//            }
//        }

//        public virtual Task<int> DeleteAsync(IUser entity)
//        {
//            var user = Mapper.Map<IUser, ExamModel.User>(entity);
//            var dalUser = Mapper.Map<ExamModel.User, DALModel.User>(user);
//            return Repository.DeleteAsync<DALModel.User>(dalUser);
//        }

//        public virtual Task<int> DeleteAsync(Guid id)
//        {
//            return Repository.DeleteAsync<DALModel.User>(id);
//        }

//        public virtual Task<int> AddAsync(IUnitOfWork unitOfWork, IUser entity)
//        {
//            var user = Mapper.Map<ExamModel.User>(entity);
//            var dalUser = Mapper.Map<ExamModel.User, DALModel.User>(user);
//            return unitOfWork.AddAsync<DALModel.User>(dalUser);
//        }
//    }
//}
