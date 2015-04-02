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
    [RoutePrefix("api/AnswerChoice")]
    public class AnswerChoiceController : ApiController
    {
        private IAnswerChoiceService Service { get; set; }

        public AnswerChoiceController(IAnswerChoiceService service)
        {
            Service = service;
        }

        // GET: api/AnswerChoice
        [HttpGet]
        [Route("page")]
        public async Task<IHttpActionResult> Get(int pageSize, int pageNumber)
        {
            try
            {
                var choices = await Service.GetPageAsync(pageSize, pageNumber);
                var result = Mapper.Map<List<AnswerChoice>>(choices);
                return Ok(Mapper.Map<List<AnswerChoice>, List<AnswerChoiceModel>>(result));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // GET: api/AnswerChoice
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var choices = await Service.GetAllAsync();
                var result = Mapper.Map<List<AnswerChoice>>(choices);
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: api/AnswerChoice/5
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var choice = await Service.GetByIdAsync(id);
            if (choice != null)
            {
                var result = Mapper.Map<AnswerChoice>(choice);
                return Ok(result);
            }
            else return NotFound();
        }


        [HttpGet]
        [Route("problem-correct/{id:guid}")] // ?
        public async Task<IHttpActionResult> GetCorrectAnswer(Guid id)
        {
            var choice = await Service.GetCorrectAnswer(id);
            if (choice != null)
            {
                var result = Mapper.Map<AnswerChoice>(choice);
                return Ok(result);
            }
            else return NotFound();
        }

        [HttpGet]
        [Route("problem/{id:guid}")]
        public async Task<IHttpActionResult> GetChoices(Guid id)
        {
            var choices = await Service.GetChoicesByProblemId(id);
            if (choices != null)
            {
                var result = Mapper.Map<AnswerChoice>(choices);
                return Ok(result);
            }
            else return NotFound();
        }


        // POST: api/AnswerChoice
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post(AnswerChoiceModel choice)
        {
            choice.Id = Guid.NewGuid();
            try
            {
                var result = await Service.AddAsync(Mapper.Map<AnswerChoiceModel, AnswerChoice>(choice));
                if (result == 1) return Ok(choice);
                else return BadRequest();

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // UoW? - zbog slika...
        //[HttpPost]
        //[Route("uow")]
        //public async Task<IHttpActionResult> PostNew(ProblemTypeModel problemType)
        //{
        //    problemType.Id = Guid.NewGuid();
        //    try
        //    {
        //        var result = await Service.AddUoWAsync(Mapper.Map<ProblemTypeModel, ProblemType>(problemType));
        //        if (result == 1) return Ok(problemType);
        //        else return BadRequest();

        //    }
        //    catch (Exception e)
        //    {
        //        return Ok(e.ToString());
        //    }
        //}

        // PUT: api/AnswerChoice/5
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Put(Guid id, AnswerChoiceModel choice)
        {
            if (id == choice.Id)
            {
                var result = await Service.UpdateAsync(Mapper.Map<AnswerChoiceModel, AnswerChoice>(choice));
                if (result == 1) return Ok(choice);
                else return NotFound();
            }
            return BadRequest();
        }

        // DELETE: api/AnswerChoice/
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var result = await Service.DeleteAsync(id);
            if (result == 1) return Ok("Deleted");
            else return NotFound();
        }
    }

    public class AnswerChoiceModel
    {
        public System.Guid Id { get; set; }
        public System.Guid ProblemId { get; set; }
        public bool IsCorrect { get; set; }
        public string Text { get; set; }
        public bool HasPicture { get; set; }
        public virtual Problem Problem { get; set; }
        public virtual ICollection<AnswerChoicePicture> AnswerChoicePictures { get; set; }
    }
}
