using LMS.Core.Entity;
using LMS.Core.Enum;
using LMS.Core.Models.RequestModels;
using LMS.Core.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IServices
{
    public interface INotificationService
    {
        Task<Notification> CreateNotification(List<Guid> userIds, NotificationCreateRequestModel requestModel);
        Task SetIsRead(Guid notificationId);
        PagingViewModel<NotificationViewModel> GetAll(NotificationPagingRequestModel requestModel,
            NotificationType notificationType);
    }
}
