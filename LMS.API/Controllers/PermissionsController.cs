using System.Linq;
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
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IQueryable<PermissionViewModel>), 200)]
        [PermissionAuthorize(Role.ViewDetailOfRole, Role.CreateRole, Role.UpdateRole)]
        public async Task<IActionResult> GetPermissionsByCategory(string category)
        {
            var entities = await _permissionService.GetPermissionByCategory(category);
            return Ok(entities);
        }
    }
}