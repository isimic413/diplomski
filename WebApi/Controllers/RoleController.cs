using AutoMapper;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model;
using ExamPreparation.Model.Common;
using ExamPreparation.Service.Common;

namespace ExamPreparation.WebApi.Controllers
{
    [RoutePrefix("api/Role")]
    public class RoleController : ApiController
    {
        #region Properties

        private IRoleService Service { get; set; }

        #endregion Properties

        #region Constructors

        public RoleController(IRoleService service)
        {
            Service = service;
        }

        #endregion Constructors

        #region Methods

        #region GET

        // GET: api/Role
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get(string sortOrder = "", string sortDirection = "", int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = await Service.GetAsync(new RoleFilter(sortOrder, sortDirection, pageNumber, pageSize));
                if (result != null)
                {
                    return Ok(Mapper.Map<RoleModel>(result));
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

        // GET: api/Role/5
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            try
            {
                var result = await Service.GetAsync(id);
                if (result != null)
                {
                    return Ok(Mapper.Map<RoleModel>(result));
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

        #endregion GET

        #region POST

        // POST: api/Role
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post(RoleModel role)
        {
            role.Id = Guid.NewGuid();
            try
            {
                var result = await Service.InsertAsync(Mapper.Map<IRole>(role));
                if (result == 1)
                {
                    return Ok(role);
                }
                else
                {
                    return new ExceptionResult(new Exception("POST unsuccessful."), this);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        #endregion POST

        #region PUT

        // PUT: api/Role/5
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Put(Guid id, RoleModel role)
        {
            try
            {
                if (id == role.Id)
                {
                    var result = await Service.UpdateAsync(Mapper.Map<Role>(role));
                    if (result == 1)
                    {
                        return Ok(role);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                return BadRequest("IDs do not match.");
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        #endregion PUT

        #region DELETE

        // DELETE: api/Role/
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

        #endregion DELETE

        #endregion Methods

        #region Model

        public class RoleModel
        {
            public System.Guid Id { get; set; }
            public string Title { get; set; }
            public string Abrv { get; set; }
        }

        #endregion Model
    }
}
