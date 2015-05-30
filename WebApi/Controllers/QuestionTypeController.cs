using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

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

        // GET: api/ProblemType
        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> Get(string sortOrder = "", string sortDirection = "", 
            int pageNumber = 0, int pageSize = 0)
        {
            try
            {               
                var result = await Service.GetAsync(new QuestionTypeFilter(sortOrder, sortDirection, pageNumber, pageSize));
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        Mapper.Map<List<QuestionTypeModel>>(result));
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

        // GET: api/ProblemType/5
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
                        Mapper.Map<QuestionTypeModel>(result));
                }
                else return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
            }
        }

        // POST: api/ProblemType
        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> Post(QuestionTypeModel entity)
        {
            entity.Id = Guid.NewGuid();
            try
            {
                var result = await Service.InsertAsync(Mapper.Map<IQuestionType>(entity));
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

        // PUT: api/ProblemType/5
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<HttpResponseMessage> Put(Guid id, QuestionTypeModel entity)
        {
            try
            {
                if (id == entity.Id)
                {
                    var result = await Service.UpdateAsync(Mapper.Map<QuestionType>(entity));
                    if (result == 1)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "IDs do not match.");
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
            }
        }

        // DELETE: api/ProblemType/
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
            catch (ArgumentException e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
            }
        }

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
