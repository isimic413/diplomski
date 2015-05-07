//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//using ExamPreparation.Model.Common;
//using ExamPreparation.Repository.Common;
//using ExamPreparation.Service.Common;

//namespace ExamPreparation.Service
//{
//    public class QuestionPictureService: IQuestionPictureService
//    {
//        protected IQuestionPictureRepository Repository { get; private set; }

//        public QuestionPictureService(IQuestionPictureRepository repository)
//        {
//            Repository = repository;
//        }

//        public async Task<List<IQuestionPicture>> GetAsync(string sortOrder = "problemId", int pageNumber = 0, int pageSize = 50)
//        {
//            try
//            {
//                return await Repository.GetAsync(sortOrder, pageNumber, pageSize);
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.ToString());
//            }
//        }

//        public async Task<IQuestionPicture> GetAsync(Guid id)
//        {
//            try
//            {
//                return await Repository.GetAsync(id);
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.ToString());
//            }
//        }

//        public async Task<int> AddAsync(IQuestionPicture entity)
//        {
//            try
//            {
//                return await Repository.AddAsync(entity);
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.ToString());
//            }
//        }

//        public async Task<int> UpdateAsync(IQuestionPicture entity)
//        {
//            try
//            {
//                return await Repository.UpdateAsync(entity);
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.ToString());
//            }
//        }

//        public async Task<int> DeleteAsync(IQuestionPicture entity)
//        {
//            try
//            {
//                return await Repository.DeleteAsync(entity);
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.ToString());
//            }
//        }

//        public async Task<int> DeleteAsync(Guid id)
//        {
//            try
//            {
//                return await Repository.DeleteAsync(id);
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.ToString());
//            }
//        }
//    }
//}
