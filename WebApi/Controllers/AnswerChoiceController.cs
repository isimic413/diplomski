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

// dovrsiti mapping iz AnswerChoice u AnswerChoiceModel
namespace ExamPreparation.WebApi.Controllers
{
    [RoutePrefix("api/AnswerChoice")]
    public class AnswerChoiceController : ApiController
    {
        private IAnswerChoiceService Service { get; set; }
        private IAnswerChoicePictureService PictureService { get; set; }

        public AnswerChoiceController(IAnswerChoiceService service)
        {
            Service = service;
        }

        // GET: api/AnswerChoice
        [HttpGet]
        public async Task<IHttpActionResult> Get(string sortOrder = "", string sortDirection = "", int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = await Service.GetAsync(new AnswerChoiceFilter(sortOrder, sortDirection, pageNumber, pageSize));
                if (result != null)
                {
                    return Ok(Mapper.Map<List<AnswerChoiceModel>>(result));
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

        // GET: api/AnswerChoice/5
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            try
            {
                var result = await Service.GetAsync(id);
                if(result != null)
                {
                    return Ok(Mapper.Map<AnswerChoiceModel>(result));
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
        [Route("Question/{id:guid}")]
        public async Task<IHttpActionResult> GetChoices(Guid id)
        {
            try
            {
                var result = await Service.GetChoicesAsync(id);
                if (result != null)
                {
                    return Ok(Mapper.Map<AnswerChoiceModel>(result));
                }
                else return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpGet]
        [Route("Question/Solution/{id:guid}")] // "Question/{id:guid}/Solution" ?
        public async Task<IHttpActionResult> GetCorrectAnswers(Guid id)
        {
            try
            {
                var result = await Service.GetCorrectAnswersAsync(id);
                if (result != null)
                {
                    return Ok(Mapper.Map<AnswerChoiceModel>(result));
                }
                else return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }    



        //// POST: api/AnswerChoice
        //[HttpPost]
        //[Route("")]
        //public async Task<IHttpActionResult> Post(AnswerChoiceModel choice, AnswerChoicePictureModel picture = null)
        //{
        //    choice.Id = Guid.NewGuid();
        //    try
        //    {
        //        if (picture != null)
        //        {
        //            picture.Id = Guid.NewGuid();
        //            picture.AnswerChoiceId = choice.Id;
        //        }

        //        var result = await Service.AddAsync(Mapper.Map<AnswerChoice>(choice), Mapper.Map<AnswerChoicePicture>(picture));
        //        if (result == 1) return Ok(choice);
        //        else return BadRequest();

        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.ToString());
        //    }
        //}

        //// PUT: api/AnswerChoice/5
        //[HttpPut]
        //[Route("{id:guid}")]
        //public async Task<IHttpActionResult> Put(Guid id, AnswerChoiceModel choice, AnswerChoicePicture picture = null)
        //{
        //    if (id == choice.Id)
        //    {
        //        var result = await Service.UpdateAsync(Mapper.Map<AnswerChoice>(choice), picture);
        //        if (result == 1) return Ok(choice);
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


        public class AnswerChoiceModel
        {
            public System.Guid Id { get; set; }
            public System.Guid ProblemId { get; set; }
            public bool IsCorrect { get; set; }
            public string Text { get; set; }
            public bool HasPicture { get; set; }
            public string PictureUrl { get; set; }
        }

        //public class AnswerChoicePictureModel
        //{
        //    public System.Guid Id { get; set; }
        //    public System.Guid AnswerChoiceId { get; set; }
        //    public byte[] Picture { get; set; }
        //}
    }
}
