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

namespace ExamPreparation.WebApi.Controllers
{
    [RoutePrefix("api/ExamQuestion")]
    public class ExamQuestionController : ApiController
    {
        private IExamQuestionService Service { get; set; }

        public ExamQuestionController(IExamQuestionService service)
        {
            Service = service;
        }

        // GET: api/ExamQuestion
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get(string sortOrder = "", string sortDirection = "", int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = await Service.GetAsync(new ExamQuestionFilter(sortOrder, sortDirection, pageNumber, pageSize));
                if (result != null)
                {
                    return Ok(Mapper.Map<List<ExamQuestionModel>>(result));
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


        // GET: api/ExamQuestion/5
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            try
            {
                var result = await Service.GetAsync(id);
                if (result != null)
                {
                    return Ok(Mapper.Map<ExamQuestionModel>(result));
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

        // GET: api/ExamQuestion/5
        // vratiti samo id-eve zadataka ili odmah cijele zadatke?
        [HttpGet]
        [Route("Questions/{id:guid}")]
        public async Task<IHttpActionResult> GetExamQuestionsAsync(Guid id)
        {
            try
            {
                var result = await Service.GetExamQuestionsAsync(id);
                if (result != null)
                {
                    return Ok(Mapper.Map<List<QuestionController.QuestionModel>>(result));
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

        // GET: api/ExamQuestion/5
        [HttpGet]
        [Route("Questions/{id:guid}/{number}")]
        public async Task<IHttpActionResult> GetQuestionAsync(Guid id, int number)
        {
            try
            {
                var result = await Service.GetQuestionAsync(id, number);
                if (result != null)
                {
                    return Ok(Mapper.Map<QuestionController.QuestionModel>(result));
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

        //// POST: api/ExamQuestion
        //[HttpPost]
        //[Route("")]
        //public async Task<IHttpActionResult> Post(ExamProblemModel examProblem)
        //{
        //    examProblem.Id = Guid.NewGuid();
        //    try
        //    {
        //        var result = await Service.AddAsync(Mapper.Map<ExamProblemModel, ExamProblem>(examProblem));
        //        if (result == 1) return Ok(examProblem);
        //        else return BadRequest();

        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest();
        //    }
        //}


        //// PUT: api/ExamQuestion/5
        //[HttpPut]
        //[Route("{id:guid}")]
        //public async Task<IHttpActionResult> Put(Guid id, ExamProblemModel examProblem)
        //{
        //    if (id == examProblem.Id)
        //    {
        //        var result = await Service.UpdateAsync(Mapper.Map<ExamProblemModel, ExamProblem>(examProblem));
        //        if (result == 1) return Ok(examProblem);
        //        else return NotFound();
        //    }
        //    return BadRequest();
        //}

        //// DELETE: api/ExamQuestion/
        //[HttpDelete]
        //[Route("{id:guid}")]
        //public async Task<IHttpActionResult> Delete(Guid id)
        //{
        //    var result = await Service.DeleteAsync(id);
        //    if (result == 1) return Ok("Deleted");
        //    else return NotFound();
        //}

        public class ExamQuestionModel
        {
            public System.Guid Id { get; set; }
            public System.Guid ProblemId { get; set; }
            public System.Guid ExamId { get; set; }
            public short ProblemNumber { get; set; }
        }
    }
}
