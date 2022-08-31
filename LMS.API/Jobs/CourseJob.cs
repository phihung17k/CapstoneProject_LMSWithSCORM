using Hangfire;
using LMS.Core.Entity;
using LMS.Core.Enum;
using LMS.Core.Models.MailModels;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.API.Jobs
{
    public class CourseJob
    {
        private readonly QuizJob _quizNotificationJob;
        private readonly SurveyNotificationJob _surveyNotificationJob;
        private readonly IMailService _mailService;
        private readonly IUserRepository _userRepository;
        private readonly ICourseService _courseService;

        public CourseJob(QuizJob quizNotificationJob, SurveyNotificationJob surveyNotificationJob,
            IMailService mailService, IUserRepository userRepository, ICourseService courseService)
        {
            _quizNotificationJob = quizNotificationJob;
            _surveyNotificationJob = surveyNotificationJob;
            _mailService = mailService;
            _userRepository = userRepository;
            _courseService = courseService;
        }
        public void CheckCourseDaily()
        {
            var upcommingCourses = _courseService.GetUpcomingCourseInToday();

            if (upcommingCourses.Any())
            {
                foreach (var course in upcommingCourses)
                {
                    List<Guid> studentIds = course.Users.Where(u => u.ActionType == ActionType.Study)
                                                      .Select(u => u.UserId).ToList();

                    //create background job when course start
                    BackgroundJob.Schedule(() =>
                                    ActiveJobWhenCourseStart(studentIds, course),
                                    course.StartTime);
                }
            }

            //auto update learning progress from in progress -> fail when course end
            var endCourses = _courseService.GetEndCourseInToday();
            if (endCourses.Any())
            {
                foreach (var course in endCourses)
                {
                    BackgroundJob.Schedule(() =>
                                       _courseService.UpdateLearningProgressWhenCourseEnd(course.Id),
                                       course.EndTime.Add(TimeSpan.FromSeconds(30)));
                }
            }
        }

        public async Task ActiveJobWhenCourseStart(List<Guid> studentIds, Course course)
        {
            //Announcing to student at the time course start(mail)
            await ReminderAttendeesJoinCourseByMail(studentIds, course);

            //Create background job for announcing to student at the time quiz and survey start (notification)
            await NotifyUpcomingQuizAndSurvey(studentIds, course.Id);
        }

        //for student
        public async Task NotifyUpcomingQuizAndSurvey(List<Guid> studentIds, int courseId)
        {
            var course = await _courseService.GetCourseWithActivities(courseId);
            foreach (var topic in course.Topics)
            {
                if (topic.Quizzes != null && topic.Quizzes.Any())
                {
                    foreach (var quiz in topic.Quizzes)
                    {
                        BackgroundJob.Schedule(() =>
                            _quizNotificationJob.ActiveJobWhenQuizStart(studentIds, quiz, courseId),
                            quiz.StartTime);
                    }
                }
                if (topic.Surveys != null && topic.Surveys.Any())
                {
                    foreach (var survey in topic.Surveys)
                    {
                        BackgroundJob.Schedule(() =>
                            _surveyNotificationJob.NotifyStartingToSpecifiedUserList(studentIds, survey, courseId),
                            survey.StartDate);
                    }
                }
            }
        }

        public async Task ReminderAttendeesJoinCourseByMail(List<Guid> studentIds, Course course)
        {
            foreach (var studentId in studentIds)
            {
                var attendee = _userRepository.FindAsync(studentId).Result;
                if (attendee != null)
                {
                    Message message = new()
                    {
                        To = attendee.Email,
                        Subject = "Your course has started, join now!",
                        Content = _mailService.GetEmailTemplate("CourseStartTemplate", new TemplateModel()
                        {
                            Name = string.Join(" ", attendee.FirstName, attendee.LastName),
                            CourseId = course.Id,
                            CourseName = course.Name,
                        })
                    };
                    await _mailService.SendEmailAsync(message);
                }
            }
            //save tracking mail in db
            await _mailService.CreateMail(studentIds, "Announcement of " + course.Name + " course starting");
        }
    }
}
