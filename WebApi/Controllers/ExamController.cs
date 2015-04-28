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
    [RoutePrefix("api/Exam")]
    public class ExamController : ApiController
    {
        private IExamService Service { get; set; }

        public ExamController(IExamService service)
        {
            Service = service;
        }


        // GET: api/Exam
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get(string sortOrder = "", string sortDirection = "", int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = await Service.GetAsync(new ExamFilter(sortOrder, sortDirection, pageNumber, pageSize));
                if (result != null)
                {
                    return Ok(Mapper.Map<List<ExamModel>>(result));
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

        // GET: api/Exam/5
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            try
            {
                var result = await Service.GetAsync(id);
                if (result != null)
                {
                    return Ok(Mapper.Map<List<ExamModel>>(result));
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
        [Route("Year/{year}")]
        public async Task<IHttpActionResult> GetByYearAsync(int year, string sortDirection = "", int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = await Service.GetByYearAsync(year, new ExamFilter("Year", sortDirection, pageNumber, pageSize));
                if (result != null)
                {
                    return Ok(Mapper.Map<List<ExamModel>>(result));
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
        [Route("TestingArea/{id:guid}")]
        public async Task<IHttpActionResult> GetByTestingAreaIdAsync(Guid id, string sortDirection = "", int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = await Service.GetByTestingAreaIdAsync(
                    id, new ExamFilter("TestingAreaId", sortDirection, pageNumber, pageSize));

                if (result != null)
                {
                    return Ok(Mapper.Map<List<ExamModel>>(result));
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


        //    // POST: api/Exam
        //    [HttpPost]
        //    [Route("")]
        //    public async Task<IHttpActionResult> Post(ExamModel exam)
        //    {
        //        exam.Id = Guid.NewGuid();
        //        try
        //        {
        //            var result = await Service.AddAsync(Mapper.Map<ExamModel, Exam>(exam));
        //            if (result == 1) return Ok(exam);
        //            else return BadRequest();

        //        }
        //        catch (Exception e)
        //        {
        //            return BadRequest();
        //        }
        //    }

        //    // PUT: api/Exam/5
        //    [HttpPut]
        //    [Route("{id:guid}")]
        //    public async Task<IHttpActionResult> Put(Guid id, ExamModel exam)
        //    {
        //        if (id == exam.Id)
        //        {
        //            var result = await Service.UpdateAsync(Mapper.Map<ExamModel, Exam>(exam));
        //            if (result == 1) return Ok(exam);
        //            else return NotFound();
        //        }
        //        return BadRequest();
        //    }

        //    // DELETE: api/Exam/
        //    [HttpDelete]
        //    [Route("{id:guid}")]
        //    public async Task<IHttpActionResult> Delete(Guid id)
        //    {
        //        var result = await Service.DeleteAsync(id);
        //        if (result == 1) return Ok("Deleted");
        //        else return NotFound();
        //    }


        public class ExamModel
        {
            public System.Guid Id { get; set; }
            public System.Guid TestingAreaId { get; set; }
            public short Year { get; set; }
            public short Month { get; set; }
            public System.TimeSpan Duration { get; set; }
        }
    }
}
