using LMS.API.Permission;
using LMS.Core.Application;
using LMS.Core.Enum;
using LMS.Core.Models.RequestModels.QuizRequestModel;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.IServices;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static LMS.Core.Common.PermissionConstants;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private readonly IQuizService _quizService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserCourseService _userCourseService;

        public QuizzesController(IQuizService quizService, ICurrentUserService currentUserService,
            IUserCourseService userCourseService)
        {
            _quizService = quizService;
            _currentUserService = currentUserService;
            _userCourseService = userCourseService;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(QuizPreviewViewModel), 200)]
        [PermissionAuthorize(Course.CreateQuiz)]
        public async Task<IActionResult> CreateInTopic(QuizCreateRequestModel requestModel)
        {
            //teacher can access 
            await _userCourseService.CheckTopicAccessibility(requestModel.TopicId, _currentUserService.UserId, 
                ActionMethods.ManageQuiz, isTeacher: true);

            var createdQuiz = await _quizService.CreateQuizInTopic(requestModel);
            return Created(new Uri(Request.GetEncodedUrl() + "/" + createdQuiz.Id), createdQuiz);
        }

        [HttpGet("detail/{quizId}")]
        [ProducesResponseType(typeof(QuizPreviewViewModel), 200)]
        [PermissionAuthorize(Course.PreviewQuiz)]
        public async Task<IActionResult> GetQuizDetail(int quizId)
        {
            //teacher and manager can access 
            await _userCourseService.CheckTopicResourceAccessibility(quizId, _currentUserService.UserId,
               TopicResourceType.Quiz, ActionMethods.Nothing, isTeacher: true, isManager: true);

            var result = await _quizService.GetQuizDetail(quizId);
            return Ok(result);
        }

        [HttpPut("update/{quizId}")]
        [ProducesResponseType(typeof(QuizPreviewViewModel), 200)]
        [PermissionAuthorize(Course.UpdateQuiz)]
        public async Task<IActionResult> UpdateQuiz(int quizId, [FromBody] QuizUpdateRequestModel requestModel)
        {
            //teacher can access 
            await _userCourseService.CheckTopicResourceAccessibility(quizId, _currentUserService.UserId,
               TopicResourceType.Quiz, ActionMethods.ManageQuiz, isTeacher: true);

            var updatedQuiz = await _quizService.UpdateQuiz(quizId, requestModel);
            return Ok(updatedQuiz);
        }

        [HttpDelete("{quizId}")]
        [ProducesResponseType(200)]
        [PermissionAuthorize(Course.DeleteQuiz)]
        public async Task<IActionResult> DeleteQuiz(int quizId)
        {
            //teacher can access 
            await _userCourseService.CheckTopicResourceAccessibility(quizId, _currentUserService.UserId,
               TopicResourceType.Quiz, ActionMethods.ManageQuiz, isTeacher: true);

            await _quizService.DeleteQuiz(quizId);
            return Ok();
        }

        [HttpGet("view/{quizId}")]
        [ProducesResponseType(typeof(QuizInfoViewModel), 200)]
        [PermissionAuthorize(Course.AttemptAndReattemptQuiz)]
        public async Task<IActionResult> GetQuizInformation(int quizId)
        {
            await _userCourseService.CheckTopicResourceAccessibility(quizId, _currentUserService.UserId,
               TopicResourceType.Quiz, ActionMethods.Nothing, isStudent: true);

            var quizResult = await _quizService.GetQuizInformation(quizId);
            return Ok(quizResult);
        }

        [HttpGet("result/overall/{quizId}")]
        [ProducesResponseType(typeof(QuizReportViewModel), 200)]
        [PermissionAuthorize(Course.ViewSummaryOfQuizResults)]
        public async Task<IActionResult> ViewOverallResult(int quizId, [FromQuery] QuizReportRequestModel requestModel)
        {
            await _userCourseService.CheckTopicResourceAccessibility(quizId, _currentUserService.UserId,
               TopicResourceType.Quiz, ActionMethods.Nothing, isTeacher: true, isManager: true);

            var overallResult = await _quizService.ViewOverallQuizResult(quizId, requestModel);
            return Ok(overallResult);
        }

        [HttpGet("review/trainee/attempt/{quizAttemptId}")]
        [ProducesResponseType(typeof(StudentQuizResultViewModel), 200)]
        [PermissionAuthorize(Course.ViewDetailOfQuizResultOfStudent)]
        public async Task<IActionResult> ReviewStudentQuizAttempt(long quizAttemptId)
        {
            var quizResult = await _quizService.ReviewStudentQuizAttempt(quizAttemptId);
            return Ok(quizResult);
        }


        [HttpGet("restriction/topicList")]
        [ProducesResponseType(typeof(List<TopicWithRestrictionViewModel>), 200)]
        [PermissionAuthorize(Course.CreateQuiz, Course.UpdateQuiz, Course.PreviewQuiz)]
        public async Task<IActionResult> GetTopicListForQuizRestriction(int? topicId, int? quizId)
        {
            var result = await _quizService.GetTopicListForQuizRestriction(topicId, quizId);
            return Ok(result);
        }
    }
}
