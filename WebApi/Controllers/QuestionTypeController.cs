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
    [RoutePrefix("api/ProblemType")]
    public class QuestionTypeController : ApiController
    {
        private IQuestionTypeService Service { get; set; }

        public QuestionTypeController(IQuestionTypeService service)
        {
            Service = service;
        }

        // GET: api/ProblemType
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get(string sortOrder = "", string sortDirection = "", int pageNumber = 0, int pageSize = 0)
        {
            try
            {               
                var result = await Service.GetAsync(new QuestionTypeFilter(sortOrder, sortDirection, pageNumber, pageSize));
                if (result != null)
                {
                    return Ok(Mapper.Map<List<QuestionTypeModel>>(result));
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

        // GET: api/ProblemType/5
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            try
            {
                var result = await Service.GetAsync(id);
                if (result != null)
                {
                    return Ok(Mapper.Map<QuestionTypeModel>(result));
                }
                else return NotFound();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        // POST: api/ProblemType
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post(QuestionTypeModel type)
        {
            type.Id = Guid.NewGuid();
            try
            {
                var result = await Service.AddAsync(Mapper.Map<QuestionType>(type));
                if (result == 1) return Ok(type);
                else return BadRequest("POST unsuccessful for " + type.ToString());
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }


        // PUT: api/ProblemType/5
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Put(Guid id, QuestionTypeModel type)
        {
            try
            {
                if (id == type.Id)
                {
                    var result = await Service.UpdateAsync(Mapper.Map<QuestionType>(type));
                    if (result == 1) return Ok(type);
                    else return NotFound();
                }
                return BadRequest("IDs do not match.");
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        // DELETE: api/ProblemType/
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            try
            {
                var result = await Service.DeleteAsync(id);
                if (result == 1) return Ok("Deleted");
                else return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        public class QuestionTypeModel
        {
            public System.Guid Id { get; set; }
            public string Title { get; set; }
            public string Abrv { get; set; }
        }
    }
}
