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
    [RoutePrefix("api/AnswerChoice")]
    public class AnswerChoiceController : ApiController
    {
        #region Properties

        private IAnswerChoiceService Service { get; set; }
        private IAnswerChoicePictureService PictureService { get; set; }

        #endregion Properties

        #region Constructors

        public AnswerChoiceController(IAnswerChoiceService service, IAnswerChoicePictureService pictureService)
        {
            Service = service;
            PictureService = pictureService;
        }

        #endregion Constructors

        #region Methods

        // GET: api/AnswerChoice
        [HttpGet]
        public async Task<HttpResponseMessage> Get(string sortOrder = "", string sortDirection = "", 
            int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = await Service.GetAsync(new AnswerChoiceFilter(sortOrder, sortDirection, pageNumber, pageSize));
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        Mapper.Map<List<AnswerChoiceModel>>(result));
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

        // GET: api/AnswerChoice/5
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<HttpResponseMessage> Get(Guid id)
        {
            try
            {
                var result = await Service.GetAsync(id);
                if(result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, 
                        Mapper.Map<AnswerChoiceModel>(result));
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
        public async Task<HttpResponseMessage> GetChoices(Guid id)
        {
            try
            {
                var result = await Service.GetChoicesAsync(id);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        Mapper.Map<List<AnswerChoiceModel>>(result));
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
        [Route("Question/Solution/{id:guid}")] // "Question/{id:guid}/Solution" ?
        public async Task<HttpResponseMessage> GetCorrectAnswers(Guid id)
        {
            try
            {
                var result = await Service.GetCorrectAnswersAsync(id);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        Mapper.Map<List<AnswerChoiceModel>>(result));
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

        // POST: api/AnswerChoice
        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> Post(AnswerChoiceModel entity,
            AnswerChoicePictureModel entityPicture = null)
        {
            entity.Id = Guid.NewGuid();
            try
            {
                if (entityPicture != null)
                {
                    entityPicture.Id = Guid.NewGuid();
                    entityPicture.AnswerChoiceId = entity.Id;
                }

                var result = await Service.InsertAsync(Mapper.Map<IAnswerChoice>(entity), 
                    Mapper.Map<IAnswerChoicePicture>(entityPicture));

                if (result == 1)
                {
                    entity.PictureUrl = (entityPicture != null) ? "AnswerChoice/" + entityPicture.Id.ToString() : "";
                        //HttpContext.Current.Request.Url.AbsoluteUri + "/Picture/" + picture.Id.ToString() : "";

                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "POST unsuccessful.");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
            }
        }

        // PUT: api/AnswerChoice/5
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<HttpResponseMessage> Put(Guid id, AnswerChoiceModel entity,
            AnswerChoicePictureModel entityPicture = null)
        {
            try
            {
                if (id != entity.Id)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "IDs do not match.");
                }

                if (entityPicture != null)
                {
                    entityPicture.AnswerChoiceId = id;

                    if (entity.PictureUrl == null || entity.PictureUrl == "")
                    {
                        entityPicture.Id = Guid.NewGuid();
                    }
                    else
                    {
                        entityPicture.Id = new Guid(entity.PictureUrl.Split('/').Last());
                    }
                }
                
                var result = await Service.UpdateAsync(Mapper.Map<IAnswerChoice>(entity),
                    Mapper.Map<IAnswerChoicePicture>(entityPicture));

                if (result == 1)
                {
                    if (entity.PictureUrl == null || entity.PictureUrl == "")
                    {
                        entity.PictureUrl = (entityPicture != null) ? "AnswerChoice/" + entityPicture.Id.ToString() : "";
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
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString()); ;
            }
        }

        // PUT: api/AnswerChoice/Picture/5
        [HttpPut]
        [Route("Picture/{id:guid}")]
        public async Task<HttpResponseMessage> Put(Guid id, AnswerChoicePictureModel entity)
        {
            try
            {
                if (id != entity.Id)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest,
                        "IDs do not match.");
                }

                var result = await PictureService.UpdateAsync(Mapper.Map<IAnswerChoicePicture>(entity));

                if (result == 1)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Updated.");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "PUT unsuccessful.");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
            }
        }

        // DELETE: api/AnswerChoice/
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
            catch (InvalidOperationException e)
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

        public class AnswerChoiceModel
        {
            public System.Guid Id { get; set; }
            public System.Guid ProblemId { get; set; }
            public bool IsCorrect { get; set; }
            public string Text { get; set; }
            public string PictureUrl { get; set; }
        }

        public class AnswerChoicePictureModel
        {
            public System.Guid Id { get; set; }
            public System.Guid AnswerChoiceId { get; set; }
            public byte[] Picture { get; set; }
        }

        #endregion Model
    }
}
