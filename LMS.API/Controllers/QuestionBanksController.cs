using LMS.API.Permission;
using LMS.Infrastructure.IServices;
using LMS.Core.Models.RequestModels.QuestionBankRequestModel;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static LMS.Core.Common.PermissionConstants;
using LMS.Core.Models.ViewModels;
using System.Collections.Generic;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionBanksController : ControllerBase
    {
        private readonly IQuestionBankService _questionBankService;

        public QuestionBanksController(IQuestionBankService questionBankService)
        {
            _questionBankService = questionBankService;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(QuestionBankViewModel), 201)]
        [PermissionAuthorize(QuestionBank.CreateQuestionBank)]
        public async Task<IActionResult> Create(QuestionBankCreateRequestModel questionBankCreateRequestModel)
        {
            var createdQuestionBank = await _questionBankService.CreateQuestionBank(questionBankCreateRequestModel);
            return Created(new Uri(Request.GetEncodedUrl() + "/" + createdQuestionBank.Id), createdQuestionBank);
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(List<QuestionBankBySubjectViewModel>), 200)]
        [PermissionAuthorize(Question.ViewQuestionsList)]
        public async Task<IActionResult> GetQuestionBankBySubject([FromQuery] QuestionBankRequestModel questionBankRequestModel)
        {
            var result = await _questionBankService.GetQuestionBankBySubject(questionBankRequestModel);
            return Ok(result);
        }

        [HttpGet("detail/{questionBankId}")]
        [ProducesResponseType(typeof(QuestionBankViewModel), 200)]
        [PermissionAuthorize(Question.ViewQuestionsList)]
        public async Task<IActionResult> GetQuestionBankDetail(int questionBankId)
        {
            var result = await _questionBankService.GetQuestionBankDetail(questionBankId);
            return Ok(result);
        }

        [HttpPut("update/{questionBankId}")]
        [ProducesResponseType(typeof(QuestionBankViewModel), 200)]
        [PermissionAuthorize(QuestionBank.UpdateQuestionBank)]
        public async Task<IActionResult> UpdateQuestionBank(int questionBankId, [FromBody] QuestionBankUpdateRequestModel questionBankUpdateRequestModel)
        {
            var result = await _questionBankService.UpdateQuestionBank(questionBankId, questionBankUpdateRequestModel);
            return Ok(result);
        }

        //[HttpPut("update/status/{questionBankId}")]
        //[ProducesResponseType(typeof(QuestionBankViewModel), 200)]
        //[PermissionAuthorize(QuestionBank.UpdateQuestionBank)]
        //public async Task<IActionResult> UpdateQuestionBankStatus(int questionBankId, bool isActive)
        //{
        //    var result = await _questionBankService.UpdateQuestionBankStatus(questionBankId, isActive);
        //    return Ok(result);
        //}

        [HttpDelete("{questionBankId}")]
        [ProducesResponseType(200)]
        [PermissionAuthorize(QuestionBank.DeleteQuestionBank)]
        public async Task<IActionResult> DeleteQuestionBank(int questionBankId)
        {
            var result = await _questionBankService.DeleteQuestionBank(questionBankId);
            return Ok(result);
        }
    }
}
