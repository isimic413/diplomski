//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using ExamPreparation.Repo.Repository;
//using ExamPreparation.Repo.UnitOfWork;

//namespace ExamPreparation.Services
//{
//    public class TestingAreaService : RepoTestingArea
//    {
//        private UoW _unitOfWork;

//        public TestingAreaService(UoW unitOfWork) : base(unitOfWork.DbContext)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        public IQueryable<RepoTestingArea> GetByAbrv(string abrv)
//        {
//            IQueryable<RepoTestingArea> testingAreas = _unitOfWork.TestingAreas.GetAll().Where(c => c.Abrv == abrv);
//            return testingAreas;
//        }
//    }
//}
