﻿using AutoMapper;
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
        [Route("")]
        public async Task<IHttpActionResult> Get(string sortOrder = "roleId", int pageNumber = 1, int pageSize = 50)
        {
            try
            {
                var roles = await Service.GetAsync(sortOrder, pageNumber, pageSize);
                var rolesResult = Mapper.Map<List<RoleModel>>(roles);
                return Ok(rolesResult);
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
            var iRole = await Service.GetAsync(id);
            if (iRole != null)
            {
                var testingArea = Mapper.Map<RoleModel>(iRole);
                return Ok(testingArea);
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
                var result = await Service.AddAsync(Mapper.Map<Role>(role));
                if (result == 1) return Ok(role);
                else return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }


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
                    if (result == 1) return Ok(role);
                    else return NotFound();
                }
                return BadRequest("IDs do not match.");
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

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
    }
    public class RoleModel
    {
        public System.Guid Id { get; set; }
        public string Title { get; set; }
        public string Abrv { get; set; }
    }
}
