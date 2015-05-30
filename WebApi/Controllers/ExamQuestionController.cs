using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

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

        // GET: api/ExamQuestion
        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> Get(string sortOrder = "", string sortDirection = "", 
            int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = await Service.GetAsync(new ExamQuestionFilter(sortOrder, sortDirection, pageNumber, pageSize));
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        Mapper.Map<List<ExamQuestionModel>>(result));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
            }
        }

        // GET: api/ExamQuestion/5
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<HttpResponseMessage> Get(Guid id)
        {
            try
            {
                var result = await Service.GetAsync(id);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        Mapper.Map<ExamQuestionModel>(result));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
            }
        }

        // GET: api/ExamQuestion/5
        // vratiti samo id-eve zadataka ili odmah cijele zadatke?
        [HttpGet]
        [Route("Questions/{id:guid}")]
        public async Task<HttpResponseMessage> GetExamQuestionsAsync(Guid id, string sortOrder = "", 
            string sortDirection = "", int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = await Service.GetExamQuestionsAsync(
                    id, new ExamQuestionFilter(sortOrder, sortDirection, pageNumber, pageSize));

                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        Mapper.Map<List<QuestionController.QuestionModel>>(result));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
            }
        }

        // GET: api/ExamQuestion/5
        [HttpGet]
        [Route("Questions/{id:guid}/{number}")]
        public async Task<HttpResponseMessage> GetQuestionAsync(Guid id, int number)
        {
            try
            {
                var result = await Service.GetQuestionAsync(id, number);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        Mapper.Map<QuestionController.QuestionModel>(result));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
            }
        }

        // POST: api/ExamQuestion
        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> Post(ExamQuestionModel entity)
        {
            entity.Id = Guid.NewGuid();
            try
            {
                var result = await Service.InsertAsync(Mapper.Map<IExamQuestion>(entity));
                if (result == 1)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError,
                        "POST unsuccessful.");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
            }
        }

        // PUT: api/ExamQuestion/5
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<HttpResponseMessage> Put(Guid id, ExamQuestionModel entity)
        {
            try
            {
                if (id != entity.Id)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "IDs do not match.");
                }

                var result = await Service.UpdateAsync(Mapper.Map<IExamQuestion>(entity));

                if (result == 1)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError,
                        "PUT unsuccessful.");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
            }
        }

        // DELETE: api/ExamQuestion/
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            try
            {
                if (await Service.DeleteAsync(id) == 1)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Deleted.");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
            }
        }

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
