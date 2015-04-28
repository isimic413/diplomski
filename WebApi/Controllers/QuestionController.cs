using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model;
using ExamPreparation.Model.Common;
using ExamPreparation.Service;
using ExamPreparation.Service.Common;
using ExamPreparation.WebApi.Models;


// dovrsiti mapu (slike)
namespace ExamPreparation.WebApi.Controllers
{
    [RoutePrefix("api/Question")]
    public class QuestionController : ApiController
    {
        private IQuestionService Service { get; set; }

        public QuestionController(IQuestionService service)
        {
            Service = service;
        }


        // GET: api/Problem
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get(string sortOrder = "", string sortDirection = "", int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = await Service.GetAsync(new QuestionFilter(sortOrder, sortDirection, pageNumber, pageSize));
                if (result != null)
                {
                    return Ok(Mapper.Map<List<QuestionModel>>(result));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        // GET: api/Problem/5
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            try
            {
                var result = await Service.GetAsync(id);
                if (result != null)
                {
                    return Ok(Mapper.Map<QuestionModel>(result));
                }
                else return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        
        [HttpGet]
        [Route("TestingArea/{id:guid}")]
        public async Task<IHttpActionResult> GetByTestingArea(Guid id)
        {
            try
            {
                var result = await Service.GetByTestingAreaIdAsync(id, new QuestionFilter("TestingAreaId",0,0));
                if (result != null)
                {
                    return Ok(Mapper.Map<List<QuestionModel>>(result));
                }
                else return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpGet]
        [Route("QuestionType/{id:guid}")]
        public async Task<IHttpActionResult> GetByType(Guid id)
        {
            try
            {
                var result = await Service.GetByTypeIdAsync(id, new QuestionFilter("QuestionTypeId",0,0));
                if (result != null)
                {
                    return Ok(Mapper.Map<List<QuestionModel>>(result));
                }
                else return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }



        //[HttpPost]
        //[Route("")]
        //public async Task<IHttpActionResult> Post(ProblemModel problem, List<AnswerChoiceModel> choices)
        //{
        //    problem.Id = Guid.NewGuid();
        //    try
        //    {
        //        var mappedChoices = Mapper.Map<List<AnswerChoiceModel>, List<AnswerChoice>>(choices);
        //        var result = await Service.AddAsync(
        //             Mapper.Map<ProblemModel, Problem>(problem),
        //             Mapper.Map<List<IAnswerChoice>>(mappedChoices)
        //            );

        //        if (result == 1) return Ok(problem);
        //        else return BadRequest();

        //    }
        //    catch (Exception e)
        //    {
        //        return Ok(e.ToString());
        //    }
        //}

        //// PUT: api/Problem/5
        //[HttpPut]
        //[Route("{id:guid}")]
        //public async Task<IHttpActionResult> Put(Guid id, ProblemModel problem)
        //{
        //    if (id == problem.Id)
        //    {
        //        var result = await Service.UpdateAsync(Mapper.Map<ProblemModel, Problem>(problem));
        //        if (result == 1) return Ok(problem);
        //        else return NotFound();
        //    }
        //    return BadRequest();
        //}

        //// DELETE: api/Problem/
        //[HttpDelete]
        //[Route("{id:guid}")]
        //public async Task<IHttpActionResult> Delete(Guid id)
        //{
        //    var result = await Service.DeleteAsync(id);
        //    if (result == 1) return Ok("Deleted");
        //    else return NotFound();
        //}


        public class QuestionModel
        {
            public System.Guid Id { get; set; }
            public System.Guid TestingAreaId { get; set; }
            public System.Guid QuestionTypeId { get; set; }
            public string Text { get; set; }
            public byte Points { get; set; }
            public bool PictureUrl { get; set; }
            public bool HasSteps { get; set; }
        }
    }
}