using LMS.Core.Entity;
using LMS.Core.Enum;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Services
{
    public class UserCourseService : IUserCourseService
    {
        private readonly IUserCourseRepository _userCourseRepository;
        private readonly ITopicOtherLearningResourceRepository _topicOLRRepository;
        private readonly ITopicSCORMRepository _topicSCORMRepository;
        private readonly ISurveyRepository _surveyRepository;
        private readonly IQuizRepository _quizRepository;
        private readonly IRoleUserRepository _roleUserRepository;

        public UserCourseService(IUserCourseRepository userCourseRepository,
            ITopicOtherLearningResourceRepository topicOLRRepository, ITopicSCORMRepository topicSCORMRepository,
            ISurveyRepository surveyRepository, IQuizRepository quizRepository, IRoleUserRepository roleUserRepository)
        {
            _userCourseRepository = userCourseRepository;
            _topicOLRRepository = topicOLRRepository;
            _topicSCORMRepository = topicSCORMRepository;
            _surveyRepository = surveyRepository;
            _quizRepository = quizRepository;
            _roleUserRepository = roleUserRepository;
        }

        private CourseProgressStatus GetCourseProgress(Course course)
        {
            if (course == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.CourseNotFound,
                    ErrorMessages.CourseNotFound);
            }
            DateTimeOffset now = DateTimeOffset.Now;
            DateTimeOffset startTime = course.StartTime;
            DateTimeOffset endTime = course.EndTime;
            if (now < startTime)
            {
                return CourseProgressStatus.UpComming;
            }
            if (startTime <= now && now <= endTime)
            {
                return CourseProgressStatus.InProgress;
            }
            //endTime < now
            return CourseProgressStatus.Completed;
        }

        private void ValidateAccessingInEachFunction(ActionMethods actionMethods, ActionType actionType,
            CourseProgressStatus courseProgress)
        {
            switch (actionMethods)
            {
                case ActionMethods.Nothing:
                    break;
                case ActionMethods.ManageTopic:
                    //teacher and manager can create/update/delete topic before course start
                    if ((actionType == ActionType.Teach || actionType == ActionType.Manage)
                        && courseProgress == CourseProgressStatus.UpComming)
                    {
                    }
                    else
                    {
                        throw new RequestException(HttpStatusCode.Forbidden, ErrorCodes.CannotPerformAction,
                            ErrorMessages.CannotPerformAction);
                    }
                    break;
                case ActionMethods.ManageLearningResource:
                    //teacher can upload, delete, move learning resource (OLR and SCORM) before course start
                    if(actionType == ActionType.Teach && courseProgress == CourseProgressStatus.UpComming)
                    {
                    }
                    else
                    {
                        throw new RequestException(HttpStatusCode.Forbidden, ErrorCodes.CannotPerformAction,
                            ErrorMessages.CannotPerformAction);
                    }
                    break;
                case ActionMethods.ManageQuiz:
                    //teacher can create, update, delete quiz before course start
                    if (actionType == ActionType.Teach && courseProgress == CourseProgressStatus.UpComming)
                    {
                    }
                    else
                    {
                        throw new RequestException(HttpStatusCode.Forbidden, ErrorCodes.CannotPerformAction,
                            ErrorMessages.CannotPerformAction);
                    }
                    break;
                case ActionMethods.ManageSurvey:
                    //manager can update survey before course start 
                    if (actionType == ActionType.Manage && courseProgress == CourseProgressStatus.UpComming)
                    {
                    }
                    else
                    {
                        throw new RequestException(HttpStatusCode.Forbidden, ErrorCodes.CannotPerformAction,
                            ErrorMessages.CannotPerformAction);
                    }
                    break;
                case ActionMethods.UpdateCourse:
                    //teacher can update course before course start
                    if (actionType == ActionType.Teach && courseProgress == CourseProgressStatus.UpComming)
                    {
                    }
                    else
                    {
                        throw new RequestException(HttpStatusCode.Forbidden, ErrorCodes.CannotPerformAction,
                            ErrorMessages.CannotPerformAction);
                    }
                    break;
            }
        }

        private bool IsAllowedAccess(UserCourse userCourse, ActionMethods methods, bool isStudent, bool isTeacher,
            bool isManager)
        {
            if (userCourse == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.UserCourseNotFound,
                    ErrorMessages.UserCourseNotFound);
            }

            //if user is student/teacher/manager and method allows student/teacher/manager, return true
            //if user is admin => return true
            if ((userCourse.ActionType == ActionType.Study && isStudent) ||
                (userCourse.ActionType == ActionType.Teach && isTeacher) ||
                (userCourse.ActionType == ActionType.Manage && isManager))
            {
                CourseProgressStatus courseProgress = GetCourseProgress(userCourse.Course);
                //if student join in course hasn't started yet, throw exception
                if (userCourse.ActionType == ActionType.Study && courseProgress == CourseProgressStatus.UpComming)
                {
                    throw new RequestException(HttpStatusCode.Forbidden, ErrorCodes.CannotAccessUpcomingCourse,
                        ErrorMessages.CannotAccessUpcomingCourse);
                }

                //check course start end for each function
                ValidateAccessingInEachFunction(methods, userCourse.ActionType, courseProgress);

                return true;
            }
            return false;
        }

        //check action in course: get attendees summary, create topic
        //example: in course detail page, teacher and manager can create topic, student is not
        //          so that, this method check current user is teacher or manager to allow accessibility
        public Task CheckCourseAccessibility(int courseId, Guid userId, ActionMethods methods, bool isStudent = false,
            bool isTeacher = false, bool isManager = false)
        {
            var userCourse = _userCourseRepository.Get(uc => uc.CourseId == courseId && uc.UserId == userId)
                                                  .Include(uc => uc.Course)
                                                  .FirstOrDefault();
            bool isAllowedAccess = IsAllowedAccess(userCourse, methods, isStudent, isTeacher, isManager);
            if (isAllowedAccess)
            {
                return Task.CompletedTask;
            }
            throw new RequestException(HttpStatusCode.Forbidden, ErrorCodes.Forbidden, ErrorMessages.Forbidden);
        }

        //check action in topic: update topic, add resource, ...
        public Task CheckTopicAccessibility(int topicId, Guid userId, ActionMethods methods, bool isStudent = false,
            bool isTeacher = false, bool isManager = false)
        {
            var userCourseList = _userCourseRepository.Get(uc => uc.UserId == userId)
                                                  .Include(uc => uc.Course)
                                                  .ThenInclude(c => c.Topics)
                                                  .AsSplitQuery();
            if (userCourseList == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.UserCourseNotFound,
                       ErrorMessages.UserCourseNotFound);
            }
            //get user course by topic id 
            var userCourse = userCourseList.Where(uc => uc.Course.Topics != null
                                                     && uc.Course.Topics.Select(t => t.Id).Contains(topicId))
                                           .FirstOrDefault();
            bool isAllowedAccess = IsAllowedAccess(userCourse, methods, isStudent, isTeacher, isManager);
            if (isAllowedAccess)
            {
                return Task.CompletedTask;
            }
            throw new RequestException(HttpStatusCode.Forbidden, ErrorCodes.Forbidden, ErrorMessages.Forbidden);
        }

        //get topic id from 4 resource types include OLR, SCORM, Survey, Quiz
        private int GetTopicId(int topicResourceId, TopicResourceType resourceType)
        {
            int topicId = 0;
            //get topic id contains the topic other learning resource
            switch (resourceType)
            {
                case TopicResourceType.OtherLearningResource:
                    var topicOLR = _topicOLRRepository.FindAsync(topicResourceId).GetAwaiter().GetResult();
                    if (topicOLR == null)
                    {
                        throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.ResourceInTopicIsNotFound,
                               ErrorMessages.ResourceInTopicIsNotFound);
                    }
                    topicId = topicOLR.TopicId;
                    break;
                case TopicResourceType.SCORM:
                    var topicSCORM = _topicSCORMRepository.FindAsync(topicResourceId).GetAwaiter().GetResult();
                    if (topicSCORM == null)
                    {
                        throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.ResourceInTopicIsNotFound,
                               ErrorMessages.ResourceInTopicIsNotFound);
                    }
                    topicId = topicSCORM.TopicId;
                    break;
                case TopicResourceType.Survey:
                    var survey = _surveyRepository.FindAsync(topicResourceId).GetAwaiter().GetResult();
                    if (survey == null)
                    {
                        throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.ResourceInTopicIsNotFound,
                               ErrorMessages.ResourceInTopicIsNotFound);
                    }
                    topicId = survey.TopicId;
                    break;
                case TopicResourceType.Quiz:
                    var quiz = _quizRepository.FindAsync(topicResourceId).GetAwaiter().GetResult();
                    if (quiz == null)
                    {
                        throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.ResourceInTopicIsNotFound,
                               ErrorMessages.ResourceInTopicIsNotFound);
                    }
                    topicId = quiz.TopicId;
                    break;
            }
            return topicId;
        }

        //check action in topic resource: delete OLR, update quiz, ...
        //topicResourceId may be topicOLRId, topicSCORMId, surveyId or quizId
        public Task CheckTopicResourceAccessibility(int topicResourceId, Guid userId, TopicResourceType resourceType,
            ActionMethods methods, bool isStudent = false, bool isTeacher = false, bool isManager = false)
        {
            var userCourseList = Task.FromResult(_userCourseRepository.Get(uc => uc.UserId == userId)
                                                  .Include(uc => uc.Course)
                                                  .ThenInclude(c => c.Topics)
                                                  .AsSplitQuery()).Result;
            if (userCourseList == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.UserCourseNotFound,
                       ErrorMessages.UserCourseNotFound);
            }

            int topicId = GetTopicId(topicResourceId, resourceType);

            //get user course by topic id 
            var userCourse = userCourseList.Where(uc => uc.Course.Topics != null
                                                     && uc.Course.Topics.Select(t => t.Id).Contains(topicId))
                                           .FirstOrDefault();
            bool isAllowedAccess = IsAllowedAccess(userCourse, methods, isStudent, isTeacher, isManager);
            if (isAllowedAccess)
            {
                return Task.CompletedTask;
            }
            throw new RequestException(HttpStatusCode.Forbidden, ErrorCodes.Forbidden, ErrorMessages.Forbidden);
        }

        //for view course detail
        public Task CheckViewingCourseDetail(int courseId, Guid userId)
        {
            var userCourse = _userCourseRepository.Get(uc => uc.CourseId == courseId && uc.UserId == userId)
                                                  .Include(uc => uc.Course)
                                                  .FirstOrDefault();
            //case: user is not assigned => pass condition
            if (userCourse == null)
            {
                return Task.CompletedTask;
            }
            //case: user is assigned for Study in course and course progress is upcoming => forbid
            CourseProgressStatus courseProgress = GetCourseProgress(userCourse.Course);
            if (userCourse.ActionType == ActionType.Study && courseProgress == CourseProgressStatus.UpComming)
            {
                throw new RequestException(HttpStatusCode.Forbidden, ErrorCodes.CannotAccessUpcomingCourse,
                    ErrorMessages.CannotAccessUpcomingCourse);
            }
            //case: user is assigned for Teach or Manager=> pass condition
            return Task.CompletedTask;
        }

        //for view list attendees in course detail
        public Task CheckViewingListAttendessInCourse(int courseId, Guid userId)
        {
            var userCourse = _userCourseRepository.Get(uc => uc.CourseId == courseId && uc.UserId == userId)
                                                  .FirstOrDefault();
            //case: user is not assigned => pass condition
            if (userCourse == null)
            {
                return Task.CompletedTask;
            }
            //case: user is assigned for Study => forbid
            if (userCourse.ActionType == ActionType.Study)
            {
                throw new RequestException(HttpStatusCode.Forbidden, ErrorCodes.CannotPerformAction,
                    ErrorMessages.CannotPerformAction);
            }
            //case: user is assigned for Teach or Manage => pass condition
            return Task.CompletedTask;
        }
    }
}
