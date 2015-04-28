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

namespace ExamPreparation.WebApi.Controllers
{
    [RoutePrefix("api/TestingArea")]
    public class TestingAreaController : ApiController
    {
        private ITestingAreaService Service { get; set; }

        public TestingAreaController(ITestingAreaService service)
        {
            Service = service;
        }

        // GET: api/TestingArea
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get(string sortOrder = "", string sortDirection = "", int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = await Service.GetAsync(new TestingAreaFilter(sortOrder, sortDirection, pageNumber, pageSize));
                if (result != null)
                {
                    return Ok(Mapper.Map<List<TestingAreaModel>>(result));
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

        // GET: api/TestingArea/5
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            try
            {
                var result = await Service.GetAsync(id);
                if (result != null)
                {
                    return Ok(Mapper.Map<TestingAreaModel>(result));
                }
                else return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        //// POST: api/TestingArea
        //[HttpPost]
        //[Route("")]
        //public async Task<IHttpActionResult> Post(TestingAreaModel testingArea)
        //{
        //    testingArea.Id = Guid.NewGuid();
        //    try
        //    {
        //        var result = await Service.AddAsync(Mapper.Map<TestingArea>(testingArea));
        //        if (result == 1) return Ok(testingArea);
        //        else return BadRequest("POST unsuccessful for " + testingArea.ToString());
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.ToString());
        //    }
        //}

        
        //// PUT: api/TestingArea/5
        //[HttpPut]
        //[Route("{id:guid}")]
        //public async Task<IHttpActionResult> Put(Guid id, TestingAreaModel testingArea)
        //{
        //    try
        //    {
        //        if (id == testingArea.Id)
        //        {
        //            var result = await Service.UpdateAsync(Mapper.Map<TestingArea>(testingArea));
        //            if (result == 1) return Ok(testingArea);
        //            else return NotFound();
        //        }
        //        return BadRequest("IDs do not match.");
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.ToString());
        //    }
        //}

        //// DELETE: api/TestingArea/
        //[HttpDelete]
        //[Route("{id:guid}")]
        //public async Task<IHttpActionResult> Delete(Guid id)
        //{
        //    try
        //    {
        //        var result = await Service.DeleteAsync(id);
        //        if (result == 1) return Ok("Deleted");
        //        else return NotFound();
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.ToString());
        //    }
        //}

        public class TestingAreaModel
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Abrv { get; set; }
        }
    }
}
