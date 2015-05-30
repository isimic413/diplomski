using System;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public async Task<HttpResponseMessage> Get(string url)
        {
            try
            {
                var id = new Guid(url.Split('/').Last());

                if (url.Contains("AnswerChoice"))
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        await AnswerChoicePictureService.GetAsync(id));
                }
                else if (url.Contains("AnswerStep"))
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        await AnswerStepPictureService.GetAsync(id));
                }
                else if (url.Contains("Question"))
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        await QuestionPictureService.GetAsync(id));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Url.");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
            }
        }

        #endregion Methods
    }
}
