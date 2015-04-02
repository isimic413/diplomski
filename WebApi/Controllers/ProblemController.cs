using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using ExamPreparation.Model;
using ExamPreparation.Model.Common;
using ExamPreparation.Service;
using ExamPreparation.Service.Common;
using ExamPreparation.WebApi.Models;

namespace ExamPreparation.WebApi.Controllers
{
    [RoutePrefix("api/Problem")]
    public class ProblemController : ApiController
    {
        private IProblemService Service { get; set; }

        public ProblemController(IProblemService service)
        {
            Service = service;
        }

        // GET: api/Problem
        [HttpGet]
        [Route("page")]
        public async Task<IHttpActionResult> Get(int pageSize, int pageNumber)
        {
            try
            {
                var problems = await Service.GetPageAsync(pageSize, pageNumber);
                var result = Mapper.Map<List<Problem>>(problems);
                return Ok(Mapper.Map<List<Problem>, List<ProblemModel>>(result));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // GET: api/Problem
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var problems = await Service.GetAllAsync();
                var result = Mapper.Map<List<Problem>>(problems);
                return Ok(Mapper.Map<List<Problem>, List<ProblemModel>>(result));
            }
            catch 
            {
                return NotFound();
            }
        }

        // GET: api/Problem/5
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var result = await Service.GetByIdAsync(id);
            if (result != null)
            {
                //var res = Mapper.Map<TestingArea>(result);
                return Ok(result);
            }
            else return NotFound();
        }

        // POST: api/Problem
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post(ProblemModel problem)
        {
            problem.Id = Guid.NewGuid();
            try
            {
                var result = await Service.AddAsync(Mapper.Map<ProblemModel, Problem>(problem));
                if (result == 1) return Ok(problem);
                else return BadRequest();
                
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetByTestingArea(TestingAreaModel testingArea)
        {
            var problems = await Service.GetByTestingAreaId(testingArea.Id); // ?
            if (problems != null)
            {
                var result = Mapper.Map<AnswerChoice>(problems);
                return Ok(result);
            }
            else return NotFound();
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetByType(ProblemTypeModel type)
        {
            var problems = await Service.GetByTypeId(type.Id); // ?
            if (problems != null)
            {
                var result = Mapper.Map<Problem>(problems);
                return Ok(result);
            }
            else return NotFound();
        }
       
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post(ProblemModel problem, List<AnswerChoiceModel> choices)
        {
            problem.Id = Guid.NewGuid();
            try
            {
                var mappedChoices = Mapper.Map<List<AnswerChoiceModel>, List<AnswerChoice>>(choices);
                var result = await Service.AddAsync(
                     Mapper.Map<ProblemModel, Problem>(problem), 
                     Mapper.Map<List<IAnswerChoice>>(mappedChoices)
                    );

                if (result == 1) return Ok(problem);
                else return BadRequest();

            }
            catch (Exception e)
            {
                return Ok(e.ToString());
            }
        }

        // PUT: api/Problem/5
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Put(Guid id, ProblemModel problem)
        {
            if (id == problem.Id)
            {
                var result = await Service.UpdateAsync(Mapper.Map<ProblemModel, Problem>(problem));
                if (result == 1) return Ok(problem);
                else return NotFound();
            }
            return BadRequest();
        }

        // DELETE: api/Problem/
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var result = await Service.DeleteAsync(id);
            if (result == 1) return Ok("Deleted");
            else return NotFound();
        }
    }

    public class ProblemModel 
    {
        public System.Guid Id { get; set; }
        public System.Guid TestingAreaId { get; set; }
        public System.Guid ProblemTypeId { get; set; }
        public string Text { get; set; }
        public byte Points { get; set; }
        public bool HasPicture { get; set; }
        public bool HasSteps { get; set; }
        public virtual ICollection<AnswerChoice> AnswerChoices { get; set; }
        public virtual ICollection<AnswerStep> AnswerSteps { get; set; }
        public virtual ICollection<ExamProblem> ExamProblems { get; set; }
        public virtual ProblemType ProblemType { get; set; }
        public virtual TestingArea TestingArea { get; set; }
        public virtual ICollection<ProblemPicture> ProblemPictures { get; set; }
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
    }
}