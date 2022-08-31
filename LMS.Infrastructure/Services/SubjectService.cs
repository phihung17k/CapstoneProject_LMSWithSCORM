using AutoMapper;
using LMS.Core.Entity;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IRepositories;
using LMS.Core.Models.RequestModels;
using LMS.Core.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using LMS.Infrastructure.Utils;
using System.Net.Http.Headers;
using LMS.Infrastructure.Data;
using LMS.Core.Models.TMSResponseModel;
using Newtonsoft.Json;
using LMS.Infrastructure.IServices;
using LMS.Core.Enum;
using LMS.Core.Models.RequestModels.SubjectRequestModel;

namespace LMS.Infrastructure.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _factory;
        private readonly TMSRepository _tmsRepository;
        private readonly ISyncLogRepository _syncLogRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseRepository _courseRepository;
        private readonly ITopicRepository _topicRepository;

        public SubjectService(ISubjectRepository subjectRepository, IMapper mapper, IHttpClientFactory factory, 
            TMSRepository tmsRepository, ISyncLogRepository syncLogRepository, IUnitOfWork unitOfWork,
            ICourseRepository courseRepository, ITopicRepository topicRepository)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
            _factory = factory;
            _tmsRepository = tmsRepository;
            _syncLogRepository = syncLogRepository;
            _unitOfWork = unitOfWork;
            _courseRepository = courseRepository;
            _topicRepository = topicRepository;
        }

        public Task<PagingViewModel<SubjectViewModel>> GetSubjectList(SubjectPagingRequestModel requestModel, Guid? userId)
        {
            //search
            var result = _subjectRepository.Get(s =>
                (requestModel.Search != null ? (s.Code.ToLower().Contains(requestModel.Search.ToLower()) || s.Name.ToLower().Contains(requestModel.Search.ToLower())) : true) &&
                (requestModel.IsActive != null ? s.IsActive == requestModel.IsActive : true) &&
                s.IsDeleted != true &&
                (userId != null ? s.Users.Any(u => u.UserId == userId) : true), s => s.Users);

            if (!result.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }

            //default sort
            result = result.OrderBy(s => s.Code);

            //paging
            int totalRecord = result.Count();
            result = result.Skip((requestModel.CurrentPage - 1) * requestModel.PageSize)
                                        .Take(requestModel.PageSize);

            //include teachers
            result = result.Include(s => s.Users).ThenInclude(u => u.User).AsSplitQuery();

            //mapping
            var items = _mapper.Map<List<SubjectViewModel>>(result.ToList());
            if (!items.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            return Task.FromResult(new PagingViewModel<SubjectViewModel>
                                           (items, totalRecord,
                                           requestModel.CurrentPage,
                                           requestModel.PageSize));
        }

        public Task<SubjectViewModel> GetDetailSubject(int subjectId)
        {
            var subjectDetail = _subjectRepository.Get(s => s.Id == subjectId)
                                                  .Include(s => s.Sections.Where(se => se.IsDeleted != true))
                                                  .ThenInclude(se => se.OtherLearningResourceList
                                                    .Where(olr => olr.IsDeleted != true))
                                                  .AsSplitQuery()
                                                  .Include(s => s.Sections.Where(se => se.IsDeleted != true))
                                                  .ThenInclude(se => se.SCORMList.Where(sc => sc.IsDeleted != true))
                                                  .Include(s => s.Users)
                                                  .ThenInclude(u => u.User)
                                                  .AsSplitQuery().FirstOrDefault();
            if (subjectDetail == null || subjectDetail.IsDeleted == true)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            return Task.FromResult(_mapper.Map<SubjectViewModel>(subjectDetail));
        }

        //get list of section and resource inside it
        //if resource in section is existed in course, => add isMoved field = true in resource in that section
        public Task<SubjectWithSectionsViewModel> GetSectionsAndResourcesInCourse(int subjectId, int courseId)
        {
            Subject subject = _subjectRepository.Get(s => s.Id == subjectId)
                                                .Include(s => s.Sections
                                                    .Where(se => se.IsDeleted != true))
                                                .ThenInclude(se => se.OtherLearningResourceList
                                                    .Where(olr => olr.IsDeleted != true))
                                                .AsSplitQuery()
                                                .Include(s => s.Sections
                                                    .Where(se => se.IsDeleted != true))
                                                .ThenInclude(se => se.SCORMList
                                                    .Where(sc => sc.IsDeleted != true))
                                                .FirstOrDefault();
            if (subject == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.SubjectNotFound, 
                    ErrorMessages.SubjectNotFound);
            }

            Course course = _courseRepository.Get(c => c.Id == courseId)
                                             .Include(c => c.Topics)
                                             .ThenInclude(t => t.TopicOtherLearningResources)
                                             .AsSplitQuery()
                                             .Include(c => c.Topics)
                                             .ThenInclude(t => t.TopicSCORMs)
                                             .FirstOrDefault();
            if(course == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.CourseNotFound, ErrorMessages.CourseNotFound);
            }

            //get resource ids in course
            List<int> otherLearningResourceIds = new();
            List<int> scormIds = new();
            if(course.Topics != null)
            {
                foreach (var topic in course.Topics)
                {
                    if(topic.TopicOtherLearningResources != null)
                    {
                        List<int> olrIdList = topic.TopicOtherLearningResources.Select(tolr => 
                            tolr.OtherLearningResourceId).ToList();
                        otherLearningResourceIds.AddRange(olrIdList);
                    }
                    if (topic.TopicSCORMs != null)
                    {
                        List<int> scormIdList = topic.TopicSCORMs.Select(tolr => tolr.SCORMId).ToList();
                        scormIds.AddRange(scormIdList);
                    }
                }
            }
            //remove duplicate
            otherLearningResourceIds = otherLearningResourceIds.Distinct().ToList();
            scormIds = scormIds.Distinct().ToList();

            var result = new SubjectWithSectionsViewModel();
            result.SubjectCode = subject.Code;
            result.SubjectName = subject.Name;
            if(subject.Sections != null && subject.Sections.Any())
            {
                result.Sections = _mapper.Map<List<SectionWithResourcesViewModel>>(subject.Sections.ToList());
                foreach (var section in result.Sections)
                {
                    foreach (var olr in section.OtherLearningResourceList)
                    {
                        if (otherLearningResourceIds.Contains(olr.Id))
                        {
                            olr.IsMoved = true;
                        }
                    }
                    foreach (var scorm in section.SCORMList)
                    {
                        if (scormIds.Contains(scorm.Id))
                        {
                            scorm.IsMoved = true;
                        }
                    }
                }
            }
            return Task.FromResult(result);
        }

        //get list of section and resource inside it
        //if resource in section is existed in topic, => add isMoved field = true in resource in that section
        public Task<SubjectWithSectionsViewModel> GetSectionsAndResourcesInTopic(int subjectId, int topicId)
        {
            Subject subject = _subjectRepository.Get(s => s.Id == subjectId)
                                                .Include(s => s.Sections
                                                    .Where(se => se.IsDeleted != true))
                                                .ThenInclude(se => se.OtherLearningResourceList
                                                    .Where(olr => olr.IsDeleted != true))
                                                .AsSplitQuery()
                                                .Include(s => s.Sections
                                                    .Where(se => se.IsDeleted != true))
                                                .ThenInclude(se => se.SCORMList
                                                    .Where(sc => sc.IsDeleted != true))
                                                .FirstOrDefault();
            if (subject == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.SubjectNotFound,
                    ErrorMessages.SubjectNotFound);
            }

            Topic topic = _topicRepository.Get(t => t.Id == topicId)
                                             .Include(t => t.TopicOtherLearningResources)
                                             .AsSplitQuery()
                                             .Include(t => t.TopicSCORMs)
                                             .FirstOrDefault();
            if (topic == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.TopicNotFound, 
                    ErrorMessages.TopicNotFound);
            }

            //get resource ids in course
            List<int> otherLearningResourceIds = new();
            List<int> scormIds = new();
            if (topic.TopicOtherLearningResources != null)
            {
                List<int> olrIdList = topic.TopicOtherLearningResources.Select(tolr =>
                    tolr.OtherLearningResourceId).ToList();
                otherLearningResourceIds.AddRange(olrIdList);
            }
            if (topic.TopicSCORMs != null)
            {
                List<int> scormIdList = topic.TopicSCORMs.Select(tolr => tolr.SCORMId).ToList();
                scormIds.AddRange(scormIdList);
            }

            //remove duplicate
            otherLearningResourceIds = otherLearningResourceIds.Distinct().ToList();
            scormIds = scormIds.Distinct().ToList();

            var result = new SubjectWithSectionsViewModel();
            result.SubjectCode = subject.Code;
            result.SubjectName = subject.Name;
            if (subject.Sections != null && subject.Sections.Any())
            {
                result.Sections = _mapper.Map<List<SectionWithResourcesViewModel>>(subject.Sections.ToList());
                foreach (var section in result.Sections)
                {
                    foreach (var olr in section.OtherLearningResourceList)
                    {
                        if (otherLearningResourceIds.Contains(olr.Id))
                        {
                            olr.IsMoved = true;
                        }
                    }
                    foreach (var scorm in section.SCORMList)
                    {
                        if (scormIds.Contains(scorm.Id))
                        {
                            scorm.IsMoved = true;
                        }
                    }
                }
            }
            return Task.FromResult(result);
        }

        private async Task<HttpStatusCode> StoreDataSync(HttpClient client, DateTimeOffset? lastSyncTime = null)
        {
            List<SubjectResponseModel> subjectResponseList = new();
            //flag is stop condition to get tms data model, the end point of paging
            bool flag = true;
            //index page in total page
            int index = 1;
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            //list contains subject type that support in lms
            List<string> listOfSubjectType = new List<string>
            {
                SubjectType.Initial.ToString(),
                SubjectType.Recurrent.ToString(),
                SubjectType.Bridge.ToString(),
                SubjectType.Upgrade.ToString(),
                "Re-Qualification"
            };
            do
            {
                string query = $"/api/subject?index={index}&size=5000&isActive=true";
                HttpResponseMessage response = await client.GetAsync(query);
                if (!response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException(content);
                }
                statusCode = response.StatusCode;
                string resultJson = await response.Content.ReadAsStringAsync();
                var subjectResponse = JsonConvert.DeserializeObject<SubjectResponseModel>(resultJson);
                if (subjectResponse is null)
                {
                    throw new RequestException(HttpStatusCode.InternalServerError, ErrorCodes.ConvertTMSDataModelFail,
                        ErrorMessages.ConvertTMSDataModelFail);
                }
                var subjectDetails = subjectResponse.Data;
                var paging = subjectResponse.Paging;
                if (!paging.HasNext)
                {
                    flag = false;
                }
                else
                {
                    index++;
                }
                subjectDetails = subjectDetails.Where(sd => sd.IsActive == true
                                                    && sd.IsDeleted == false
                                                    && sd.PassScore != null 
                                                    && sd.PassScore > 0
                                                    && sd.SubjectType != null
                                                    && sd.SubjectType.Name != null
                                                    && listOfSubjectType.Contains(sd.SubjectType.Name)
                       && ((lastSyncTime != null && sd.ModifyDate != null) ? sd.ModifyDate > lastSyncTime: true)
                       && ((lastSyncTime != null && sd.CreatedDate != null)? sd.CreatedDate > lastSyncTime: true))
                                               .ToList();
                List<Subject> subjectList = _mapper.Map<List<Subject>>(subjectDetails);
                await _subjectRepository.SynchronizeBulk(subjectList);
            }
            while (flag);
            return statusCode;
        }

        public async Task SyncSubject()
        {
            HttpClient client = _factory.CreateClient(StringUtils.ClientString);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", 
                _tmsRepository.AccessToken);
            DateTimeOffset startTime = DateTimeOffset.Now;
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            if (!_subjectRepository.GetAll().Any())
            {
                statusCode = await StoreDataSync(client);
            }
            else
            {
                //get the last time for sync table course
                SyncLog syncLog = _syncLogRepository.Get(s => s.TableName.Equals("subject"))
                                  .OrderByDescending(s => s.Id)
                                  .Take(1).FirstOrDefault();
                if (syncLog != null)
                {
                    DateTimeOffset lastSyncTime = syncLog.StartTime;
                    statusCode = await StoreDataSync(client, lastSyncTime: lastSyncTime);
                }
            }

            await _syncLogRepository.AddAsync(new SyncLog
            {
                StartTime = startTime,
                EndTime = DateTimeOffset.Now,
                StatusCode = (int)statusCode,
                TableName = "subject",
            });
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<SubjectViewModelWithoutSection> UpdateDescription(int subjectId, SubjectUpdateRequestModel requestModel)
        {
            var subject = _subjectRepository.Get(s => s.Id == subjectId)
                .Include(s => s.Users).ThenInclude(us => us.User)
                .FirstOrDefault();
            if (subject == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            subject.Description = requestModel.Description;
            await _unitOfWork.SaveChangeAsync();
            var result = _mapper.Map<SubjectViewModelWithoutSection>(subject);
            return result;
        }
    }
}
