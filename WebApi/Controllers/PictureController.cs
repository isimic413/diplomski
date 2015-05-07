using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

using ExamPreparation.Service.Common;

namespace ExamPreparation.WebApi.Controllers
{
    [RoutePrefix("api/Picture")]
    public class PictureController : ApiController
    {
        #region Properties

        private IAnswerChoicePictureService AnswerChoicePictureService { get; set; }
        private IAnswerStepPictureService AnswerStepPictureService { get; set; }
        private IQuestionPictureService QuestionPictureService { get; set; }

        #endregion Properties

        #region Constructors

        public PictureController(IAnswerChoicePictureService answerChoicePictureService,
            IAnswerStepPictureService answerStepPictureService,
            IQuestionPictureService questionPictureService)
        {
            AnswerChoicePictureService = answerChoicePictureService;
            AnswerStepPictureService = answerStepPictureService;
            QuestionPictureService = questionPictureService;
        }

        #endregion Constructors

        #region Methods

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get(string url)
        {
            try
            {
                var id = new Guid(url.Split('/').Last());

                if (url.Contains("AnswerChoice"))
                {
                    return Ok(await AnswerChoicePictureService.GetAsync(id));
                }
                else if (url.Contains("AnswerStep"))
                {
                    return Ok(await AnswerStepPictureService.GetAsync(id));
                }
                else if (url.Contains("Question"))
                {
                    return Ok(await QuestionPictureService.GetAsync(id));
                }
                else
                {
                    return BadRequest("Invalid Url.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        #endregion Methods
    }
}
