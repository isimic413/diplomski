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
    [RoutePrefix("api/TestingArea")]
    public class TestingAreaController : ApiController
    {
        #region Properties

        private ITestingAreaService Service { get; set; }

        #endregion Properties

        #region Constructors

        public TestingAreaController(ITestingAreaService service)
        {
            Service = service;
        }

        #endregion Constructors

        #region Methods

        // GET: api/TestingArea
        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> Get(string sortOrder = "", string sortDirection = "", 
            int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = await Service.GetAsync(new TestingAreaFilter(sortOrder, sortDirection, pageNumber, pageSize));
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, 
                        Mapper.Map<List<TestingAreaModel>>(result));
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

        // GET: api/TestingArea/5
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<HttpResponseMessage> Get(Guid id)
        {
            try
            {
                var result = await Service.GetAsync(id);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<TestingAreaModel>(result));
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

        // POST: api/TestingArea
        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> Post(TestingAreaModel entity)
        {
            entity.Id = Guid.NewGuid();
            try
            {
                var result = await Service.InsertAsync(Mapper.Map<ITestingArea>(entity));
                if (result == 1)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, entity);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "POST unsuccessful.");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
            }
        }

        // PUT: api/TestingArea/5
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<HttpResponseMessage> Put(Guid id, TestingAreaModel entity)
        {
            try
            {
                if (id == entity.Id)
                {
                    var result = await Service.UpdateAsync(Mapper.Map<ITestingArea>(entity));
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

        // DELETE: api/TestingArea/
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            try
            {
                if (await Service.DeleteAsync(id) == 1)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Deleted");
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

        public class TestingAreaModel
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Abrv { get; set; }
        }

        #endregion Model
    }
}
