using System;
using System.Threading.Tasks;
using LMS.API.Permission;
using LMS.Infrastructure.IServices;
using LMS.Core.Models.RequestModels.QuestionRequestModel;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using static LMS.Core.Common.PermissionConstants;
using LMS.Core.Models.ViewModels;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(PagingViewModel<QuestionViewModelWithoutOptions>), 200)]
        [PermissionAuthorize(Question.ViewQuestionsList)]
        public async Task<IActionResult> SearchQuestion([FromQuery] QuestionPagingRequestModel questionPagingRequestModel)
        {
            var questions = await _questionService.Search(questionPagingRequestModel);
            return Ok(questions);
        }

        [HttpPost]
        [ProducesResponseType(typeof(QuestionViewModel), 201)]
        [PermissionAuthorize(Question.CreateQuestion)]
        public async Task<IActionResult> Create(QuestionCreateRequestModel questionCreateRequestModel)
        {
            var createdQuestion = await _questionService.CreateQuestion(questionCreateRequestModel);
            return Created(new Uri(Request.GetEncodedUrl() + "/" + createdQuestion.Id), createdQuestion);
        }

        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(QuestionViewModel), 200)]
        [PermissionAuthorize(Question.UpdateQuestion)]
        public async Task<IActionResult> UpdateQuestion(int id, [FromBody] QuestionUpdateRequestModel requestModel)
        {
            var result = await _questionService.UpdateQuestion(id, requestModel);
            return Ok(result);
        }

        [HttpGet("detail/{id}")]
        [ProducesResponseType(typeof(QuestionViewModel), 200)]
        [PermissionAuthorize(Question.ViewDetailOfQuestion)]
        public async Task<IActionResult> GetDetail(int id)
        {
            var question = await _questionService.GetDetail(id);
            return Ok(question);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [PermissionAuthorize(Question.DeleteQuestion)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _questionService.Delete(id);
            return Ok();
        }

        [HttpPut("update/status/{id}")]
        [ProducesResponseType(typeof(QuestionViewModelWithoutOptions), 200)]
        [PermissionAuthorize(Question.UpdateQuestion)]
        public async Task<IActionResult> UpdateStatus(int id, bool isActive)
        {
            var result = await _questionService.UpdateStatus(id, isActive);
            return Ok(result);
        }
    }
}