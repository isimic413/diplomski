using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;
using ExamPreparation.Service.Common;

namespace ExamPreparation.WebApi.Controllers
{
    [RoutePrefix("api/AnswerStep")]
    public class AnswerStepController : ApiController
    {
        #region Properties

        private IAnswerStepService Service { get; set; }
        private IAnswerStepPictureService PictureService { get; set; }

        #endregion Properties

        #region Constructors

        public AnswerStepController(IAnswerStepService service, IAnswerStepPictureService pictureService)
        {
            Service = service;
            PictureService = pictureService;
        }

        #endregion Constructors

        #region Methods

        // GET: api/AnswerStep
        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> Get(string sortOrder = "", string sortDirection = "", 
            int pageSize = 0, int pageNumber = 0)
        {
            try
            {
                AnswerStepFilter filter = new AnswerStepFilter(sortOrder, sortDirection, pageNumber, pageSize);
                var result = await Service.GetAsync(filter);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        Mapper.Map<List<AnswerStepModel>>(result));
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

        // GET: api/AnswerStep
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
                        Mapper.Map<AnswerStepModel>(result));
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
        [Route("Question/{id:guid}")]
        public async Task<HttpResponseMessage> GetSteps(Guid id)
        {
            try
            {
                var result = await Service.GetStepsAsync(id);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        Mapper.Map<List<AnswerStepModel>>(result));
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

        // POST: api/AnswerStep
        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> Post(AnswerStepModel entity, AnswerStepPictureModel entityPicture = null)
        {
            entity.Id = Guid.NewGuid();
            try
            {
                if (entity.StepNumber < 1)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest,
                        "StepNumber cannot be less than 1.");
                }

                if (entityPicture != null)
                {
                    entityPicture.Id = Guid.NewGuid();
                    entityPicture.AnswerStepId = entity.Id;
                }

                var result = await Service.InsertAsync(Mapper.Map<IAnswerStep>(entity), 
                    Mapper.Map<IAnswerStepPicture>(entityPicture));

                if (result == 1)
                {
                    entity.PictureUrl = (entityPicture != null) ? "AnswerStep/" + entityPicture.Id.ToString() : "";
                    //HttpContext.Current.Request.Url.AbsoluteUri + "/Picture/" + picture.Id.ToString() : "";

                    return Request.CreateResponse(HttpStatusCode.OK, entity);
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

        // PUT: api/AnswerStep/5
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<HttpResponseMessage> Put(Guid id, AnswerStepModel entity, 
            AnswerStepPictureModel entityPicture = null)
        {
            try
            {
                if (id != entity.Id)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "IDs do not match.");
                }

                if (entityPicture != null)
                {
                    entityPicture.AnswerStepId = id;

                    if (entity.PictureUrl == null || entity.PictureUrl == "")
                    {
                        entityPicture.Id = Guid.NewGuid();
                    }
                    else
                    {
                        entityPicture.Id = new Guid(entity.PictureUrl.Split('/').Last());
                    }
                }

                var result = await Service.UpdateAsync(Mapper.Map<IAnswerStep>(entity), 
                    Mapper.Map<IAnswerStepPicture>(entityPicture));

                if (result == 1)
                {
                    if (entity.PictureUrl == null || entity.PictureUrl == "")
                    {
                        entity.PictureUrl = (entityPicture != null) ? "AnswerStep/" + entityPicture.Id.ToString() : "";
                        //HttpContext.Current.Request.Url.AbsoluteUri + "/Picture/" + picture.Id.ToString() : "";
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "PUT unsuccessful.");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
            }
        }

        // PUT: api/AnswerStep/Picture/5
        [HttpPut]
        [Route("Picture/{id:guid}")]
        public async Task<HttpResponseMessage> Put(Guid id, AnswerStepPictureModel entityPicture)
        {
            try
            {
                if (id != entityPicture.Id)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest,
                        "IDs do not match.");
                }

                var result = await PictureService.UpdateAsync(Mapper.Map<IAnswerStepPicture>(entityPicture));

                if (result == 1)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Updated.");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "PUT unsuccessful.");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
            }
        }

        // DELETE: api/AnswerStep/
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

        #region Models

        public class AnswerStepModel
        {
            public System.Guid Id { get; set; }
            public System.Guid QuestionId { get; set; }
            public short StepNumber { get; set; }
            public byte Points { get; set; }
            public string Text { get; set; }
            public string PictureUrl { get; set; }
        }

        public class AnswerStepPictureModel
        {
            public System.Guid Id { get; set; }
            public System.Guid AnswerStepId { get; set; }
            public byte[] Picture { get; set; }
        }

        #endregion Models
    }
}
