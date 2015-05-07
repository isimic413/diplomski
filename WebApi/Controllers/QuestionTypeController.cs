using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model;
using ExamPreparation.Model.Common;
using ExamPreparation.Service.Common;

namespace ExamPreparation.WebApi.Controllers
{
    [RoutePrefix("api/ProblemType")]
    public class QuestionTypeController : ApiController
    {
        #region Properties

        private IQuestionTypeService Service { get; set; }

        #endregion Properties

        #region Constructors

        public QuestionTypeController(IQuestionTypeService service)
        {
            Service = service;
        }

        #endregion Constructors

        #region Methods

        #region GET

        // GET: api/ProblemType
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get(string sortOrder = "", string sortDirection = "", int pageNumber = 0, int pageSize = 0)
        {
            try
            {               
                var result = await Service.GetAsync(new QuestionTypeFilter(sortOrder, sortDirection, pageNumber, pageSize));
                if (result != null)
                {
                    return Ok(Mapper.Map<List<QuestionTypeModel>>(result));
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

        // GET: api/ProblemType/5
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            try
            {
                var result = await Service.GetAsync(id);
                if (result != null)
                {
                    return Ok(Mapper.Map<QuestionTypeModel>(result));
                }
                else return NotFound();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        #endregion GET

        #region POST

        // POST: api/ProblemType
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post(QuestionTypeModel type)
        {
            type.Id = Guid.NewGuid();
            try
            {
                var result = await Service.InsertAsync(Mapper.Map<IQuestionType>(type));
                if (result == 1)
                {
                    return Ok(type);
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

        // PUT: api/ProblemType/5
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Put(Guid id, QuestionTypeModel type)
        {
            try
            {
                if (id == type.Id)
                {
                    var result = await Service.UpdateAsync(Mapper.Map<QuestionType>(type));
                    if (result == 1)
                    {
                        return Ok(type);
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

        // DELETE: api/ProblemType/
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
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        #endregion DELETE

        #endregion Methods

        #region Model

        public class QuestionTypeModel
        {
            public System.Guid Id { get; set; }
            public string Title { get; set; }
            public string Abrv { get; set; }
        }

        #endregion Model
    }
}
