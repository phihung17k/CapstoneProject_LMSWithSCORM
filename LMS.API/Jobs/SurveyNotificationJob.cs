using LMS.API.Hubs;
using LMS.Core.Entity;
using LMS.Core.Models.RequestModels;
using LMS.Infrastructure.IServices;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.API.Jobs
{
    public class SurveyNotificationJob
    {
        private readonly IHubContext<NotificationHub> _notiHubContext;
        private readonly INotificationService _notificationService;

        public SurveyNotificationJob(IHubContext<NotificationHub> notiHubContext, INotificationService notificationService)
        {
            _notiHubContext = notiHubContext;
            _notificationService = notificationService;
        }

        public async Task NotifyStartingToSpecifiedUserList(List<Guid> studentIds, Survey survey, int courseId)
        {
            DateTimeOffset startTime = survey.StartDate;
            string title = $"Your survey is about to begin";
            //format date time in string is 12:00 PM on March 12, 2022
            string message = $"You have a survey called {survey.Name} that starts at {startTime.ToString("hh:mm tt")} " +
                $"on {startTime.ToString("MMMM dd, yyyy")}\n";
            //string url = $"https://lms.hisoft.vn/admin/learning?courseid={courseId}";
            string url = $"/learning?surveyid={survey.Id}&courseid={courseId}";

            await _notificationService.CreateNotification(studentIds, new NotificationCreateRequestModel
            {
                Message = message,
                Title = title,
                Url = url
            });

            foreach (var userId in studentIds)
            {
                await _notiHubContext.Clients.User(userId.ToString())
                        .SendAsync("Notify", title, message, url);
            }
        }
    }
}
