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
//    [RoutePrefix("api/AnswerStep")]
//    public class AnswerStepController : ApiController
//    {
//        private IAnswerStepService Service { get; set; }

//        public AnswerStepController(IAnswerStepService service)
//        {
//            Service = service;
//        }

//        // GET: api/AnswerStep
//        [HttpGet]
//        [Route("page")]
//        public async Task<IHttpActionResult> Get(int pageSize, int pageNumber)
//        {
//            try
//            {
//                var steps = await Service.GetPageAsync(pageSize, pageNumber);
//                var result = Mapper.Map<List<AnswerStep>>(steps);
//                return Ok(Mapper.Map<List<AnswerStep>, List<AnswerStep>>(result));
//            }
//            catch (Exception e)
//            {
//                return BadRequest();
//            }
//        }

//        // GET: api/AnswerStep
//        [HttpGet]
//        [Route("")]
//        public async Task<IHttpActionResult> Get()
//        {
//            try
//            {
//                var step = await Service.GetAllAsync();
//                var result = Mapper.Map<List<AnswerStep>>(step);
//                return Ok(result);
//            }
//            catch
//            {
//                return NotFound();
//            }
//        }

//        // GET: api/AnswerStep/5
//        [HttpGet]
//        [Route("{id:guid}")]
//        public async Task<IHttpActionResult> Get(Guid id)
//        {
//            var step = await Service.GetByIdAsync(id);
//            if (step != null)
//            {
//                var result = Mapper.Map<AnswerStep>(step);
//                return Ok(result);
//            }
//            else return NotFound();
//        }


//        [HttpGet]
//        [Route("problem/{id:guid}")]
//        public async Task<IHttpActionResult> GetSteps(Guid id)
//        {
//            var steps = await Service.GetStepsByProblemId(id);
//            if (steps != null)
//            {
//                var result = Mapper.Map<AnswerStep>(steps);
//                return Ok(result);
//            }
//            else return NotFound();
//        }


//        // POST: api/AnswerStep
//        [HttpPost]
//        [Route("")]
//        public async Task<IHttpActionResult> Post(AnswerStepModel step)
//        {
//            step.Id = Guid.NewGuid();
//            try
//            {
//                var result = await Service.AddAsync(Mapper.Map<AnswerStepModel, AnswerStep>(step));
//                if (result == 1) return Ok(step);
//                else return BadRequest();

//            }
//            catch (Exception e)
//            {
//                return BadRequest();
//            }
//        }

//        // UoW? - zbog slika...
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

//        // PUT: api/AnswerChoice/5
//        [HttpPut]
//        [Route("{id:guid}")]
//        public async Task<IHttpActionResult> Put(Guid id, AnswerStepModel step)
//        {
//            if (id == step.Id)
//            {
//                var result = await Service.UpdateAsync(Mapper.Map<AnswerStepModel, AnswerStep>(step));
//                if (result == 1) return Ok(step);
//                else return NotFound();
//            }
//            return BadRequest();
//        }

//        // DELETE: api/AnswerChoice/
//        [HttpDelete]
//        [Route("{id:guid}")]
//        public async Task<IHttpActionResult> Delete(Guid id)
//        {
//            var result = await Service.DeleteAsync(id);
//            if (result == 1) return Ok("Deleted");
//            else return NotFound();
//        }
//    }

//    public class AnswerStepModel
//    {
//        public System.Guid Id { get; set; }
//        public System.Guid ProblemId { get; set; }
//        public short StepNumber { get; set; }
//        public byte Points { get; set; }
//        public string Text { get; set; }
//        public bool HasPicture { get; set; }
//        public virtual Problem Problem { get; set; }
//        public virtual ICollection<AnswerStepPicture> AnswerStepPictures { get; set; }
//    }
//}
