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

        #region GET

        // GET: api/AnswerChoice
        [HttpGet]
        public async Task<IHttpActionResult> Get(string sortOrder = "", string sortDirection = "", int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = await Service.GetAsync(new AnswerChoiceFilter(sortOrder, sortDirection, pageNumber, pageSize));
                if (result != null)
                {
                    return Ok(Mapper.Map<List<AnswerChoiceModel>>(result));
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

        // GET: api/AnswerChoice/5
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            try
            {
                var result = await Service.GetAsync(id);
                if(result != null)
                {
                    return Ok(Mapper.Map<AnswerChoiceModel>(result));
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

        [HttpGet]
        [Route("Question/{id:guid}")]
        public async Task<IHttpActionResult> GetChoices(Guid id)
        {
            try
            {
                var result = await Service.GetChoicesAsync(id);
                if (result != null)
                {
                    return Ok(Mapper.Map<AnswerChoiceModel>(result));
                }
                else return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpGet]
        [Route("Question/Solution/{id:guid}")] // "Question/{id:guid}/Solution" ?
        public async Task<IHttpActionResult> GetCorrectAnswers(Guid id)
        {
            try
            {
                var result = await Service.GetCorrectAnswersAsync(id);
                if (result != null)
                {
                    return Ok(Mapper.Map<AnswerChoiceModel>(result));
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

        // POST: api/AnswerChoice
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post(AnswerChoiceModel choice, AnswerChoicePictureModel picture = null)
        {
            choice.Id = Guid.NewGuid();
            try
            {
                var mappedChoice = Mapper.Map<IAnswerChoice>(choice);

                if (picture != null)
                {
                    picture.Id = Guid.NewGuid();
                    picture.AnswerChoiceId = choice.Id;
                    mappedChoice.HasPicture = true;
                }
                else
                {
                    mappedChoice.HasPicture = false;
                }
                
                var result = await Service.InsertAsync(mappedChoice, Mapper.Map<IAnswerChoicePicture>(picture));

                if (result == 1)
                {
                    choice.PictureUrl = (picture != null) ? "AnswerChoice/" + picture.Id.ToString() : "";
                        //HttpContext.Current.Request.Url.AbsoluteUri + "/Picture/" + picture.Id.ToString() : "";

                    return Ok(choice);
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

        // PUT: api/AnswerChoice/5
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Put(Guid id, AnswerChoiceModel choice, AnswerChoicePictureModel picture = null)
        {
            try
            {
                if (id != choice.Id)
                {
                    return BadRequest("IDs do not match.");
                }

                var mappedChoice = Mapper.Map<IAnswerChoice>(choice);

                if (choice.PictureUrl == null || choice.PictureUrl == "")
                {
                    if (picture != null)
                    {
                        picture.AnswerChoiceId = id;
                        picture.Id = Guid.NewGuid();
                        mappedChoice.HasPicture = true;
                    }
                    else
                    {
                        mappedChoice.HasPicture = false;
                    }
                }
                else
                {
                    mappedChoice.HasPicture = true;
                }

                var result = await Service.UpdateAsync(mappedChoice, Mapper.Map<IAnswerChoicePicture>(picture));

                if (result == 1)
                {
                    if (choice.PictureUrl == null || choice.PictureUrl == "")
                    {
                        choice.PictureUrl = (picture != null) ? "AnswerChoice/" + picture.Id.ToString() : "";
                        //HttpContext.Current.Request.Url.AbsoluteUri + "/Picture/" + picture.Id.ToString() : "";
                    }

                    return Ok(choice);
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

        // PUT: api/AnswerChoice/Picture/5
        [HttpPut]
        [Route("Picture/{id:guid}")]
        public async Task<IHttpActionResult> Put(Guid id, AnswerChoicePictureModel picture)
        {
            try
            {
                if (id != picture.Id)
                {
                    return BadRequest("IDs do not match.");
                }

                var result = await Service.UpdatePictureAsync(Mapper.Map<IAnswerChoicePicture>(picture));

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

        // DELETE: api/AnswerChoice/
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
            catch (InvalidOperationException e)
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

        public class AnswerChoiceModel
        {
            public System.Guid Id { get; set; }
            public System.Guid ProblemId { get; set; }
            public bool IsCorrect { get; set; }
            public string Text { get; set; }
            public bool HasPicture { get; set; }
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
