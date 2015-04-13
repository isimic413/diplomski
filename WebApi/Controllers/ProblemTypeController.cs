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
    [RoutePrefix("api/ProblemType")]
    public class ProblemTypeController : ApiController
    {
        private IProblemTypeService Service { get; set; }

        public ProblemTypeController(IProblemTypeService service)
        {
            Service = service;
        }

        // GET: api/ProblemType
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get(string sortOrder = "roleId", int pageNumber = 1, int pageSize = 50)
        {
            try
            {
                var types = await Service.GetAsync(sortOrder, pageNumber, pageSize);
                var typesResult = Mapper.Map<List<ProblemTypeModel>>(types);
                return Ok(typesResult);
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
            var type = await Service.GetAsync(id);
            if (type != null)
            {
                var result = Mapper.Map<ProblemTypeModel>(type);
                return Ok(result);
            }
            else return NotFound();
        }

        // POST: api/ProblemType
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post(ProblemTypeModel type)
        {
            type.Id = Guid.NewGuid();
            try
            {
                var result = await Service.AddAsync(Mapper.Map<ProblemType>(type));
                if (result == 1) return Ok(type);
                else return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }


        // PUT: api/ProblemType/5
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Put(Guid id, ProblemTypeModel type)
        {
            try
            {
                if (id == type.Id)
                {
                    var result = await Service.UpdateAsync(Mapper.Map<ProblemType>(type));
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
    }

    public class ProblemTypeModel
    {
        public System.Guid Id { get; set; }
        public string Title { get; set; }
        public string Abrv { get; set; }
    }
}
