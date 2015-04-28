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


// dovrsiti mapu za AnswerStep u AnswerStepModel (URL)
namespace ExamPreparation.WebApi.Controllers
{
    [RoutePrefix("api/AnswerStep")]
    public class AnswerStepController : ApiController
    {
        private IAnswerStepService Service { get; set; }

        public AnswerStepController(IAnswerStepService service)
        {
            Service = service;
        }

        // GET: api/AnswerStep
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get(string sortOrder = "", string sortDirection = "", int pageSize = 0, int pageNumber = 0)
        {
            try
            {
                AnswerStepFilter filter = new AnswerStepFilter(sortOrder, sortDirection, pageNumber, pageSize);
                var result = await Service.GetAsync(filter);
                if (result != null)
                {
                    return Ok(Mapper.Map<List<AnswerStepModel>>(result));
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

        // GET: api/AnswerStep
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            try
            {
                var result = await Service.GetAsync(id);
                if (result != null)
                {
                    return Ok(Mapper.Map<AnswerStepModel>(result));
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

        [HttpGet]
        [Route("question/{id:guid}")]
        public async Task<IHttpActionResult> GetSteps(Guid id)
        {
            try
            {
                var result = await Service.GetStepsAsync(id);
                if (result != null)
                {
                    return Ok(Mapper.Map<List<AnswerStepModel>>(result));
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


        //// POST: api/AnswerStep
        //[HttpPost]
        //[Route("")]
        //public async Task<IHttpActionResult> Post(AnswerStepModel step)
        //{
        //    step.Id = Guid.NewGuid();
        //    try
        //    {
        //        var result = await Service.AddAsync(Mapper.Map<AnswerStepModel, AnswerStep>(step));
        //        if (result == 1) return Ok(step);
        //        else return BadRequest();

        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest();
        //    }
        //}

        //// PUT: api/AnswerChoice/5
        //[HttpPut]
        //[Route("{id:guid}")]
        //public async Task<IHttpActionResult> Put(Guid id, AnswerStepModel step)
        //{
        //    if (id == step.Id)
        //    {
        //        var result = await Service.UpdateAsync(Mapper.Map<AnswerStepModel, AnswerStep>(step));
        //        if (result == 1) return Ok(step);
        //        else return NotFound();
        //    }
        //    return BadRequest();
        //}

        //// DELETE: api/AnswerChoice/
        //[HttpDelete]
        //[Route("{id:guid}")]
        //public async Task<IHttpActionResult> Delete(Guid id)
        //{
        //    var result = await Service.DeleteAsync(id);
        //    if (result == 1) return Ok("Deleted");
        //    else return NotFound();
        //}




        public class AnswerStepModel
        {
            public System.Guid Id { get; set; }
            public System.Guid QuestionId { get; set; }
            public short StepNumber { get; set; }
            public byte Points { get; set; }
            public string Text { get; set; }
            public string PictureUrl { get; set; }
        }
    }
}
