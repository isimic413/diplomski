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
    [RoutePrefix("api/AnswerStep")]
    public class AnswerStepController : ApiController
    {
        #region Properties

        private IAnswerStepService Service { get; set; }

        #endregion Properties

        #region Constructors

        public AnswerStepController(IAnswerStepService service)
        {
            Service = service;
        }

        #endregion Constructors

        #region Methods

        #region GET
        // GET: api/AnswerStep
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get(string sortOrder = "", string sortDirection = "", int pageSize = 0, int pageNumber = 0)
        {
            try
            {
                AnswerStepFilter filter = new AnswerStepFilter(sortOrder, sortDirection, pageNumber, pageSize);
                var result = await Service.GetAsync(filter);
                if (result != null)
                {
                    return Ok(Mapper.Map<List<AnswerStepModel>>(result));
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

        // GET: api/AnswerStep
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            try
            {
                var result = await Service.GetAsync(id);
                if (result != null)
                {
                    return Ok(Mapper.Map<AnswerStepModel>(result));
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
        [Route("question/{id:guid}")]
        public async Task<IHttpActionResult> GetSteps(Guid id)
        {
            try
            {
                var result = await Service.GetStepsAsync(id);
                if (result != null)
                {
                    return Ok(Mapper.Map<List<AnswerStepModel>>(result));
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

        #endregion GET

        #region POST

        // POST: api/AnswerStep
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post(AnswerStepModel step, AnswerStepPictureModel picture = null)
        {
            step.Id = Guid.NewGuid();
            try
            {
                if (step.StepNumber < 1)
                {
                    return BadRequest("StepNumber cannot be less than 1.");
                }

                var mappedStep = Mapper.Map<IAnswerStep>(step);

                if (picture != null)
                {
                    picture.Id = Guid.NewGuid();
                    picture.AnswerStepId = step.Id;
                    mappedStep.HasPicture = true;
                }
                else
                {
                    mappedStep.HasPicture = false;
                }

                var result = await Service.InsertAsync(mappedStep, Mapper.Map<IAnswerStepPicture>(picture));

                if (result == 1)
                {
                    step.PictureUrl = (picture != null) ? "AnswerStep/" + picture.Id.ToString() : "";
                    //HttpContext.Current.Request.Url.AbsoluteUri + "/Picture/" + picture.Id.ToString() : "";

                    return Ok(step);
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

        // PUT: api/AnswerStep/5
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Put(Guid id, AnswerStepModel step, AnswerStepPictureModel picture = null)
        {
            try
            {
                if (id != step.Id)
                {
                    return BadRequest("IDs do not match.");
                }

                var mappedStep = Mapper.Map<IAnswerStep>(step);

                if (step.PictureUrl == null || step.PictureUrl == "")
                {
                    if (picture != null)
                    {
                        picture.AnswerStepId = id;
                        picture.Id = Guid.NewGuid();
                        mappedStep.HasPicture = true;
                    }
                    else
                    {
                        mappedStep.HasPicture = false;
                    }
                }
                else
                {
                    mappedStep.HasPicture = true;
                }

                var result = await Service.UpdateAsync(mappedStep, Mapper.Map<IAnswerStepPicture>(picture));

                if (result == 1)
                {
                    if (step.PictureUrl == null || step.PictureUrl == "")
                    {
                        step.PictureUrl = (picture != null) ? "AnswerStep/" + picture.Id.ToString() : "";
                        //HttpContext.Current.Request.Url.AbsoluteUri + "/Picture/" + picture.Id.ToString() : "";
                    }

                    return Ok(step);
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

        // PUT: api/AnswerStep/Picture/5
        [HttpPut]
        [Route("Picture/{id:guid}")]
        public async Task<IHttpActionResult> Put(Guid id, AnswerStepPictureModel picture)
        {
            try
            {
                if (id != picture.Id)
                {
                    return BadRequest("IDs do not match.");
                }

                var result = await Service.UpdatePictureAsync(Mapper.Map<IAnswerStepPicture>(picture));

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

        // DELETE: api/AnswerStep/
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            try
            {
                if (await Service.DeleteAsync(id) == 1)
                {
                    return Ok("Step deleted.");
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

        #region Model

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

        #endregion Model
    }
}
