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
    [RoutePrefix("api/ExamQuestion")]
    public class ExamQuestionController : ApiController
    {
        #region Properties

        private IExamQuestionService Service { get; set; }

        #endregion Properties

        #region Constructors

        public ExamQuestionController(IExamQuestionService service)
        {
            Service = service;
        }

        #endregion Constructors

        #region Methods

        #region GET

        // GET: api/ExamQuestion
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get(string sortOrder = "", string sortDirection = "", int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = await Service.GetAsync(new ExamQuestionFilter(sortOrder, sortDirection, pageNumber, pageSize));
                if (result != null)
                {
                    return Ok(Mapper.Map<List<ExamQuestionModel>>(result));
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


        // GET: api/ExamQuestion/5
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            try
            {
                var result = await Service.GetAsync(id);
                if (result != null)
                {
                    return Ok(Mapper.Map<ExamQuestionModel>(result));
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

        // GET: api/ExamQuestion/5
        // vratiti samo id-eve zadataka ili odmah cijele zadatke?
        [HttpGet]
        [Route("Questions/{id:guid}")]
        public async Task<IHttpActionResult> GetExamQuestionsAsync(Guid id)
        {
            try
            {
                var result = await Service.GetExamQuestionsAsync(id);
                if (result != null)
                {
                    return Ok(Mapper.Map<List<QuestionController.QuestionModel>>(result));
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

        // GET: api/ExamQuestion/5
        [HttpGet]
        [Route("Questions/{id:guid}/{number}")]
        public async Task<IHttpActionResult> GetQuestionAsync(Guid id, int number)
        {
            try
            {
                var result = await Service.GetQuestionAsync(id, number);
                if (result != null)
                {
                    return Ok(Mapper.Map<QuestionController.QuestionModel>(result));
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

        // POST: api/ExamQuestion
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post(ExamQuestionModel examQuestion)
        {
            examQuestion.Id = Guid.NewGuid();
            try
            {
                var result = await Service.InsertAsync(Mapper.Map<IExamQuestion>(examQuestion));
                if (result == 1)
                {
                    return Ok(examQuestion);
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

        // PUT: api/ExamQuestion/5
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Put(Guid id, ExamQuestionModel entity)
        {
            try
            {
                if (id != entity.Id)
                {
                    return BadRequest("IDs do not match.");
                }

                var result = await Service.UpdateAsync(Mapper.Map<IExamQuestion>(entity));

                if (result == 1)
                {
                    return Ok(entity);
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

        // DELETE: api/ExamQuestion/
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

        public class ExamQuestionModel
        {
            public System.Guid Id { get; set; }
            public System.Guid ProblemId { get; set; }
            public System.Guid ExamId { get; set; }
            public short ProblemNumber { get; set; }
        }

        #endregion Model
    }
}
