using Hangfire;
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
    public class QuizJob
    {
        private readonly IHubContext<NotificationHub> _notiHubContext;
        private readonly INotificationService _notificationService;
        private readonly IQuizAttemptService _quizAttemptService;

        public QuizJob(IHubContext<NotificationHub> notiHubContext, INotificationService notificationService,
            IQuizAttemptService quizAttemptService)
        {
            _notiHubContext = notiHubContext;
            _notificationService = notificationService;
            _quizAttemptService = quizAttemptService;
        }
        public async Task ActiveJobWhenQuizStart(List<Guid> studentIds, Quiz quiz, int courseId)
        {
            await NotifyStartingToSpecifiedUserList(studentIds, quiz, courseId);

            //create background job for create user quiz for student didn't attempt quiz
            BackgroundJob.Schedule(() =>
                            _quizAttemptService.CreateUserQuizWhenQuizEnd(studentIds, quiz, courseId),
                            quiz.EndTime);
        }
        public async Task NotifyStartingToSpecifiedUserList(List<Guid> studentIds, Quiz quiz, int courseId)
        {
            DateTimeOffset startTime = quiz.StartTime;
            string title = $"Your quiz is about to begin";
            //format date time in string is 12:00 PM on March 12, 2022
            string message = $"You have a quiz called {quiz.Name} that starts at {startTime.ToString("hh:mm tt")} " +
                $"on {startTime.ToString("MMMM dd, yyyy")}\n";
            //string url = $"https://lms.hisoft.vn/admin/quiz?quizid={quiz.Id}&courseid={courseId}";
            string url = $"/learning?quizid={quiz.Id}&courseid={courseId}";

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
