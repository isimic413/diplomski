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
    [RoutePrefix("api/Role")]
    public class RoleController : ApiController
    {
        private IRoleService Service { get; set; }

        public RoleController(IRoleService service)
        {
            Service = service;
        }

        // GET: api/Role
        [HttpGet]
        [Route("page")]
        public async Task<IHttpActionResult> Get(int pageSize, int pageNumber)
        {
            try
            {
                var roles = await Service.GetPageAsync(pageSize, pageNumber);
                var roleResult = Mapper.Map<List<Role>>(roles);
                return Ok(Mapper.Map<List<Role>, List<RoleModel>>(roleResult));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // GET: api/Role
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var roles = await Service.GetAllAsync();
                var result = Mapper.Map<List<Role>>(roles);
                return Ok(result);
            }
            catch 
            {
                return NotFound();
            }
        }

        // GET: api/Role/5
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var iRole = await Service.GetByIdAsync(id);
            if (iRole != null)
            {
                var role = Mapper.Map<Role>(iRole);
                return Ok(role);
            }
            else return NotFound();
        }

        // POST: api/Role
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post(RoleModel role)
        {
            role.Id = Guid.NewGuid();
            try
            {
                var result = await Service.AddAsync(Mapper.Map<RoleModel, Role>(role));
                if (result == 1) return Ok(role);
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
        public async Task<IHttpActionResult> PostNew(RoleModel role)
        {
            role.Id = Guid.NewGuid();
            try
            {
                var result = await Service.AddUoWAsync(Mapper.Map<RoleModel, Role>(role));
                if (result == 1) return Ok(role);
                else return BadRequest();

            }
            catch (Exception e)
            {
                return Ok(e.ToString());
            }
        }

        // PUT: api/Role/5
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Put(Guid id, RoleModel role)
        {
            if (id == role.Id)
            {
                var result = await Service.UpdateAsync(Mapper.Map<RoleModel, Role>(role));
                if (result == 1) return Ok(role);
                else return NotFound();
            }
            return BadRequest();
        }

        // DELETE: api/Role/
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var result = await Service.DeleteAsync(id);
            if (result == 1) return Ok("Deleted");
            else return NotFound();
        }
    }

    public class RoleModel
    {
        public System.Guid Id { get; set; }
        public string Title { get; set; }
        public string Abrv { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
