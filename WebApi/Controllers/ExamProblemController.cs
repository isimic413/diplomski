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
//    [RoutePrefix("api/ExamProblem")]
//    public class ExamProblemController : ApiController
//    {
//        private IExamProblemService Service { get; set; }

//        public ExamProblemController(IExamProblemService service)
//        {
//            Service = service;
//        }

//        // GET: api/ExamProblem
//        [HttpGet]
//        [Route("page")]
//        public async Task<IHttpActionResult> Get(int pageSize, int pageNumber)
//        {
//            try
//            {
//                var examProblems = await Service.GetPageAsync(pageSize, pageNumber);
//                var result = Mapper.Map<List<ExamProblem>>(examProblems);
//                return Ok(Mapper.Map<List<ExamProblem>, List<ExamProblemModel>>(result));
//            }
//            catch (Exception e)
//            {
//                return BadRequest();
//            }
//        }

//        // GET: api/ExamProblem
//        [HttpGet]
//        [Route("")]
//        public async Task<IHttpActionResult> Get()
//        {
//            try
//            {
//                var examProblem = await Service.GetAllAsync();
//                var result = Mapper.Map<List<TestingArea>>(examProblem);
//                return Ok(Mapper.Map<List<TestingArea>, List<TestingAreaModel>>(result));
//            }
//            catch
//            {
//                return NotFound();
//            }
//        }

//        // GET: api/ExamProblem/5
//        [HttpGet]
//        [Route("problems/{id:guid}")]
//        public async Task<IHttpActionResult> GetProblems(Guid id)
//        {
//            var result = await Service.GetExamProblemsById(id);
//            if (result != null)
//            {
//                return Ok(result);
//            }
//            else return NotFound();
//        }

//        // GET: api/ExamProblem/5
//        [HttpGet]
//        [Route("{id:guid}")]
//        public async Task<IHttpActionResult> Get(Guid id)
//        {
//            var result = await Service.GetByIdAsync(id);
//            if (result != null)
//            {
//                return Ok(result);
//            }
//            else return NotFound();
//        }

//        // GET: api/ExamProblem/5
//        [HttpGet]
//        [Route("{id:guid}")]
//        public async Task<IHttpActionResult> Get(Guid id, int number)
//        {
//            var result = await Service.GetExamProblemByExamId(id, number);
//            if (result != null)
//            {
//                return Ok(result);
//            }
//            else return NotFound();
//        }

//        // POST: api/ExamProblem
//        [HttpPost]
//        [Route("")]
//        public async Task<IHttpActionResult> Post(ExamProblemModel examProblem)
//        {
//            examProblem.Id = Guid.NewGuid();
//            try
//            {
//                var result = await Service.AddAsync(Mapper.Map<ExamProblemModel, ExamProblem>(examProblem));
//                if (result == 1) return Ok(examProblem);
//                else return BadRequest();

//            }
//            catch (Exception e)
//            {
//                return BadRequest();
//            }
//        }

       
//        // PUT: api/ExamProblem/5
//        [HttpPut]
//        [Route("{id:guid}")]
//        public async Task<IHttpActionResult> Put(Guid id, ExamProblemModel examProblem)
//        {
//            if (id == examProblem.Id)
//            {
//                var result = await Service.UpdateAsync(Mapper.Map<ExamProblemModel, ExamProblem>(examProblem));
//                if (result == 1) return Ok(examProblem);
//                else return NotFound();
//            }
//            return BadRequest();
//        }

//        // DELETE: api/ExamProblem/
//        [HttpDelete]
//        [Route("{id:guid}")]
//        public async Task<IHttpActionResult> Delete(Guid id)
//        {
//            var result = await Service.DeleteAsync(id);
//            if (result == 1) return Ok("Deleted");
//            else return NotFound();
//        }
//    }

//    public class ExamProblemModel
//    {
//        public System.Guid Id { get; set; }
//        public System.Guid ProblemId { get; set; }
//        public System.Guid ExamId { get; set; }
//        public short ProblemNumber { get; set; }
//        public virtual Exam Exam { get; set; }
//        public virtual Problem Problem { get; set; }
//    }
//}
