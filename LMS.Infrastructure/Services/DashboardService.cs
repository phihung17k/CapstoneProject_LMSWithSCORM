using LMS.Core.Application;
using LMS.Core.Enum;
using LMS.Core.Models.RequestModels.DashboardRequestModel;
using LMS.Core.Models.ViewModels;
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
    public class DashboardService : IDashboardService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserCourseRepository _userCourseRepository;
        private readonly IRoleRepository _roleRepository;

        public DashboardService(ICourseRepository courseRepository, ICurrentUserService currentUserService,
            IUserCourseRepository userCourseRepository, IRoleRepository roleRepository)
        {
            _courseRepository = courseRepository;
            _currentUserService = currentUserService;
            _userCourseRepository = userCourseRepository;
            _roleRepository = roleRepository;
        }

        public Task<AttendeeLearningProgressRatioViewModel> GetAttendeesLearningProgressRatio(AttendeeLearningProgressRatioRequestModel requestModel, Guid? userId)
        {
            if (requestModel.ActionType != null && userId == null)
            {
                userId = _currentUserService.UserId;
            }
            var courses = _courseRepository.Get(c => c.IsActive != false && c.IsDeleted != true
            && (requestModel.CourseId == 0 || c.Id == requestModel.CourseId)
            && (userId == null || c.Users.Any(u => u.UserId == userId))
                    && (requestModel.ActionType != ActionTypeWithoutStudy.Manage || c.Users.Any(u => u.ActionType == ActionType.Manage && u.UserId == userId))
                    && (requestModel.ActionType != ActionTypeWithoutStudy.Teach || c.Users.Any(u => u.ActionType == ActionType.Teach && u.UserId == userId)),
                    c => c.Users.Where(uc => uc.ActionType == ActionType.Study));
            if (!courses.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            var attendeesInCourses = courses.SelectMany(c => c.Users);
            int numOfInProgress = attendeesInCourses.Where(ac => ac.LearningStatus == LearningStatus.InProgress).Count();
            int numOfPassed = attendeesInCourses.Where(ac => ac.LearningStatus == LearningStatus.Passed).Count();
            int numOfFailed = attendeesInCourses.Where(ac => ac.LearningStatus == LearningStatus.Failed).Count();

            return Task.FromResult(new AttendeeLearningProgressRatioViewModel
            {
                AttendeeLearningProgressStatusRatio = new()
                {
                    new StatusRatioViewModel()
                    {
                        Status = nameof(LearningStatus.InProgress),
                        Count = numOfInProgress
                    },
                    new StatusRatioViewModel()
                    {
                        Status = nameof(LearningStatus.Passed),
                        Count = numOfPassed
                    },
                    new StatusRatioViewModel()
                    {
                        Status = nameof(LearningStatus.Failed),
                        Count = numOfFailed
                    }
                }
            });
        }

        public Task<CourseProgressStatusRatioViewModel> GetCourseProgressRatio(CourseProgressRatioRequestModel requestModel, Guid? userId)
        {
            if (requestModel.ActionType != null && userId == null)
            {
                userId = _currentUserService.UserId;
            }
            var courses = _courseRepository.Get(c => c.IsActive != false && c.IsDeleted != true
            && (userId == null || c.Users.Any(u => u.UserId == userId))
                    && (requestModel.ActionType != ActionTypeWithoutStudy.Manage || c.Users.Any(u => u.ActionType == ActionType.Manage && u.UserId == userId))
                    && (requestModel.ActionType != ActionTypeWithoutStudy.Teach || c.Users.Any(u => u.ActionType == ActionType.Teach && u.UserId == userId)));
            if (!courses.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            var now = DateTimeOffset.UtcNow;
            int numOfUpComming = courses.Where(c => c.StartTime > now).Count();
            int numOfInProgress = courses.Where(c => c.StartTime <= now && c.EndTime >= now).Count();
            int numOfCompleted = courses.Where(c => c.EndTime <= now).Count();
            return Task.FromResult(new CourseProgressStatusRatioViewModel
            {
                CourseProgressStatusRatio = new()
                {
                    new StatusRatioViewModel()
                    {
                        Status = nameof(CourseProgressStatus.UpComming),
                        Count = numOfUpComming
                    },
                    new StatusRatioViewModel()
                    {
                        Status = nameof(CourseProgressStatus.InProgress),
                        Count = numOfInProgress
                    },
                    new StatusRatioViewModel()
                    {
                        Status = nameof(CourseProgressStatus.Completed),
                        Count = numOfCompleted
                    }
                }
            });
        }

        public Task<OwnLearningProgressRatioByCourseViewModel> GetOwnLearningProgressByCourse()
        {
            var userId = _currentUserService.UserId;
            var coursesTracking = _userCourseRepository.Get(uc => uc.UserId == userId && uc.ActionType == ActionType.Study, uc => uc.Course);
            if (!coursesTracking.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            var now = DateTimeOffset.UtcNow;
            int numOfUpComming = coursesTracking.Where(c => c.Course.StartTime > now).Count();
            int numOfInProgress = coursesTracking.Where(c => c.Course.StartTime <= now && c.Course.EndTime >= now
            && (c.LearningStatus == LearningStatus.Undefined || c.LearningStatus == LearningStatus.InProgress)).Count();
            int numOfPassed = coursesTracking.Where(c => c.LearningStatus == LearningStatus.Passed).Count();
            int numOfFailed = coursesTracking.Where(c => c.LearningStatus == LearningStatus.Failed).Count();
            return Task.FromResult(new OwnLearningProgressRatioByCourseViewModel
            {
                OwnLearningProgressRatio = new()
                {
                    new StatusRatioViewModel()
                    {
                        Status = nameof(CourseProgressStatus.UpComming),
                        Count = numOfUpComming
                    },
                    new StatusRatioViewModel()
                    {
                        Status = nameof(LearningStatus.InProgress),
                        Count = numOfInProgress
                    },
                    new StatusRatioViewModel()
                    {
                        Status = nameof(LearningStatus.Passed),
                        Count = numOfPassed
                    },
                    new StatusRatioViewModel()
                    {
                        Status = nameof(LearningStatus.Failed),
                        Count = numOfFailed
                    }
                }
            });
        }

        public Task<TotalRoleRatioViewModel> GetTotalRoleRatio()
        {
            var roles = _roleRepository.Get(r => true, r => r.Users).AsSplitQuery().ToList();
            if (!roles.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            List<RoleRatioViewModel> roleRatio = new();
            roles = roles.OrderByDescending(r => r.Users.Count).ToList();
            var totalRoles = roles.Count;
            for (int i = 0; i < totalRoles; i++)
            {
                if (i > 9) //chart must has max 10 items
                {
                    break;
                }
                if (totalRoles > 10 && i == 9)
                {
                    var otherUserCount = 0;
                    for (int j = i; j < totalRoles; j++)
                    {
                        otherUserCount += roles[j].Users.Count;
                    }
                    roleRatio.Add(new RoleRatioViewModel
                    {
                        Role = "Others",
                        UserCount = otherUserCount
                    });
                }
                else
                {
                    roleRatio.Add(new RoleRatioViewModel
                    {
                        Role = roles[i].Name,
                        UserCount = roles[i].Users.Count
                    });
                }
            }

            return Task.FromResult(new TotalRoleRatioViewModel
            {
                TotalRoles = totalRoles,
                RoleRatio = roleRatio
            });
        }
    }
}
