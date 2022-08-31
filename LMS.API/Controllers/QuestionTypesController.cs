using System.Threading.Tasks;
using LMS.API.Permission;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.IServices;
using Microsoft.AspNetCore.Mvc;
using static LMS.Core.Common.PermissionConstants;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionTypesController : ControllerBase
    {
        private readonly IQuestionTypeService _questionTypeService;

        public QuestionTypesController(IQuestionTypeService questionTypeService)
        {
            _questionTypeService = questionTypeService;
        }
        [HttpGet]
        [ProducesResponseType(typeof(QuestionTypeViewModel), 200)]
        [PermissionAuthorize(Question.CreateQuestion)]
        public async Task<IActionResult> GetAllAsync()
        {
            var types = await _questionTypeService.GetAllQuestionTypes();
            return Ok(types);
        }
    }
}