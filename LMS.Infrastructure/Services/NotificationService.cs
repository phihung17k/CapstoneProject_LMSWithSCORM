using AutoMapper;
using LMS.Core.Application;
using LMS.Core.Entity;
using LMS.Core.Enum;
using LMS.Core.Models.RequestModels;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository notificationRepository;
        private readonly ICurrentUserService currentUserService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly INotificationRecipientRepository notificationRecipientRepository;

        public NotificationService(INotificationRepository notificationRepository, ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork, IMapper mapper, INotificationRecipientRepository notificationRecipientRepository)
        {
            this.notificationRepository = notificationRepository;
            this.currentUserService = currentUserService;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.notificationRecipientRepository = notificationRecipientRepository;
        }

        public async Task<Notification> CreateNotification(List<Guid> userIds, NotificationCreateRequestModel requestModel)
        {
            Notification notification = new Notification
            {
                Id = Guid.NewGuid(),
                Title = requestModel.Title,
                Message = requestModel.Message,
                Url = requestModel.Url
            };
            await notificationRepository.AddAsync(notification);
            await unitOfWork.SaveChangeAsync();
            foreach (var userId in userIds)
            {
                NotificationRecipient notificationRecipient = new NotificationRecipient
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    NotificationId = notification.Id
                };
                await notificationRecipientRepository.AddAsync(notificationRecipient);
            }
            await unitOfWork.SaveChangeAsync();
            return notification;
        }

        public PagingViewModel<NotificationViewModel> GetAll(NotificationPagingRequestModel requestModel,
            NotificationType notificationType)
        {
            var notificationList = notificationRepository.GetAll()
                            .Include(n => n.NotificationRecipientList)
                            .AsSplitQuery();
            List<NotificationViewModel> notificationViewModelList = new();
            int totalRecord = 0;
            int numberOfUnreadNotification = 0;
            if (notificationList != null && notificationList.Any())
            {
                //filter notification of current user
                notificationList = notificationList.Where(n => 
                    n.NotificationRecipientList.Select(nr => nr.UserId).Contains(currentUserService.UserId));
                if (notificationType == NotificationType.All)
                {
                    foreach (var notification in notificationList)
                    {
                        var notificationViewModel = mapper.Map<NotificationViewModel>(notification);
                        if (!notificationViewModel.IsRead)
                        {
                            numberOfUnreadNotification++;
                        }
                        notificationViewModelList.Add(notificationViewModel);
                    }
                }
                else if (notificationType == NotificationType.Unread)
                {
                    foreach (var notification in notificationList)
                    {
                        var notificationViewModel = mapper.Map<NotificationViewModel>(notification);
                        if (!notificationViewModel.IsRead)
                        {
                            notificationViewModelList.Add(notificationViewModel);
                        }
                    }
                }
                totalRecord = notificationViewModelList.Count;
                notificationViewModelList = notificationViewModelList.AsQueryable()
                                                   .OrderByDescending(n => n.CreateTime)
                                                   .Skip((requestModel.CurrentPage - 1) * requestModel.PageSize)
                                                   .Take(requestModel.PageSize)
                                                   .ToList();
            }
            return new PagingViewModel<NotificationViewModel>
                                            (notificationViewModelList, totalRecord,
                                            requestModel.CurrentPage,
                                            requestModel.PageSize,
                                            numberOfUnreadNotification: numberOfUnreadNotification);
        }

        public async Task SetIsRead(Guid notificationId)
        {
            var notification = notificationRepository.Get(n => n.Id == notificationId)
                        .Include(n => n.NotificationRecipientList.Where(nr => nr.UserId == currentUserService.UserId
                                                                    && !nr.IsRead))
                        .AsSplitQuery()
                        .FirstOrDefault();
            if (notification == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            notification.NotificationRecipientList.FirstOrDefault().IsRead = true;
            await unitOfWork.SaveChangeAsync();
        }
    }
}
