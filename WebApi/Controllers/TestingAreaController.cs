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
        [Route("page")]
        public async Task<IHttpActionResult> Get(int pageSize, int pageNumber)
        {
            try
            {
                var testingAreas = await Service.GetPageAsync(pageSize, pageNumber);
                var testingAreaResult = Mapper.Map<List<TestingArea>>(testingAreas);
                return Ok(Mapper.Map<List<TestingArea>, List<TestingAreaModel>>(testingAreaResult));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // GET: api/TestingArea
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var testingAreas = await Service.GetAllAsync();
                var testingAreaResult = Mapper.Map<List<TestingArea>>(testingAreas);
                // return Ok(Mapper.Map<List<TestingArea>, List<TestingAreaModel>>(testingAreaResult));
                return Ok(testingAreaResult);
            }
            catch 
            {
                return NotFound();
            }
        }

        // GET: api/TestingArea/5
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var iTestingArea = await Service.GetByIdAsync(id);
            if (iTestingArea != null)
            {
                var testingArea = Mapper.Map<TestingArea>(iTestingArea);
                //return Ok(Mapper.Map<TestingArea, TestingAreaModel>(testingArea));
                return Ok(testingArea);
            }
            else return NotFound();
        }

        // POST: api/TestingArea
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post(TestingAreaModel testingArea)
        {
            testingArea.Id = Guid.NewGuid();
            try
            {
                var result = await Service.AddAsync(Mapper.Map<TestingAreaModel, TestingArea>(testingArea));
                if (result == 1) return Ok(testingArea);
                else return BadRequest();
                
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // Za provjeru UoW-a. Inace nece trebati UoW za TestingArea...
        [HttpPost]
        [Route("uow")]
        public async Task<IHttpActionResult> PostNew(TestingAreaModel testingArea)
        {
            testingArea.Id = Guid.NewGuid();
            try
            {
                var result = await Service.AddUoWAsync(Mapper.Map<TestingAreaModel, TestingArea>(testingArea));
                if (result == 1) return Ok(testingArea);
                else return BadRequest();

            }
            catch (Exception e)
            {
                return Ok(e.ToString());
            }
        }

        // PUT: api/TestingArea/5
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Put(Guid id, TestingAreaModel testingArea)
        {
            if(id == testingArea.Id)
            {
                var result = await Service.UpdateAsync(Mapper.Map<TestingAreaModel, TestingArea>(testingArea));
                if (result == 1) return Ok(testingArea);
                else return NotFound();
            }
            return BadRequest();
        }

        // DELETE: api/TestingArea/
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var result = await Service.DeleteAsync(id);
            if (result == 1) return Ok("Deleted");
            else return NotFound();
        }
    }

    public class TestingAreaModel
    {
        public Guid Id { get; set; }

        //[Display(Name = "Testing area title")]
        public string Title { get; set; }

        //[Display(Name = "Testing area abbreviation")]
        public string Abrv { get; set; }


        //public virtual ICollection<Exam> Exams { get; set; }
        //public virtual ICollection<TestingAreaProblem> TestingAreaProblems { get; set; }
    }
}
