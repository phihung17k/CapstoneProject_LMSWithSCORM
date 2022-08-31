using LMS.API.Permission;
using LMS.Core.Models.RequestModels.SectionRequestModel;
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
    public class SectionController : ControllerBase
    {
        private readonly ISectionService _service;

        public SectionController(ISectionService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SectionViewModel), 201)]
        [PermissionAuthorize(Subject.CreateSection)]
        public async Task<IActionResult> Create(SectionCreateRequestModel requestModel)
        {
            var result = await _service.CreateSection(requestModel);
            return Created(new Uri(Request.GetEncodedUrl() + "/" + result.Id), result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SectionViewModel), 200)]
        [PermissionAuthorize(Subject.UpdateSection)]
        public async Task<IActionResult> Update(int id, SectionUpdateRequestModel requestModel)
        {
            var result = await _service.UpdateSection(id, requestModel);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [PermissionAuthorize(Subject.DeleteSection)]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}
