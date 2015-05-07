using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;
using ExamPreparation.Service.Common;

namespace ExamPreparation.WebApi.Controllers
{
    [RoutePrefix("api/Exam")]
    public class ExamController : ApiController
    {
        #region Properties

        private IExamService Service { get; set; }

        #endregion Properties

        #region Constructors

        public ExamController(IExamService service)
        {
            Service = service;
        }

        #endregion Constructors

        #region Methods

        #region GET

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
        #endregion GET

        #region POST

        // POST: api/Exam
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post(ExamModel exam)
        {
            exam.Id = Guid.NewGuid();
            try
            {
                var result = await Service.InsertAsync(Mapper.Map<IExam>(exam));
                if (result == 1)
                {
                    return Ok(exam);
                }
                else
                {
                    return new ExceptionResult(new Exception("POST unsuccessful."), this);
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        #endregion POST

        #region PUT

        // PUT: api/Exam/5
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Put(Guid id, ExamModel exam)
        {
            try
            {
                if (id != exam.Id)
                {
                    return BadRequest("IDs do not match.");
                }

                var result = await Service.UpdateAsync(Mapper.Map<IExam>(exam));
                if (result == 1)
                {
                    return Ok(exam);
                }
                else
                {
                    return new ExceptionResult(new Exception("PUT unsuccessful."), this);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        #endregion PUT

        #region DELETE

        // DELETE: api/Exam/
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            try
            {
                if (await Service.DeleteAsync(id) == 1)
                {
                    return Ok("Deleted.");
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

        #endregion DELETE

        #endregion Methods

        #region Model

        public class ExamModel
        {
            public System.Guid Id { get; set; }
            public System.Guid TestingAreaId { get; set; }
            public short Year { get; set; }
            public short Month { get; set; }
            public System.TimeSpan Duration { get; set; }
        }

        #endregion Model
    }
}
