using System;
using System.Threading.Tasks;
using LMS.API.Permission;
using LMS.Infrastructure.IServices;
using LMS.Core.Models.RequestModels.RoleRequestModel;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using static LMS.Core.Common.PermissionConstants;
using LMS.Core.Models.ViewModels;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(PagingViewModel<RoleViewModelWithoutPermission>), 200)]
        [PermissionAuthorize(Role.ViewRolesList)]
        public async Task<IActionResult> SearchRole([FromQuery] RolePagingRequestModel rolePagingRequestModel)
        {
            var roles = await _roleService.Search(rolePagingRequestModel);
            return Ok(roles);
        }

        [HttpGet("detail/{id}")]
        [ProducesResponseType(typeof(RoleViewModel), 200)]
        [PermissionAuthorize(Role.ViewDetailOfRole)]
        public async Task<IActionResult> Get(int id)
        {
            var role = await _roleService.Get(id);
            return Ok(role);
        }

        [HttpPost]
        [ProducesResponseType(typeof(RoleViewModel), 201)]
        [PermissionAuthorize(Role.CreateRole)]
        public async Task<IActionResult> Create(RoleCreateRequestModel roleRequestModel)
        {
            var createdRole = await _roleService.CreateRole(roleRequestModel);
            return Created(new Uri(Request.GetEncodedUrl() + "/" + createdRole.Id), createdRole);
        }
        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(RoleViewModel), 200)]
        [PermissionAuthorize(Role.UpdateRole)]
        public async Task<IActionResult> Update(int id, RoleUpdateRequestModel roleRequestModel)
        {
            var updatedRole = await _roleService.Update(id, roleRequestModel);
            return Ok(updatedRole);
        }
        [HttpPut("update/status/{id}")]
        [ProducesResponseType(typeof(RoleViewModelWithoutPermission), 200)]
        [PermissionAuthorize(Role.UpdateRole)]
        public async Task<IActionResult> UpdateStatus(int id, bool isActive)
        {
            var updatedRole = await _roleService.UpdateStatus(id, isActive);
            return Ok(updatedRole);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [PermissionAuthorize(Role.DeleteRole)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _roleService.Delete(id);
            return Ok();
        }

    }
}
