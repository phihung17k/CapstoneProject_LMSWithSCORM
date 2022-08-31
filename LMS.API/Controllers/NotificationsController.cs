using LMS.API.Permission;
using LMS.Core.Enum;
using LMS.Core.Models.RequestModels;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static LMS.Core.Common.PermissionConstants;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService service;

        public NotificationsController(INotificationService service)
        {
            this.service = service;
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(PagingViewModel<NotificationViewModel>), 200)]
        [PermissionAuthorize(BasePermission.ReceiveNotification)]
        public IActionResult GetAll([FromQuery] NotificationPagingRequestModel requestModel)
        {
            var result = service.GetAll(requestModel, NotificationType.All);
            return Ok(result);
        }

        [HttpGet("all/unread")]
        [ProducesResponseType(typeof(PagingViewModel<NotificationViewModel>), 200)]
        [PermissionAuthorize(BasePermission.ReceiveNotification)]
        public IActionResult GetUnreadNotification([FromQuery] NotificationPagingRequestModel requestModel)
        {
            var result = service.GetAll(requestModel, NotificationType.Unread);
            return Ok(result);
        }

        [HttpPut("read/{notificationId}")]
        [PermissionAuthorize(BasePermission.ReceiveNotification)]
        public async Task<IActionResult> SetIsRead(Guid notificationId)
        {
            await service.SetIsRead(notificationId);
            return Ok();
        }
    }
}
