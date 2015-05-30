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
using System.Web.Http.Results;


namespace ExamPreparation.WebApi.Controllers
{
    [RoutePrefix("api/Question")]
    public class QuestionController : ApiController
    {
        #region Properties

        private IQuestionService Service { get; set; }
        private IQuestionPictureService PictureService { get; set; }

        #endregion Properties

        #region Constructors

        public QuestionController(IQuestionService service, IQuestionPictureService pictureService)
        {
            Service = service;
            PictureService = pictureService;
        }

        #endregion Constructors

        #region Methods

        // GET: api/Question
        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> Get(string sortOrder = "", string sortDirection = "", 
            int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = await Service.GetAsync(new QuestionFilter(sortOrder, sortDirection, pageNumber, pageSize));
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        Mapper.Map<List<QuestionModel>>(result));
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

        // GET: api/Question/5
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
                        Mapper.Map<QuestionModel>(result));
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
        public async Task<HttpResponseMessage> GetByTestingArea(Guid id)
        {
            try
            {
                var result = await Service.GetByTestingAreaIdAsync(id, new QuestionFilter("TestingAreaId", 0, 0));
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        Mapper.Map<List<QuestionModel>>(result));
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
        [Route("QuestionType/{id:guid}")]
        public async Task<HttpResponseMessage> GetByType(Guid id)
        {
            try
            {
                var result = await Service.GetByTypeIdAsync(id, new QuestionFilter("QuestionTypeId", 0, 0));
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        Mapper.Map<List<QuestionModel>>(result));
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

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> Post(
            QuestionModel question, 
            List<AnswerChoiceController.AnswerChoiceModel> choices,
            QuestionPictureModel questionPicture = null,
            List<AnswerChoiceController.AnswerChoicePictureModel> choicePictures = null,
            List<AnswerStepController.AnswerStepModel> steps = null,
            List<AnswerStepController.AnswerStepPictureModel> stepPictures = null
            )
        {
            question.Id = Guid.NewGuid();
            try
            {
                if (question.Points < 1)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest,"Points < 1");
                }

                // set picture
                if (questionPicture != null)
                {
                    questionPicture.Id = Guid.NewGuid();
                    questionPicture.QuestionId = question.Id;
                }

                // set choices
                var chPictures = new List<IAnswerChoicePicture>();
                foreach (var choice in choices)
                {
                    Guid newId = Guid.NewGuid();
                    var tempPictures = choicePictures.FindAll(item => item.AnswerChoiceId == choice.Id);
                    
                    if (tempPictures != null && tempPictures.Count > 0)
                    {
                        if (tempPictures.Count > 1)
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest, "More than one picture for one choice.");
                        }
                        tempPictures.First().Id = Guid.NewGuid();
                        tempPictures.First().AnswerChoiceId = newId;

                        chPictures.Add(Mapper.Map<IAnswerChoicePicture>(tempPictures.First()));
                    }
                    choice.Id = newId;
                }

                // set steps
                var stPictures = new List<IAnswerStepPicture>();
                foreach (var step in steps)
                {
                    Guid newId = Guid.NewGuid();
                    var tempPictures = stepPictures.FindAll(item => item.AnswerStepId == step.Id);

                    if (tempPictures != null && tempPictures.Count > 0)
                    {
                        if (tempPictures.Count > 1)
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest, "More than one picture for one step.");
                        }
                        tempPictures.First().Id = Guid.NewGuid();
                        tempPictures.First().AnswerStepId = newId;

                        stPictures.Add(Mapper.Map<IAnswerStepPicture>(tempPictures.First()));
                    }
                    step.Id = newId;
                }

                var result = await Service.InsertAsync(
                    Mapper.Map<IQuestion>(question),
                    Mapper.Map<List<IAnswerChoice>>(choices),
                    Mapper.Map<IQuestionPicture>(questionPicture),
                    chPictures,
                    Mapper.Map<List<IAnswerStep>>(steps),
                    stPictures
                    );

                if (result == 1)
                {
                    question.PictureUrl = (questionPicture != null) ? "Question/" + questionPicture.Id.ToString() : "";
                    //HttpContext.Current.Request.Url.AbsoluteUri + "/Picture/" + questionPicture.Id.ToString() : "";

                    return Request.CreateResponse(HttpStatusCode.OK, question);
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

        // PUT: api/Question/5
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<HttpResponseMessage> Put(Guid id, QuestionModel entity)
        {
            try
            {
                if (id != entity.Id)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "IDs do not match.");
                }

                var result = await Service.UpdateAsync(Mapper.Map<IQuestion>(entity));

                if (result == 1)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
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

        // PUT: api/Question/Picture/5
        [HttpPut]
        [Route("Picture/{id:guid}")]
        public async Task<HttpResponseMessage> Put(Guid id, QuestionPictureModel entity)
        {
            try
            {
                if (id != entity.Id)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, 
                        "IDs do not match.");
                }

                var result = await PictureService.UpdateAsync(Mapper.Map<IQuestionPicture>(entity));

                if (result == 1)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Updated.");
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

        // DELETE: api/Question/
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

        public class QuestionModel
        {
            public System.Guid Id { get; set; }
            public System.Guid TestingAreaId { get; set; }
            public System.Guid QuestionTypeId { get; set; }
            public string Text { get; set; }
            public byte Points { get; set; }
            public string PictureUrl { get; set; }
        }

        public class QuestionPictureModel
        {
            public System.Guid Id { get; set; }
            public System.Guid QuestionId { get; set; }
            public byte[] Picture { get; set; }
        }

        #endregion Models
    }
}