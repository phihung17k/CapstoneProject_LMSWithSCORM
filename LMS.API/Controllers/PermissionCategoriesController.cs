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
    public class PermissionCategoriesController : ControllerBase
    {
        private readonly IPermissionCategoryService _permissionCategoryService;

        public PermissionCategoriesController(IPermissionCategoryService permissionCategoryService)
        {
            _permissionCategoryService = permissionCategoryService;
        }
        [HttpGet]
        [ProducesResponseType(typeof(PermissionCategoryViewModel), 200)]
        [PermissionAuthorize(Role.ViewDetailOfRole, Role.CreateRole, Role.UpdateRole)]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = await _permissionCategoryService.GetAllPermissionCategories();
            return Ok(categories);
        }
    }
}