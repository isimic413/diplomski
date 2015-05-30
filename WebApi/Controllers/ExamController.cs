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

        // GET: api/Exam
        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> Get(string sortOrder = "", string sortDirection = "", 
            int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = await Service.GetAsync(new ExamFilter(sortOrder, sortDirection, pageNumber, pageSize));
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        Mapper.Map<List<ExamModel>>(result));
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

        // GET: api/Exam/5
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
                        Mapper.Map<List<ExamModel>>(result));
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


        [HttpGet]
        [Route("Year/{year}")]
        public async Task<HttpResponseMessage> GetByYearAsync(int year, string sortDirection = "", 
            int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = await Service.GetByYearAsync(
                    year, new ExamFilter("Year", sortDirection, pageNumber, pageSize));

                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        Mapper.Map<List<ExamModel>>(result));
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

        [HttpGet]
        [Route("TestingArea/{id:guid}")]
        public async Task<HttpResponseMessage> GetByTestingAreaIdAsync(Guid id, string sortDirection = "", 
            int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = await Service.GetByTestingAreaIdAsync(
                    id, new ExamFilter("TestingAreaId", sortDirection, pageNumber, pageSize));

                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        Mapper.Map<List<ExamModel>>(result));
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

        // POST: api/Exam
        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> Post(ExamModel entity)
        {
            entity.Id = Guid.NewGuid();
            try
            {
                var result = await Service.InsertAsync(Mapper.Map<IExam>(entity));
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

        // PUT: api/Exam/5
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<HttpResponseMessage> Put(Guid id, ExamModel entity)
        {
            try
            {
                if (id != entity.Id)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest,
                        "IDs do not match.");
                }

                var result = await Service.UpdateAsync(Mapper.Map<IExam>(entity));
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

        // DELETE: api/Exam/
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
