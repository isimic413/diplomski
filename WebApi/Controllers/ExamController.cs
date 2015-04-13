//using AutoMapper;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Web.Http;

//using ExamPreparation.Model;
//using ExamPreparation.Model.Common;
//using ExamPreparation.Service;
//using ExamPreparation.Service.Common;
//using ExamPreparation.WebApi.Models;

//namespace ExamPreparation.WebApi.Controllers
//{
//    [RoutePrefix("api/Exam")]
//    public class ExamController : ApiController
//    {
//        private IExamService Service { get; set; }

//        public ExamController(IExamService service)
//        {
//            Service = service;
//        }

//        // GET: api/Exam
//        [HttpGet]
//        [Route("page")]
//        public async Task<IHttpActionResult> Get(int pageSize, int pageNumber)
//        {
//            try
//            {
//                var exams = await Service.GetPageAsync(pageSize, pageNumber);
//                var result = Mapper.Map<List<Exam>>(exams);
//                return Ok(Mapper.Map<List<Exam>, List<ExamModel>>(result));
//            }
//            catch (Exception e)
//            {
//                return BadRequest();
//            }
//        }

//        // GET: api/Exam
//        [HttpGet]
//        [Route("")]
//        public async Task<IHttpActionResult> Get()
//        {
//            try
//            {
//                var exam = await Service.GetAllAsync();
//                var result = Mapper.Map<List<Exam>>(exam);
//                return Ok(result);
//            }
//            catch
//            {
//                return NotFound();
//            }
//        }

//        // GET: api/Exam/5
//        [HttpGet]
//        [Route("{id:guid}")]
//        public async Task<IHttpActionResult> Get(Guid id)
//        {
//            var exam = await Service.GetByIdAsync(id);
//            if (exam != null)
//            {
//                var result = Mapper.Map<Exam>(exam);
//                return Ok(result);
//            }
//            else return NotFound();
//        }


//        [HttpGet]
//        [Route("")] 
//        public async Task<IHttpActionResult> GetByYear(int year)
//        {
//            var exams = await Service.GetByYear(year);
//            if (exams != null)
//            {
//                var result = Mapper.Map<Exam>(exams);
//                return Ok(result);
//            }
//            else return NotFound();
//        }

//        [HttpGet]
//        [Route("")]
//        public async Task<IHttpActionResult> GetByTestingArea(TestingAreaModel testingArea)
//        {
//            var exams = await Service.GetByTestingAreaId(testingArea.Id); // ?
//            if (exams != null)
//            {
//                var result = Mapper.Map<Exam>(exams);
//                return Ok(result);
//            }
//            else return NotFound();
//        }


//        // POST: api/Exam
//        [HttpPost]
//        [Route("")]
//        public async Task<IHttpActionResult> Post(ExamModel exam)
//        {
//            exam.Id = Guid.NewGuid();
//            try
//            {
//                var result = await Service.AddAsync(Mapper.Map<ExamModel, Exam>(exam));
//                if (result == 1) return Ok(exam);
//                else return BadRequest();

//            }
//            catch (Exception e)
//            {
//                return BadRequest();
//            }
//        }

//        // UoW - unos zadataka odmah?
//        //[HttpPost]
//        //[Route("uow")]
//        //public async Task<IHttpActionResult> PostNew(ProblemTypeModel problemType)
//        //{
//        //    problemType.Id = Guid.NewGuid();
//        //    try
//        //    {
//        //        var result = await Service.AddUoWAsync(Mapper.Map<ProblemTypeModel, ProblemType>(problemType));
//        //        if (result == 1) return Ok(problemType);
//        //        else return BadRequest();

//        //    }
//        //    catch (Exception e)
//        //    {
//        //        return Ok(e.ToString());
//        //    }
//        //}

//        // PUT: api/Exam/5
//        [HttpPut]
//        [Route("{id:guid}")]
//        public async Task<IHttpActionResult> Put(Guid id, ExamModel exam)
//        {
//            if (id == exam.Id)
//            {
//                var result = await Service.UpdateAsync(Mapper.Map<ExamModel, Exam>(exam));
//                if (result == 1) return Ok(exam);
//                else return NotFound();
//            }
//            return BadRequest();
//        }

//        // DELETE: api/Exam/
//        [HttpDelete]
//        [Route("{id:guid}")]
//        public async Task<IHttpActionResult> Delete(Guid id)
//        {
//            var result = await Service.DeleteAsync(id);
//            if (result == 1) return Ok("Deleted");
//            else return NotFound();
//        }
//    }

//    public class ExamModel
//    {
//        public System.Guid Id { get; set; }
//        public System.Guid TestingAreaId { get; set; }
//        public short Year { get; set; }
//        public short Month { get; set; }
//        public System.TimeSpan Duration { get; set; }
//        public virtual TestingArea TestingArea { get; set; }
//        public virtual ICollection<ExamProblem> ExamProblems { get; set; }
//    }
//}
