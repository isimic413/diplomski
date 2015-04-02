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
        [Route("page")]
        public async Task<IHttpActionResult> Get(int pageSize, int pageNumber)
        {
            try
            {
                var problemTypes = await Service.GetPageAsync(pageSize, pageNumber);
                var result = Mapper.Map<List<ProblemType>>(problemTypes);
                return Ok(Mapper.Map<List<ProblemType>, List<ProblemTypeModel>>(result));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // GET: api/ProblemType
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var problemTypes = await Service.GetAllAsync();
                var problemTypeResult = Mapper.Map<List<ProblemType>>(problemTypes);
                return Ok(problemTypeResult);
            }
            catch 
            {
                return NotFound();
            }
        }

        // GET: api/ProblemType/5
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var iProblemType = await Service.GetByIdAsync(id);
            if (iProblemType != null)
            {
                var problemType = Mapper.Map<ProblemType>(iProblemType);
                return Ok(problemType);
            }
            else return NotFound();
        }

        // POST: api/ProblemType
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post(ProblemTypeModel problemType)
        {
            problemType.Id = Guid.NewGuid();
            try
            {
                var result = await Service.AddAsync(Mapper.Map<ProblemTypeModel, ProblemType>(problemType));
                if (result == 1) return Ok(problemType);
                else return BadRequest();
                
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // UoW?
        [HttpPost]
        [Route("uow")]
        public async Task<IHttpActionResult> PostNew(ProblemTypeModel problemType)
        {
            problemType.Id = Guid.NewGuid();
            try
            {
                var result = await Service.AddUoWAsync(Mapper.Map<ProblemTypeModel, ProblemType>(problemType));
                if (result == 1) return Ok(problemType);
                else return BadRequest();

            }
            catch (Exception e)
            {
                return Ok(e.ToString());
            }
        }

        // PUT: api/ProblemType/5
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Put(Guid id, ProblemTypeModel problemType)
        {
            if (id == problemType.Id)
            {
                var result = await Service.UpdateAsync(Mapper.Map<ProblemTypeModel, ProblemType>(problemType));
                if (result == 1) return Ok(problemType);
                else return NotFound();
            }
            return BadRequest();
        }

        // DELETE: api/ProblemType/
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var result = await Service.DeleteAsync(id);
            if (result == 1) return Ok("Deleted");
            else return NotFound();
        }
    }

    public class ProblemTypeModel
    {
        public System.Guid Id { get; set; }
        public string Title { get; set; }
        public string Abrv { get; set; }
        public virtual ICollection<Problem> Problems { get; set; }
    }
}
