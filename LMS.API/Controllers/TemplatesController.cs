using System;
using System.Threading.Tasks;
using LMS.API.Permission;
using LMS.Infrastructure.IServices;
using LMS.Core.Models.RequestModels.TemplateRequestModel;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using static LMS.Core.Common.PermissionConstants;
using LMS.Core.Models.ViewModels;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        private readonly ITemplateService _templateService;

        public TemplatesController(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(TemplateViewModel), 201)]
        [PermissionAuthorize(SurveyTemplate.CreateSurveyTemplate)]
        public async Task<IActionResult> Create(TemplateRequestModel templateCreateRequestModel)
        {
            var createdTemplate = await _templateService.CreateTemplate(templateCreateRequestModel);
            return Created(new Uri(Request.GetEncodedUrl() + "/" + createdTemplate.Id), createdTemplate);
        }

        [HttpGet("detail/{id}")]
        [ProducesResponseType(typeof(TemplateViewModel), 200)]
        [PermissionAuthorize(SurveyTemplate.ViewDetailOfSurveyTemplate)]
        public async Task<IActionResult> Get(int id)
        {
            var template = await _templateService.Get(id);
            return Ok(template);
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(PagingViewModel<TemplateViewModelWithoutQuestions>), 200)]
        [PermissionAuthorize(SurveyTemplate.ViewSurveyTemplatesList)]
        public async Task<IActionResult> SearchTemplate([FromQuery] TemplatePagingRequestModel templatePagingRequestModel)
        {
            var templates = await _templateService.Search(templatePagingRequestModel);
            return Ok(templates);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [PermissionAuthorize(SurveyTemplate.DeleteSurveyTemplate)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _templateService.Delete(id);
            return NoContent();
        }

        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(TemplateViewModel), 200)]
        [PermissionAuthorize(SurveyTemplate.UpdateSurveyTemplate)]
        public async Task<IActionResult> Update(int id, TemplateRequestModel templateUpdateRequestModel)
        {
            var updatedTemplate = await _templateService.Update(id, templateUpdateRequestModel);
            return Ok(updatedTemplate);
        }

        [HttpPut("update/status/{id}")]
        [ProducesResponseType(typeof(TemplateViewModelWithoutQuestions), 200)]
        [PermissionAuthorize(SurveyTemplate.UpdateSurveyTemplate)]
        public async Task<IActionResult> UpdateStatus(int id, bool isActive)
        {
            var updatedTemplate = await _templateService.UpdateStatus(id, isActive);
            return Ok(updatedTemplate);
        }
    }
}