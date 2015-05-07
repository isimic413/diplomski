using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
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

        #endregion Properties

        #region Constructors

        public QuestionController(IQuestionService service)
        {
            Service = service;
        }

        #endregion Constructors

        #region Methods

        #region GET

        // GET: api/Problem
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get(string sortOrder = "", string sortDirection = "", int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = await Service.GetAsync(new QuestionFilter(sortOrder, sortDirection, pageNumber, pageSize));
                if (result != null)
                {
                    return Ok(Mapper.Map<List<QuestionModel>>(result));
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

        // GET: api/Problem/5
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            try
            {
                var result = await Service.GetAsync(id);
                if (result != null)
                {
                    return Ok(Mapper.Map<QuestionModel>(result));
                }
                else return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        
        [HttpGet]
        [Route("TestingArea/{id:guid}")]
        public async Task<IHttpActionResult> GetByTestingArea(Guid id)
        {
            try
            {
                var result = await Service.GetByTestingAreaIdAsync(id, new QuestionFilter("TestingAreaId",0,0));
                if (result != null)
                {
                    return Ok(Mapper.Map<List<QuestionModel>>(result));
                }
                else return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpGet]
        [Route("QuestionType/{id:guid}")]
        public async Task<IHttpActionResult> GetByType(Guid id)
        {
            try
            {
                var result = await Service.GetByTypeIdAsync(id, new QuestionFilter("QuestionTypeId",0,0));
                if (result != null)
                {
                    return Ok(Mapper.Map<List<QuestionModel>>(result));
                }
                else return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        #endregion GET

        #region POST

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post(
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
                    return new ExceptionResult(new ArgumentException("Points < 1"), this);
                }

                var mappedQuestion = Mapper.Map<IQuestion>(question);

                // set picture
                if (questionPicture != null)
                {
                    mappedQuestion.HasPicture = true;
                    questionPicture.Id = Guid.NewGuid();
                    questionPicture.QuestionId = mappedQuestion.Id;
                }
                else
                {
                    mappedQuestion.HasPicture = false;
                }

                // set choices
                List<IAnswerChoice> mappedChoices = new List<IAnswerChoice>();
                List<IAnswerChoicePicture> mappedChoicePictures = new List<IAnswerChoicePicture>();
                foreach (var choice in choices)
                {
                    var newChoice = Mapper.Map<IAnswerChoice>(choice);
                    newChoice.Id = Guid.NewGuid();
                    var pictures = choicePictures.FindAll(item => item.AnswerChoiceId == choice.Id);
                    if (pictures == null || pictures.Count == 0)
                    {
                        newChoice.HasPicture = false;
                    }
                    else if (pictures.Count == 1)
                    {
                        newChoice.HasPicture = true;
                        pictures.First().AnswerChoiceId = newChoice.Id;
                        pictures.First().Id = Guid.NewGuid();
                        mappedChoicePictures.Add(Mapper.Map<IAnswerChoicePicture>(pictures.First()));
                    }
                    else
                    {
                        return new ExceptionResult(new ArgumentException("List<IAnswerChoicePicture>"), this);
                    }
                    mappedChoices.Add(newChoice);

                    if(choicePictures == null)
                    {
                        mappedChoicePictures = null;
                    }
                }

                // set steps
                List<IAnswerStep> mappedSteps = null;
                List<IAnswerStepPicture> mappedStepPictures = null;
                if (steps == null)
                {
                    mappedQuestion.HasSteps = false;
                }
                else
                {
                    mappedQuestion.HasSteps = true;
                    mappedSteps = new List<IAnswerStep>();
                    mappedStepPictures = new List<IAnswerStepPicture>();
                    foreach (var step in steps)
                    {
                        var newStep = Mapper.Map<IAnswerStep>(step);
                        newStep.Id = Guid.NewGuid();
                        var pictures = stepPictures.FindAll(item => item.AnswerStepId == step.Id);
                        if (pictures == null || pictures.Count == 0)
                        {
                            newStep.HasPicture = false;
                        }
                        else if (pictures.Count == 1)
                        {
                            newStep.HasPicture = true;
                            pictures.First().AnswerStepId = newStep.Id;
                            pictures.First().Id = Guid.NewGuid();
                            mappedStepPictures.Add(Mapper.Map<IAnswerStepPicture>(pictures.First()));
                        }
                        else
                        {
                            return new ExceptionResult(new ArgumentException("List<IAnswerStepPicture>"), this);
                        }
                        mappedSteps.Add(newStep);
                    }
                }

                int result = await Service.InsertAsync(mappedQuestion, mappedChoices,
                    Mapper.Map<IQuestionPicture>(questionPicture), mappedChoicePictures,
                    mappedSteps, mappedStepPictures);

                if (result == 1)
                {
                    question.PictureUrl = (questionPicture != null) ? "Question/" + questionPicture.Id.ToString() : "";
                    //HttpContext.Current.Request.Url.AbsoluteUri + "/Picture/" + questionPicture.Id.ToString() : "";

                    return Ok(question);
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

        // PUT: api/Problem/5
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Put(Guid id,
            QuestionModel question,
            QuestionPictureModel questionPicture = null,
            List<AnswerStepController.AnswerStepModel> steps = null,
            List<AnswerStepController.AnswerStepPictureModel> stepPictures = null
            )
        {
            question.Id = Guid.NewGuid();
            try
            {
                if (question.Points < 1)
                {
                    return new ExceptionResult(new ArgumentException("Points < 1"), this);
                }

                var mappedQuestion = Mapper.Map<IQuestion>(question);

                // set picture
                if (questionPicture != null)
                {
                    mappedQuestion.HasPicture = true;
                    questionPicture.Id = Guid.NewGuid();
                    questionPicture.QuestionId = mappedQuestion.Id;
                }
                else
                {
                    mappedQuestion.HasPicture = false;
                }

                // set steps
                List<IAnswerStep> mappedSteps = null;
                List<IAnswerStepPicture> mappedStepPictures = null;
                if (steps == null)
                {
                    mappedQuestion.HasSteps = false;
                }
                else
                {
                    mappedQuestion.HasSteps = true;
                    mappedSteps = new List<IAnswerStep>();
                    mappedStepPictures = new List<IAnswerStepPicture>();
                    foreach (var step in steps)
                    {
                        var newStep = Mapper.Map<IAnswerStep>(step);
                        newStep.Id = Guid.NewGuid();
                        var pictures = stepPictures.FindAll(item => item.AnswerStepId == step.Id);
                        if (pictures == null || pictures.Count == 0)
                        {
                            newStep.HasPicture = false;
                        }
                        else if (pictures.Count == 1)
                        {
                            newStep.HasPicture = true;
                            pictures.First().AnswerStepId = newStep.Id;
                            pictures.First().Id = Guid.NewGuid();
                            mappedStepPictures.Add(Mapper.Map<IAnswerStepPicture>(pictures.First()));
                        }
                        else
                        {
                            return new ExceptionResult(new ArgumentException("List<IAnswerStepPicture>"), this);
                        }
                        mappedSteps.Add(newStep);
                    }
                }

                int result = await Service.UpdateAsync(mappedQuestion,
                    Mapper.Map<IQuestionPicture>(questionPicture),
                    mappedSteps, mappedStepPictures);

                if (result == 1)
                {
                    question.PictureUrl = (questionPicture != null) ? "Question/" + questionPicture.Id.ToString() : "";
                    //HttpContext.Current.Request.Url.AbsoluteUri + "/Picture/" + questionPicture.Id.ToString() : "";

                    return Ok(question);
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

        // PUT: api/Question/Picture/5
        [HttpPut]
        [Route("Picture/{id:guid}")]
        public async Task<IHttpActionResult> Put(Guid id, QuestionPictureModel picture)
        {
            try
            {
                if (id != picture.Id)
                {
                    return BadRequest("IDs do not match.");
                }

                var result = await Service.UpdatePictureAsync(Mapper.Map<IQuestionPicture>(picture));

                if (result == 1)
                {
                    return Ok("Updated.");
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

        // DELETE: api/Problem/
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

        #region Models

        public class QuestionModel
        {
            public System.Guid Id { get; set; }
            public System.Guid TestingAreaId { get; set; }
            public System.Guid QuestionTypeId { get; set; }
            public string Text { get; set; }
            public byte Points { get; set; }
            public string PictureUrl { get; set; }
            public bool HasSteps { get; set; }
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