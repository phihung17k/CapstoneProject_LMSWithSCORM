using LMS.API.Permission;
using LMS.Core.Models.RequestModels.QuizAttemptRequestModel;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.IServices;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static LMS.Core.Common.PermissionConstants;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizAttemptsController : ControllerBase
    {
        private readonly IQuizAttemptService _quizAttemptService;

        public QuizAttemptsController(IQuizAttemptService quizAttemptService)
        {
            _quizAttemptService = quizAttemptService;
        }

        [HttpPost("attempt")]
        [ProducesResponseType(typeof(QuizAttemptViewModel), 201)]
        [PermissionAuthorize(Course.AttemptAndReattemptQuiz)]
        public async Task<IActionResult> CreateQuizAttempt(QuizAttemptRequestModel requestModel)
        {
            var quizAttempt = await _quizAttemptService.CreateQuizAttempt(requestModel);
            return Created(new Uri(Request.GetEncodedUrl() + "/" + quizAttempt.Id), quizAttempt);
        }

        //[HttpGet("detail/attempt/{quizAttemptId}")]
        //[ProducesResponseType(typeof(QuizAttemptViewModel), 200)]
        //[PermissionAuthorize(Course.AttemptAndReattemptQuiz)]
        //public async Task<IActionResult> GetAttemptDetail(long quizAttemptId)
        //{
        //    var quizResult = await _quizAttemptService.GetQuizAttemptDetail(quizAttemptId);
        //    return Ok(quizResult);
        //}

        [HttpPut("submit/{quizAttemptId}")]
        [ProducesResponseType(typeof(SubmitQuizViewModel), 200)]
        [PermissionAuthorize(Course.AttemptAndReattemptQuiz)]
        public async Task<IActionResult> Submit(long quizAttemptId, QuizSubmitRequestModel requestModel)
        {
            var quizResult = await _quizAttemptService.UpdateQuizAttemptResult(quizAttemptId, requestModel);
            return Ok(quizResult);
        }

        [HttpGet("review/own/{quizAttemptId}")]
        [ProducesResponseType(typeof(QuizResultViewModel), 200)]
        [PermissionAuthorize(Course.ReviewYourOwnAttempts)]
        public async Task<IActionResult> ReviewQuizAttempt(long quizAttemptId)
        {
            var quizResult = await _quizAttemptService.ReviewOwnQuizAttempt(quizAttemptId);
            return Ok(quizResult);
        }
    }
}
