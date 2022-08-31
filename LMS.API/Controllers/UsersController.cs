using LMS.API.Permission;
using LMS.Core.Application;
using LMS.Core.Models.RequestModels.UserRequestModel;
using LMS.Core.Models.RequestModels.UserRoleRequestModel;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static LMS.Core.Common.PermissionConstants;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ITMSService _tmsService;
        private readonly ICurrentUserService _currentUserService;

        public UsersController(IUserService userService, ITMSService tmsService, ICurrentUserService currentUserService)
        {
            this.userService = userService;
            _tmsService = tmsService;
            _currentUserService = currentUserService;
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(PagingViewModel<UserRoleViewModel>), 200)]
        [PermissionAuthorize(Account.ViewUsersList)]
        public async Task<IActionResult> GetAllUser([FromQuery] UserPagingRequestModel userPagingRequestModel)
        {
            var users = await userService.GetAllUser(userPagingRequestModel);
            return Ok(users);
        }

        [HttpGet]
        [Route("detail/{userId}")]
        [ProducesResponseType(typeof(UserRoleViewModel), 200)]
        [PermissionAuthorize(Account.ViewDetailOfUser)]
        public async Task<IActionResult> GetDetailUser(string userId)
        {
            var userDetail = await userService.GetDetailUserRole(userId);
            return Ok(userDetail);
        }

        [HttpGet]
        [Route("profile")]
        [ProducesResponseType(typeof(UserViewModel), 200)]
        [PermissionAuthorize(BasePermission.ViewProfile)]
        public async Task<IActionResult> ViewProfile()
        {
            var profile = await userService.GetDetailUser(_currentUserService.UserId);
            return Ok(profile);
        }

        [HttpPut("assign/{userId}")]
        [ProducesResponseType(typeof(List<RoleViewModelWithoutPermission>),200)]
        [PermissionAuthorize(Account.AssignRoleToUser)]
        public async Task<IActionResult> AssignRole(string userId, [FromBody] UserRoleRequestModel userRoleRequestModel)
        {
            var result = await userService.AssignRoleToUser(userId, userRoleRequestModel);
            return Ok(result);
        }

        [HttpPut("update/avatar")]
        [ProducesResponseType(typeof(string), 200)]
        [PermissionAuthorize(BasePermission.EditProfile)]
        public IActionResult UpdateProfile(IFormFile image)
        {
            string url = userService.UpdateAvatar(_currentUserService.UserId, image).GetAwaiter().GetResult();
            return Ok(url);
        }

        [HttpGet]
        [Route("sync-user")]
        public async Task<IActionResult> SyncUser()
        {
            await _tmsService.VerifyAuthentication();
            await userService.SyncUser();
            return Ok();
        }

        [HttpGet("get-question-creator/{questionBankId}")]
        [ProducesResponseType(typeof(List<QuestionCreatorViewModel>), 200)]
        [PermissionAuthorize(Question.ViewQuestionsList)]
        public IActionResult GetQuestionCreators(int questionBankId)
        {
            var result = userService.GetQuestionCreators(questionBankId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpPut("update/status/{id}")]
        [ProducesResponseType(typeof(UserViewModel), 200)]
        [PermissionAuthorize(Account.UpdateStatus)]
        public async Task<IActionResult> UpdateStatus(Guid id, bool isActive)
        {
            var result = await userService.UpdateStatus(id, isActive);
            return Ok(result);
        }
    }
}
