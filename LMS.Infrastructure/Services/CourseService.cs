using AutoMapper;
using LMS.Core.Application;
using LMS.Core.Entity;
using LMS.Core.Enum;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.IServices;
using LMS.Core.Models.RequestModels;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LMS.Core.Models.TMSResponseModel;
using LMS.Core.Models.MailModels;
using LMS.Core.Models.RequestModels.SectionRequestModel;

namespace LMS.Infrastructure.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IUserCourseRepository _userCourseRepository;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _factory;
        private readonly TMSRepository _tmsRepository;
        private readonly ISyncLogRepository _syncLogRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;
        private readonly IMailService _mailService;
        private readonly IUserSubjectRepository _userSubjectRepository;
        private readonly ITopicRepository _topicRepository;
        private readonly ITopicTrackingRepository _topicTrackingRepository;
        private readonly ISectionRepository _sectionRepository;

        public CourseService(ICourseRepository courseRepository, ISubjectRepository subjectRepository,
            IUserCourseRepository userCourseRepository, IMapper mapper, IHttpClientFactory factory,
            TMSRepository tmsRepository, ISyncLogRepository syncLogRepository, IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService, IUserRepository userRepository, IMailService mailService,
            IUserSubjectRepository userSubjectRepository, ITopicRepository topicRepository, 
            ITopicTrackingRepository topicTrackingRepository, ISectionRepository sectionRepository)
        {
            _courseRepository = courseRepository;
            _subjectRepository = subjectRepository;
            _userCourseRepository = userCourseRepository;
            _mapper = mapper;
            _factory = factory;
            _tmsRepository = tmsRepository;
            _syncLogRepository = syncLogRepository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
            _mailService = mailService;
            _userSubjectRepository = userSubjectRepository;
            _topicRepository = topicRepository;
            _topicTrackingRepository = topicTrackingRepository;
            _sectionRepository = sectionRepository;
        }

        public Task<PagingViewModel<CoursePagingViewModel>> SearchCourse(CoursePagingRequestModel requestModel, Guid? userId)
        {
            try
            {
                var now = DateTimeOffset.Now;
                if ((requestModel.ActionType != null || requestModel.LearningStatus != null) && userId == null)
                {
                    userId = _currentUserService.UserId;
                }

                if (requestModel.EndTime != null)
                {
                    //var endTimeWithoutTimezone = DateTimeOffset.Parse(requestModel.EndTime.ToString(), null);
                    var hour = ((DateTimeOffset)requestModel.EndTime).Hour;
                    if (hour == 0)
                    {
                        requestModel.EndTime = ((DateTimeOffset)requestModel.EndTime).Add(TimeSpan.FromHours(24));
                    }
                }

                string search = requestModel.CourseName?.ToLower();
                IQueryable<Course> result = _courseRepository.Get(c =>
                    (search == null || (c.Code + " - " + c.Name).ToLower().Contains(search) || (c.ParentCode + " - " + c.ParentName).ToLower().Contains(search))
                    && (requestModel.CourseType == null || (c.Type == requestModel.CourseType))
                    && (requestModel.StartTime == null || (c.StartTime >= requestModel.StartTime))
                    && (requestModel.EndTime == null || (c.EndTime <= requestModel.EndTime))
                    && (requestModel.CourseProgressStatus != CourseProgressStatus.UpComming || c.StartTime > now)
                    && (requestModel.CourseProgressStatus != CourseProgressStatus.InProgress || (c.StartTime <= now && c.EndTime >= now))
                    && (requestModel.CourseProgressStatus != CourseProgressStatus.Completed || c.EndTime <= now)
                    && (userId == null || c.Users.Any(u => u.UserId == userId))
                    && (requestModel.ActionType != ActionType.Study || c.Users.Any(u => u.ActionType == ActionType.Study && u.UserId == userId))
                    && (requestModel.ActionType != ActionType.Manage || c.Users.Any(u => u.ActionType == ActionType.Manage && u.UserId == userId))
                    && (requestModel.ActionType != ActionType.Teach || c.Users.Any(u => u.ActionType == ActionType.Teach && u.UserId == userId))
                    && (requestModel.LearningStatus != LearningStatusWithoutUndefined.InProgress || (c.StartTime <= now && c.EndTime >= now && c.Users.Any(u => u.ActionType == ActionType.Study && (u.LearningStatus == LearningStatus.Undefined || u.LearningStatus == LearningStatus.InProgress) && u.UserId == userId)))
                    && (requestModel.LearningStatus != LearningStatusWithoutUndefined.Passed || c.Users.Any(u => u.LearningStatus == LearningStatus.Passed && u.UserId == userId))
                    && (requestModel.LearningStatus != LearningStatusWithoutUndefined.Failed || c.Users.Any(u => u.LearningStatus == LearningStatus.Failed && u.UserId == userId))
                    && (requestModel.SubjectId == null || c.SubjectId == requestModel.SubjectId)
                    && c.IsDeleted != true);

                if (!result.Any())
                {
                    throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
                }

                #region sort
                if (requestModel.StartTimeSort != SortOrder.Descending
                    && requestModel.EndTimeSort != SortOrder.Descending)
                {
                    result = result.OrderBy(c => c.StartTime).ThenBy(c => c.EndTime);
                }
                else if (requestModel.StartTimeSort != SortOrder.Descending
                    && requestModel.EndTimeSort == SortOrder.Descending)
                {
                    result = result.OrderBy(c => c.StartTime).ThenByDescending(c => c.EndTime);
                }
                else if (requestModel.StartTimeSort == SortOrder.Descending
                    && requestModel.EndTimeSort != SortOrder.Descending)
                {
                    result = result.OrderByDescending(c => c.StartTime).ThenBy(c => c.EndTime);
                }
                else
                {
                    result = result.OrderByDescending(c => c.StartTime).ThenByDescending(c => c.EndTime);
                }
                #endregion

                int totalRecord = result.Count();
                result = result.Skip((requestModel.CurrentPage - 1) * requestModel.PageSize).Take(requestModel.PageSize);

                //include action type and subject
                result = result.Include(c => c.Users.Where(uc => uc.UserId == _currentUserService.UserId))
                    .Include(c => c.Subject).AsSplitQuery();

                var items = result.Select(c => _mapper.Map<Course, CoursePagingViewModel>(c)).ToList();
                if (!items.Any())
                {
                    throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
                }
                return Task.FromResult(new PagingViewModel<CoursePagingViewModel>
                                               (items, totalRecord,
                                               requestModel.CurrentPage,
                                               requestModel.PageSize));
            }
            catch (Exception)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
        }

        public async Task<CourseDetailViewModel> GetCourseDetail(int courseId, Guid? userId)
        {
            var course = _courseRepository.Get(c => c.Id == courseId && c.IsDeleted != true && c.IsActive != false);
            if (course == null || !course.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            var userCourse = _userCourseRepository.Get(uc => uc.UserId == userId && uc.CourseId == courseId).FirstOrDefault();
            string type = userCourse?.ActionType.ToString();
            if (type == nameof(ActionType.Study))
            {
                //if course started and this's the first time trainee join this course
                if (userCourse.LearningStatus == LearningStatus.Undefined)
                {
                    userCourse.LearningStatus = LearningStatus.InProgress;
                    //create topic tracking
                    var topics = _topicRepository.Get(t => t.CourseId == courseId).ToList();
                    if (topics.Any())
                    {
                        IEnumerable<TopicTracking> topicTrackings = topics.Select(topic => new TopicTracking
                        {
                            UserId = (Guid)userId,
                            TopicId = topic.Id
                        });
                        await _topicTrackingRepository.AddRange(topicTrackings);
                    }
                    await _unitOfWork.SaveChangeAsync();
                }
            }
            course = course.Include(c => c.Topics).ThenInclude(t => t.TopicOtherLearningResources).ThenInclude(o => o.OtherLearningResource)
                               .Include(c => c.Topics).ThenInclude(t => t.TopicOtherLearningResources).ThenInclude(to => to.OLRTrackings.Where(ot => ot.LearnerId == userId))
                               .Include(t => t.Topics).ThenInclude(t => t.TopicSCORMs).ThenInclude(o => o.SCORM)
                               .Include(t => t.Topics).ThenInclude(t => t.TopicSCORMs).ThenInclude(ts => ts.SCORMCores.Where(sc => sc.LearnerId == userId))
                               .Include(c => c.Topics).ThenInclude(t => t.Surveys).ThenInclude(s => s.UserSurveys.Where(us => us.UserId == userId))
                               .Include(c => c.Topics).ThenInclude(t => t.Quizzes).ThenInclude(q => q.UserQuizzes.Where(uq => uq.UserId == userId))
                               .Include(c => c.Topics).ThenInclude(t => t.TopicTrackings.Where(tt => tt.UserId == userId))
                               .Include(c => c.Users).ThenInclude(uc => uc.User)
                               .Include(c => c.Subject).AsSplitQuery();
            var courseDetail = _mapper.Map<CourseDetailViewModel>(course.First());
            courseDetail.ActionType = type;

            //check course has quiz with credit > 0 or not
            var quizsInCourse = courseDetail.Topics.SelectMany(t => t.Quizzes);
            float totalCredit = quizsInCourse.Sum(q => q.Credit);
            if (totalCredit == 0)
            {
                courseDetail.HasCreditQuiz = false;
            }

            return courseDetail;
        }

        private UserCourseSubjectModel ConvertCourseInformationToUserCourse(
            List<CourseInformationModel> listOfCourseInformation)
        {
            //convert monitors, instructor, trainees to user course
            List<UserCourse> userCourseList = new();
            //convert instructors to list of user subject
            List<UserSubject> userSubjectList = new();
            foreach (var courseInformation in listOfCourseInformation)
            {
                if (courseInformation.MonitorId != null)
                {
                    userCourseList.Add(new UserCourse()
                    {
                        UserId = courseInformation.MonitorId.GetValueOrDefault(),
                        ActionType = ActionType.Manage,
                        CourseId = courseInformation.CourseId
                    });
                }
                if (courseInformation.ListOfInstructorId.Any())
                {
                    foreach (var instructorId in courseInformation.ListOfInstructorId)
                    {
                        userCourseList.Add(new UserCourse()
                        {
                            UserId = instructorId,
                            ActionType = ActionType.Teach,
                            CourseId = courseInformation.CourseId
                        });
                        userSubjectList.Add(new UserSubject 
                        {
                            UserId = instructorId,
                            SubjectId = courseInformation.SubjectId
                        });
                    }
                }
                if (courseInformation.ListOfTraineeId.Any())
                {
                    foreach (var traineeId in courseInformation.ListOfTraineeId)
                    {
                        userCourseList.Add(new UserCourse()
                        {
                            UserId = traineeId,
                            ActionType = ActionType.Study,
                            CourseId = courseInformation.CourseId
                        });
                    }
                }
            }
            return new UserCourseSubjectModel 
            {
                UserCourseList = userCourseList,
                UserSubjectList = userSubjectList
            };
        }

        private async Task StoreDataSync(List<CourseModel> bigCourseList, HttpClient client,
            DateTimeOffset? lastSyncTime = null)
        {
            List<Course> listOfCourse = new();
            //list of assigned user in course detail (sub-course)
            List<CourseInformationModel> listOfCourseInformation = new();
            List<string> listOfCourseType = new List<string>
            {
                CourseType.Initial.ToString(),
                CourseType.Recurrent.ToString(),
                CourseType.Bridge.ToString(),
                CourseType.Upgrade.ToString(),
                "Re-Qualification"
            };
            foreach (var bigCourse in bigCourseList)
            {
                using (var bigCourseResponse = await client.GetAsync($"api/course/detail?id={bigCourse.Id}"))
                {
                    if (!bigCourseResponse.IsSuccessStatusCode)
                    {
                        string content = await bigCourseResponse.Content.ReadAsStringAsync();
                        throw new HttpRequestException(content);
                    }
                    string bigCourseJson = await bigCourseResponse.Content.ReadAsStringAsync();
                    var courseModel = JsonConvert.DeserializeObject<CourseModel>(bigCourseJson);
                    if (courseModel is null)
                    {
                        throw new RequestException(HttpStatusCode.InternalServerError, ErrorCodes.ConvertTMSDataModelFail,
                            ErrorMessages.ConvertTMSDataModelFail);
                    }

                    foreach (var courseDetail in courseModel.CourseDetailList)
                    {
                        CourseInformationModel courseInformation = new();
                        if (courseDetail.IsActive == true && 
                            courseDetail.IsDeleted == false &&
                            courseDetail.SubjectDetail != null && 
                            "eL".Equals(courseDetail.Method) &&
                            courseDetail.SubjectDetail.IsActive == true && 
                            courseDetail.SubjectDetail.IsDeleted == false &&
                            //courseDetail.SubjectDetail.SubjectType != null && 
                            //courseDetail.SubjectDetail.SubjectType.Name != null &&
                            //listOfCourseType.Contains(courseDetail.SubjectDetail.SubjectType.Name) &&
                            ((lastSyncTime != null && courseDetail.SubjectDetail.ModifyDate != null)
                                    ? courseDetail.SubjectDetail.ModifyDate > lastSyncTime
                                    : true) &&
                            ((lastSyncTime != null && courseDetail.SubjectDetail.CreatedDate != null)
                                    ? courseDetail.SubjectDetail.CreatedDate > lastSyncTime
                                    : true))
                        {
                            SubjectTypeString subjectTypeString = bigCourse.CourseDetailList.Where(cd => cd.Id == courseDetail.Id)
                                                                    .FirstOrDefault()?.SubjectDetail?.SubjectType;
                            if(subjectTypeString != null && 
                                subjectTypeString.Name != null &&
                                listOfCourseType.Contains(subjectTypeString.Name))
                            {
                                var isExistSubject = _subjectRepository.Get(s => s.Id == courseDetail.SubjectDetail.Id)
                                                                   .Any();
                                if (isExistSubject)
                                {
                                    //get monitor
                                    if (courseDetail.Monitor != null)
                                    {
                                        string staffId = courseDetail.Monitor.StaffId;
                                        User user = _userRepository.Get(u => u.Eid.Equals(staffId)).FirstOrDefault();
                                        if (user != null)
                                        {
                                            courseInformation.MonitorId = user.Id;
                                        }
                                    }
                                    //get list of intructor
                                    if (courseDetail.Instructors != null)
                                    {
                                        List<Guid> listOfInstructorId = new();
                                        foreach (var instructor in courseDetail.Instructors)
                                        {
                                            User user = _userRepository.Get(u => u.Eid.Equals(instructor.StaffId))
                                                .FirstOrDefault();
                                            if (user != null)
                                            {
                                                listOfInstructorId.Add(user.Id);
                                            }
                                        }
                                        courseInformation.ListOfInstructorId = listOfInstructorId;
                                        //get subject id for create user subject
                                        courseInformation.SubjectId = courseDetail.SubjectDetail.Id;
                                    }
                                    //get list of trainee
                                    List<Guid> listOfTraineeId = new();
                                    if (courseDetail.TraineeList.Any())
                                    {
                                        foreach (var trainee in courseDetail.TraineeList)
                                        {
                                            User user = _userRepository.Get(u => u.Eid.Equals(trainee.TraineeDetail.StaffId))
                                                .FirstOrDefault();
                                            if (user != null)
                                            {
                                                listOfTraineeId.Add(user.Id);
                                            }
                                        }
                                        courseInformation.ListOfTraineeId = listOfTraineeId;
                                    }
                                    //get course
                                    Course course = new()
                                    {
                                        Id = courseDetail.Id,
                                        Code = courseDetail.SubjectDetail.Code,
                                        Name = courseDetail.SubjectDetail.Name,
                                        ParentCode = courseModel.Code,
                                        ParentName = courseModel.Name,
                                        NumberOfTrainee = listOfTraineeId.Count,
                                        StartTime = courseModel.StartTime,
                                        EndTime = courseModel.EndTime,
                                        SubjectId = courseDetail.SubjectDetail.Id,
                                        IsActive = true,
                                        IsDeleted = false
                                    };
                                    //string type = courseModel.CourseTypeString?.Name;
                                    string type = courseDetail.SubjectDetail?.SubjectType?.Name;
                                    if (type != null)
                                    {
                                        if (type.Equals("Re-Qualification"))
                                        {
                                            course.Type = CourseType.Re_Qualification;
                                        }
                                        else
                                        {
                                            course.Type = Enum.Parse<CourseType>(type);
                                        }
                                    }
                                    listOfCourse.Add(course);
                                    courseInformation.CourseId = course.Id;
                                }
                            }
                        }
                        if (courseInformation.CourseId != 0)
                        {
                            listOfCourseInformation.Add(courseInformation);
                        }
                    }
                }
            }
            if (listOfCourse.Any())
            {
                await _courseRepository.SynchronizeBulk(listOfCourse);

                listOfCourseInformation = listOfCourseInformation.Distinct(new CourseInformationModelComparer()).ToList();
                var userCourseSubject = ConvertCourseInformationToUserCourse(listOfCourseInformation);
                List<UserCourse> userCourseList = userCourseSubject.UserCourseList;
                List<UserSubject> userSubjectList = userCourseSubject.UserSubjectList.Distinct(new UserSubjectComparer())
                                                                                    .ToList();
                if (lastSyncTime == null)
                {
                    await _userCourseRepository.SynchronizeBulk(userCourseList);
                    await _userSubjectRepository.SynchronizeBulk(userSubjectList);
                }
                else
                {
                    //get all modify user course (include updated user course + new user course)
                    //if user course is found in db, update them, else add new
                    List<UserCourse> addedUserCourseList = new();
                    //check user course is updated
                    bool isUpdatedUserCourse = false;
                    foreach (var userCourse in userCourseList)
                    {
                        UserCourse userCourseTemp = _userCourseRepository.Get(uc =>
                                uc.UserId == userCourse.UserId && uc.CourseId == userCourse.CourseId).FirstOrDefault();
                        if (userCourseTemp != null)
                        {
                            userCourseTemp.ActionType = userCourse.ActionType;
                            isUpdatedUserCourse = true;
                        }
                        else
                        {
                            addedUserCourseList.Add(userCourseTemp);
                        }
                    }

                    //add new user subject
                    List<UserSubject> addedUserSubjectList = new();
                    bool isAddedUserSubject = false;
                    foreach (var userSubject in userSubjectList)
                    {
                        UserSubject userSubjectTemp = _userSubjectRepository.Get(us =>
                                us.UserId == userSubject.UserId && us.SubjectId == userSubject.SubjectId).FirstOrDefault();
                        if (userSubjectTemp == null)
                        {
                            addedUserSubjectList.Add(userSubjectTemp);
                            isAddedUserSubject = true;
                        }
                    }

                    //case list of user course is new
                    if (isUpdatedUserCourse || isAddedUserSubject)
                    {
                        await _unitOfWork.SaveChangeAsync();
                    }
                    if (addedUserCourseList.Any())
                    {
                        await _userCourseRepository.SynchronizeBulk(userCourseList);
                    }
                }

                //get course that start time > now
                var newAddedCourse = new List<Course>();
                foreach (var course in listOfCourse)
                {
                    if (course.StartTime > DateTimeOffset.UtcNow)
                    {
                        newAddedCourse.Add(course);
                    }
                }
                //send mail to teacher and monitor about new course
                foreach (var course in newAddedCourse)
                {
                    await ReminderTeacherJoinCourseByMail(course);
                    await ReminderMonitorJoinCourseByMail(course);
                }
            }
        }
        public async Task ReminderTeacherJoinCourseByMail(Course course)
        {
            var teachers = _userCourseRepository.Get(uc => uc.CourseId == course.Id && uc.ActionType == ActionType.Teach, uc => uc.User);
            if (teachers.Any())
            {
                var teacherList = teachers.ToList();
                foreach (var teacher in teacherList)
                {
                    Message message = new()
                    {
                        To = teacher.User.Email,
                        Subject = "The new course you teach is available!",
                        Content = _mailService.GetEmailTemplate("NewCourseForTeacherTemplate", new TemplateModel()
                        {
                            Name = string.Join(" ", teacher.User.FirstName, teacher.User.LastName),
                            CourseId = course.Id,
                            CourseName = course.Name,
                        })
                    };
                    await _mailService.SendEmailAsync(message);
                }
                //save tracking mail in db
                await _mailService.CreateMail(teacherList.Select(t => t.UserId).ToList(), "Announcement of new " + course.Name + " course for teacher");
            }
        }

        public async Task ReminderMonitorJoinCourseByMail(Course course)
        {
            var monitors = _userCourseRepository.Get(uc => uc.CourseId == course.Id && uc.ActionType == ActionType.Manage, uc => uc.User);
            if (monitors.Any())
            {
                var monitorList = monitors.ToList();
                foreach (var monitor in monitorList)
                {
                    Message message = new()
                    {
                        To = monitor.User.Email,
                        Subject = "The new course you manage is available!",
                        Content = _mailService.GetEmailTemplate("NewCourseForMonitorTemplate", new TemplateModel()
                        {
                            Name = string.Join(" ", monitor.User.FirstName, monitor.User.LastName),
                            CourseId = course.Id,
                            CourseName = course.Name,
                        })
                    };
                    await _mailService.SendEmailAsync(message);
                }
                //save tracking mail in db
                await _mailService.CreateMail(monitorList.Select(t => t.UserId).ToList(), "Announcement of new " + course.Name + " course for monitor");
            }
        }
        //flow sync (db has data):
        //- call api /api/course to get all course without monitor, instructor, trainee for store course table in db
        //- use id of course and course detail (sub-course) to get monitor, instructor, trainee for store user course table
        public async Task SyncCourse()
        {
            HttpClient client = _factory.CreateClient(StringUtils.ClientString);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", _tmsRepository.AccessToken);
            //flag is stop condition to get tms data model, the end point of paging
            bool flag = true;
            //index page in total page
            int index = 1;
            DateTimeOffset startTime = DateTimeOffset.Now;
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            do
            {
                //get list of big course id
                string query = $"/api/course?index={index}&size=23000&isDeleted=false&isActive=true";
                //string query = $"/api/course?index={index}&size=100&isDeleted=false&isActive=true";
                HttpResponseMessage response = await client.GetAsync(query);
                if (!response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException(content);
                }
                statusCode = response.StatusCode;
                string resultJson = await response.Content.ReadAsStringAsync();
                var courseResponse = JsonConvert.DeserializeObject<CourseResponseModel>(resultJson);
                if (courseResponse is null)
                {
                    throw new RequestException(HttpStatusCode.InternalServerError, ErrorCodes.ConvertTMSDataModelFail,
                        ErrorMessages.ConvertTMSDataModelFail);
                }
                var courseModelList = courseResponse.Data;

                //if no course is exist in db, insert all
                if (!_courseRepository.GetAll().Any())
                {
                    await StoreDataSync(courseModelList, client);
                }
                else
                {
                    //insert the new courses, include the assigned users: instructors, trainee, monitor
                    //update the old courses base on the modify date, include the above assigned users
                    //  - get courses have sync time < modify date

                    //get the last time for sync table course
                    SyncLog syncLog = _syncLogRepository.Get(s => s.TableName.Equals("course"))
                                      .OrderByDescending(s => s.Id)
                                      .Take(1).FirstOrDefault();
                    if (syncLog != null)
                    {
                        DateTimeOffset lastSyncTime = syncLog.StartTime;
                        await StoreDataSync(courseModelList, client, lastSyncTime: lastSyncTime);
                    }
                }
                var paging = courseResponse.Paging;
                if (!paging.HasNext)
                {
                    flag = false;
                }
                else
                {
                    index++;
                }
            }
            while (flag);

            await _syncLogRepository.AddAsync(new SyncLog
            {
                StartTime = startTime,
                EndTime = DateTimeOffset.Now,
                StatusCode = (int)statusCode,
                TableName = "course"
            });
            await _unitOfWork.SaveChangeAsync();
        }

        public Task<PagingViewModel<AttendeeSummaryViewModel>> GetAttendeesSummaryProcress(int courseId, AttendeePagingRequestModel requestModel)
        {
            var course = _courseRepository.Get(c => c.Id == courseId && c.IsDeleted != true && c.IsActive != false);
            if (!course.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            var attendees = _userRepository.Get(u => u.Courses.Any(uc => uc.CourseId == courseId
            && uc.ActionType == ActionType.Study), u => u.Courses.Where(uc => uc.CourseId == courseId
            && uc.ActionType == ActionType.Study)).OrderBy(u => u.UserName).AsSplitQuery();
            if (!attendees.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }

            //paging
            int totalRecord = attendees.Count();
            attendees = attendees.Skip((requestModel.CurrentPage - 1) * requestModel.PageSize)
                                        .Take(requestModel.PageSize);

            //mapping
            var items = _mapper.Map<List<AttendeeSummaryViewModel>>(attendees.ToList());
            return Task.FromResult(new PagingViewModel<AttendeeSummaryViewModel>
                                           (items, totalRecord,
                                           requestModel.CurrentPage,
                                           requestModel.PageSize));
        }

        public Task<PagingViewModel<AttendeeViewModel>> GetAttendeesList(int courseId, AttendeePagingRequestModel requestModel)
        {
            var course = _courseRepository.Get(c => c.Id == courseId && c.IsDeleted != true && c.IsActive != false);
            if (!course.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            var attendees = _userRepository.Get(u => u.Courses.Any(uc => uc.CourseId == courseId
             && uc.ActionType == ActionType.Study), u => u.Courses.Where(uc => uc.CourseId == courseId
             && uc.ActionType == ActionType.Study)).OrderBy(u => u.UserName).AsSplitQuery();
            if (!attendees.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }

            //paging
            int totalRecord = attendees.Count();
            attendees = attendees.Skip((requestModel.CurrentPage - 1) * requestModel.PageSize)
                                        .Take(requestModel.PageSize);

            //mapping
            var items = _mapper.Map<List<AttendeeViewModel>>(attendees.ToList());
            return Task.FromResult(new PagingViewModel<AttendeeViewModel>
                                           (items, totalRecord,
                                           requestModel.CurrentPage,
                                           requestModel.PageSize));
        }

        public Task<LearningProgressDetailViewModel> GetAttendeeLearningProgressDetail(int courseTrackingId)
        {
            var userCourse = _userCourseRepository.Get(uc => uc.Id == courseTrackingId
            && uc.ActionType == ActionType.Study && uc.Course.IsActive != false && uc.Course.IsDeleted != true
            , uc => uc.Course);
            if (!userCourse.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            var courseTracking = userCourse.First();

            var learningProgressDetail = _mapper.Map<LearningProgressDetailViewModel>(courseTracking);

            var userId = courseTracking.UserId;
            var course = _courseRepository.Get(c => c.Id == courseTracking.CourseId && c.IsActive != false && c.IsDeleted != true)
                .Include(c => c.Topics).ThenInclude(t => t.TopicOtherLearningResources).ThenInclude(o => o.OtherLearningResource)
                .Include(c => c.Topics).ThenInclude(t => t.TopicOtherLearningResources).ThenInclude(to => to.OLRTrackings.Where(ot => ot.LearnerId == userId))
                .Include(t => t.Topics).ThenInclude(t => t.TopicSCORMs).ThenInclude(o => o.SCORM)
                .Include(t => t.Topics).ThenInclude(t => t.TopicSCORMs).ThenInclude(ts => ts.SCORMCores.Where(sc => sc.LearnerId == userId))
                .Include(c => c.Topics).ThenInclude(t => t.Surveys).ThenInclude(s => s.UserSurveys.Where(us => us.UserId == userId))
                .Include(c => c.Topics).ThenInclude(t => t.Quizzes).ThenInclude(q => q.UserQuizzes.Where(uq => uq.UserId == userId))
                .Include(c => c.Topics).ThenInclude(t => t.TopicTrackings.Where(tt => tt.UserId == userId));

            var courseDetail = _mapper.Map<CourseDetailViewModel>(course.First());
            learningProgressDetail.Topics = courseDetail.Topics;
            return Task.FromResult(learningProgressDetail);
        }

        public async Task UpdateLearningProgressWhenCourseEnd(int courseId)
        {
            var inCompletedAttendees = _userCourseRepository.Get(uc => uc.CourseId == courseId && uc.ActionType == ActionType.Study &&
                (uc.LearningStatus == LearningStatus.Undefined || uc.LearningStatus == LearningStatus.InProgress));

            if (inCompletedAttendees.Any())
            {
                foreach (var attendee in inCompletedAttendees)
                {
                    attendee.LearningStatus = LearningStatus.Failed;
                }
                await _unitOfWork.SaveChangeAsync();
            }
        }

        public async Task ActivateHangfireTest(List<int> courseIds)
        {
            foreach (int courseId in courseIds)
            {
                var course = _courseRepository.FindAsync(courseId).Result;
                await ReminderTeacherJoinCourseByMail(course);
                await ReminderMonitorJoinCourseByMail(course);
            }
            //await Task.WhenAll(ReminderTeacherJoinCourseByMail(course), ReminderMonitorJoinCourseByMail(course));
        }        

        public List<Course> GetUpcomingCourseInToday()
        {
            var result = new List<Course>();
            //get courses with starting at: now < start course < 1 day after
            DateTimeOffset now = DateTimeOffset.Now;
            DateTimeOffset tomorrow = DateTimeOffset.Now.AddDays(1);
            var courses = _courseRepository.Get(c =>  c.StartTime >= now
                                             && c.StartTime < tomorrow
                                             && c.IsActive == true
                                             && c.IsDeleted == false)
                                           .Include(c => c.Users)
                                           .AsSplitQuery();
            if (courses != null && courses.Any())
            {
                result = courses.Where(c => c.Users != null && c.Users.Any()).ToList();
            }
            return result;
        }

        public List<Course> GetEndCourseInToday()
        {
            var result = new List<Course>();
            //get courses with end at: now < end course < 1 day after
            DateTimeOffset now = DateTimeOffset.Now;
            DateTimeOffset tomorrow = DateTimeOffset.Now.AddDays(1);
            var courses = _courseRepository.Get(c => c.EndTime >= now
                                             && c.EndTime < tomorrow
                                             && c.IsActive != false && c.IsDeleted != true
                                             && c.Users != null && c.Users.Any())
                                           .Include(c => c.Users)
                                           .AsSplitQuery()
                                           .Include(c => c.Topics).ThenInclude(c => c.Quizzes)
                                           .Include(c => c.Topics).ThenInclude(c => c.Surveys);
            if (courses.Any())
            {
                result = courses.ToList();
            }
            return result;
        }

        public Task<CourseMarkReportViewModel> GetMarkReport(int courseId)
        {
            var course = _courseRepository.Get(c => c.Id == courseId);
            if (!course.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
           
            course = course.Include(c => c.Topics.OrderBy(t => t.CreateTime))
                .ThenInclude(t => t.Quizzes.Where(q => q.Credit > 0))
                .Include(c => c.Users).ThenInclude(uc => uc.User)
                .Include(c => c.Subject)
                .AsSplitQuery();

            var courseDetail = course.First();
            var markReport = _mapper.Map<CourseMarkReportViewModel>(courseDetail);
            //get all quizzes with credit > 0 in course
            var quizzes = courseDetail.Topics.SelectMany(t => t.Quizzes);

            markReport.QuizGradingInfo = _mapper.Map<List<QuizGradingInfoViewModel>>(quizzes);

            //get all attendees in course
            var attendees = _userRepository.Get(u => u.Courses.Any(uc => uc.CourseId == courseId
             && uc.ActionType == ActionType.Study), 
             u => u.Courses.Where(uc => uc.CourseId == courseId && uc.ActionType == ActionType.Study), 
             u => u.Quizzes.Where(uq => quizzes.Select(q => q.Id).Contains(uq.QuizId)))
                .OrderBy(u => u.UserName).AsSplitQuery();

            var attendeeMarkReports = _mapper.Map<List<AttendeeMarkReportViewModel>>(attendees.ToList());
            markReport.AttendeesMarkResult = attendeeMarkReports;
            return Task.FromResult(markReport);
        }

        public Task<OwnMarkReportViewModel> GetOwnMarkReport(int courseId)
        {
            var userId = _currentUserService.UserId;
            var courseQuery = _courseRepository.Get(c => c.Id == courseId && c.Users.Any(uc => uc.UserId == userId && uc.ActionType == ActionType.Study));
            if (!courseQuery.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            courseQuery = courseQuery.Include(c => c.Users).ThenInclude(uc => uc.User)
                .Include(c => c.Topics.Where(t => t.NumberOfQuizzes > 0 && t.Quizzes.Any(q => q.Credit > 0)))
                .ThenInclude(t => t.Quizzes.Where(q => q.Credit > 0))
                .ThenInclude(q => q.UserQuizzes.Where(uq => uq.UserId == userId))
                .Include(c => c.Subject)
                .AsSplitQuery();
            var course = courseQuery.First();
            var ownMarkReport = _mapper.Map<OwnMarkReportViewModel>(course);
            ownMarkReport.Topics = _mapper.Map<List<TopicWithQuizResultViewModel>>(course.Topics);

            return Task.FromResult(ownMarkReport);
        }

        public Task<List<TopicWithQuizViewModel>> GetGradingInfo(int courseId)
        {
            var course = _courseRepository.Get(c => c.Id == courseId);
            if (!course.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            course = course.Include(c => c.Topics.Where(t => t.NumberOfQuizzes > 0 && t.Quizzes.Any(q => q.Credit > 0)))
               .ThenInclude(t => t.Quizzes.Where(q => q.Credit > 0))
               .AsSplitQuery();
            return Task.FromResult(_mapper.Map<List<TopicWithQuizViewModel>>(course.First().Topics));
        }

        public Task<List<CourseGradeReportViewModel>> GetTackingCoursesGrades()
        {
            var userId = _currentUserService.UserId;
            var courses = _courseRepository.Get(c => c.Users.Any(uc => uc.UserId == userId && uc.ActionType == ActionType.Study),
                c => c.Users.Where(uc => uc.UserId == userId), c => c.Subject).OrderBy(c => c.StartTime)
                .AsSplitQuery();
            if (!courses.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            return Task.FromResult(_mapper.Map<List<CourseGradeReportViewModel>>(courses.ToList()));
        }

        //move sections and learning resources inside them to course
        public async Task<CourseDetailViewModel> MoveSectionsIntoTopics(int courseId, 
            SectionListMovingRequestModel requestModel)
        {
            ValidateUtils.CheckStringNotEmpty("course Id", courseId + "");

            var course = _courseRepository.Get(c => c.Id == courseId)
                                          .Include(c => c.Topics)
                                          .FirstOrDefault();
            if (course == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.CourseNotFound,
                    ErrorMessages.CourseNotFound);
            }

            if(requestModel.Sections != null && requestModel.Sections.Any())
            {
                foreach (var sectionMoving in requestModel.Sections)
                {
                    //filter
                    // - sectionId
                    // - olr(s) and scorm(s) are existed in request
                    //  (resources is selected to moving from section to topic)
                    var section = _sectionRepository
                        .Get(s => s.Id == sectionMoving.SectionId && s.IsDeleted != true)
                        .Include(s => s.OtherLearningResourceList
                            .Where(olr => olr.IsDeleted != true 
                                        && sectionMoving.OtherLearningResourceIds != null
                                        && sectionMoving.OtherLearningResourceIds.Contains(olr.Id)))
                        .AsSplitQuery()
                        .Include(s => s.SCORMList
                            .Where(sc => sc.IsDeleted != true
                                        && sectionMoving.ScormIds != null
                                        && sectionMoving.ScormIds.Contains(sc.Id)))
                        .FirstOrDefault();
                    if(section == null)
                    {
                        throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.SectionNotFound,
                            ErrorMessages.SectionNotFound);
                    }

                    //create topic
                    //get new name for topic from section, if duplicate, automatically increase "abc" -> "abc(1)"
                    List<string> topicNames = course.Topics?.Select(t => t.Name).ToList() ?? new List<string>();
                    string newTopicName = StringUtils.GetUniqueName(topicNames, section.Name);

                    int numberOfOtherLearningResources = section.OtherLearningResourceList?.Count() ?? 0;
                    int numberOfSCORMs = section.SCORMList?.Count() ?? 0;

                    var topic = new Topic
                    {
                        Name = newTopicName,
                        NumberOfLearningResources = numberOfOtherLearningResources + numberOfSCORMs,
                        NumberOfQuizzes = 0,
                        NumberOfSurveys = 0,
                        CourseId = courseId
                    };
                    await _topicRepository.AddAsync(topic);
                    await _unitOfWork.SaveChangeAsync();

                    //create list of topic scorm
                    List<TopicSCORM> topicSCORMs = new();
                    if (numberOfSCORMs > 0)
                    {
                        foreach (var scorm in section.SCORMList)
                        {
                            topicSCORMs.Add(new TopicSCORM
                            {
                                TopicId = topic.Id,
                                SCORMId = scorm.Id,
                                SCORMName = scorm.TitleFromUpload
                            });
                        }
                    }

                    //create list of topic other learning resource
                    List<TopicOtherLearningResource> topicOtherLearningResources = new();
                    if (numberOfOtherLearningResources > 0)
                    {
                        foreach (var olr in section.OtherLearningResourceList)
                        {
                            topicOtherLearningResources.Add(new TopicOtherLearningResource
                            {
                                TopicId = topic.Id,
                                OtherLearningResourceId = olr.Id,
                                OtherLearningResourceName = olr.Title
                            });
                        }
                    }

                    //Additional topic information
                    topic.TopicSCORMs = topicSCORMs;
                    topic.TopicOtherLearningResources = topicOtherLearningResources;
                    await _unitOfWork.SaveChangeAsync();
                }
            }
            //get view model for returning
            var courseDetail = await GetCourseDetail(courseId, _currentUserService.UserId);
            return courseDetail;
        }

        public async Task<CourseDetailViewModel> UpdateDescription(int courseId, CourseUpdateRequestModel requestModel)
        {
            var course = await _courseRepository.FindAsync(courseId);
            if(course == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            course.Description = requestModel.Description;
            await _unitOfWork.SaveChangeAsync();
            var result = await GetCourseDetail(courseId, null);
            return result;
        }

        //update description from subject
        public async Task<CourseDetailViewModel> UpdateCourseFromSubject(int courseId)
        {
            var course = _courseRepository.Get(c => c.Id == courseId, c => c.Subject).FirstOrDefault();
            if (course == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.CourseNotFound, 
                    ErrorMessages.CourseNotFound);
            }
            course.Description = course.Subject.Description ?? "";
            await _unitOfWork.SaveChangeAsync();
            var result = await GetCourseDetail(courseId, null);
            return result;
        }

        public Task<Course> GetCourseWithActivities(int courseId)
        {
            var course = _courseRepository.Get(c => c.Id == courseId && c.IsDeleted == false && c.IsActive == true);
            if (course.Any())
            {
                course = course.Include(c => c.Topics).ThenInclude(t => t.Quizzes)
                    .Include(c => c.Topics).ThenInclude(t => t.Surveys).AsSplitQuery();
            }

            return Task.FromResult(course.FirstOrDefault());
        }
    }
}
