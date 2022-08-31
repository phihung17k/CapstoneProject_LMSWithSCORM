using AutoMapper;
using LMS.Core.Entity;
using LMS.Core.Models.RequestModels;
using LMS.Core.Models.RequestModels.LearningResourceRequestModel;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.IServices;
using LMS.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseRepository _courseRepository;
        private readonly ISCORMService _scormService;
        private readonly IOtherLearningResourceService _olrService;
        private readonly IOtherLearningResourceRepository _otherLearningResourcesRepository;
        private readonly ISCORMRepository _scormRepository;


        public TopicService(ITopicRepository topicRepository, IMapper mapper, IUnitOfWork unitOfWork, 
            ICourseRepository courseRepository, ISCORMService scormService, IOtherLearningResourceService olrService,
            IOtherLearningResourceRepository otherLearningResourcesRepository, ISCORMRepository scormRepository)
        {
            _topicRepository = topicRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _courseRepository = courseRepository;
            _scormService = scormService;
            _olrService = olrService;
            _otherLearningResourcesRepository = otherLearningResourcesRepository;
            _scormRepository = scormRepository;
        }

        public async Task<TopicViewModelWithoutResource> CreateTopic(TopicCreateRequestModel topicRequestModel)
        {
            // Validate request data
            var course = _courseRepository.Get(c => c.Id == topicRequestModel.CourseId,
                c => c.Topics).FirstOrDefault();
            if (course == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            ValidateUtils.CheckStringNotEmpty("topic.Name", topicRequestModel.Name);
            ValidateUtils.CheckStringNotEmpty("topic.Course", topicRequestModel.CourseId + "");

            //check duplicate topic name
            if(course.Topics != null && course.Topics.Any())
            {
                bool isExistTopic = course.Topics.Any(t => t.Name.Trim().ToLower().
                                    Equals(topicRequestModel.Name.Trim().ToLower()));
                if (isExistTopic)
                {
                    throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.TopicIsExisted, 
                        ErrorMessages.TopicIsExisted);
                }
            }

            // Add topic
            var topic = _mapper.Map<Topic>(topicRequestModel);
            await _topicRepository.AddAsync(topic);

            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<TopicViewModelWithoutResource>(topic);
        }

        public async Task Delete(int topicId)
        {
            var topic = _topicRepository.Get(c => c.Id == topicId, c => c.Course)
                                        .Include(t => t.TopicOtherLearningResources)
                                        .Include(t => t.TopicSCORMs)
                                        .AsSplitQuery()
                                        .FirstOrDefault();
            if (topic == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            var checkCourse = topic.Course;
            if (checkCourse.StartTime < DateTimeOffset.Now && topic.CreateTime <= checkCourse.StartTime)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.CourseIsStarted, ErrorMessages.CourseIsStarted);

            }

            //delete learning resource in topic
            if (topic.TopicOtherLearningResources != null && topic.TopicOtherLearningResources.Any())
            {
                foreach (var topicOtherLearningResource in topic.TopicOtherLearningResources.ToList())
                {
                    await _olrService.DeleteOtherLearningResourceInTopic(topicOtherLearningResource.Id);
                }
            }
            if (topic.TopicSCORMs != null && topic.TopicSCORMs.Any())
            {
                foreach (var topicSCORM in topic.TopicSCORMs.ToList())
                {
                    await _scormService.DeleteSCORMInTopic(topicSCORM.Id);
                }
            }

            await _topicRepository.Remove(topicId);

            ////update number of topics in course
            //checkCourse.NumberOfTopics--;
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<TopicViewModelWithoutResource> Update(int topicId, TopicUpdateRequestModel topicRequestModel)
        {
            // Validate request data
            ValidateUtils.CheckStringNotEmpty("topic.Name", topicRequestModel.Name);
            //var topicDB = await _topicRepository.FindAsync(topicId);
            var topicDB = _topicRepository.Get(t => t.Id == topicId)
                                          .Include(t => t.Course)
                                          .ThenInclude(c => c.Topics)
                                          .FirstOrDefault();
            if (topicDB == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            IEnumerable<Topic> listOfTopic = topicDB.Course.Topics;
            bool isExistedTopic = listOfTopic.Where(t => t.Id != topicId
                                && t.Name.Trim().ToLower().Equals(topicRequestModel.Name.Trim().ToLower()))
                                             .Any();
            if (isExistedTopic)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.TopicIsExisted,
                        ErrorMessages.TopicIsExisted);
            }
            _mapper.Map(topicRequestModel, topicDB);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<TopicViewModelWithoutResource>(topicDB);
        }

        public async Task<TopicViewModelWithResource> MoveLearningResourcesToTopic(int topicId,
            LearningResourceListMovingRequestModel requestModel)
        {
            Topic topic = _topicRepository.Get(t => t.Id == topicId)
                                          .Include(t => t.TopicOtherLearningResources)
                                          .ThenInclude(tolr => tolr.OtherLearningResource)
                                          .AsSplitQuery()
                                          .Include(t => t.TopicSCORMs)
                                          .ThenInclude(ts => ts.SCORM)
                                          .FirstOrDefault();
            if (topic == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }

            List<TopicOLRDetailViewModel> additionalTopicOLRDetails = new();
            if (requestModel.Resources != null)
            {
                //get resource id in request
                List<int> otherLearningResourceIds = requestModel.Resources?.Where(r => !r.IsSCORM)
                                                                           .Select(r => r.ResourceId)
                                                                           .ToList() ?? new List<int>();
                List<int> scormIds = requestModel.Resources?.Where(r => r.IsSCORM)
                                                            .Select(r => r.ResourceId)
                                                            .ToList() ?? new List<int>();

                foreach (var resourceModel in requestModel.Resources)
                {
                    if (resourceModel.IsSCORM)
                    {
                        //check existed resource in topic by resource id
                        if (topic.TopicSCORMs.Any(ts => scormIds.Contains(ts.SCORMId)))
                        {
                            throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.ResourceIsExistedInTopic,
                                ErrorMessages.ResourceIsExistedInTopic);
                        }

                        var resource = _scormRepository.Get(r =>
                                    r.Id == resourceModel.ResourceId && r.IsDeleted != true)
                            .FirstOrDefault();
                        if (resource == null)
                        {
                            throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.ResourceNotAvaiable,
                            ErrorMessages.ResourceNotAvaiable);
                        }

                        //get new resource name in topic
                        List<string> resourceNameList = topic.TopicSCORMs.Select(r => r.SCORMName).ToList();
                        string scormName = StringUtils.GetUniqueName(resourceNameList, 
                            resource.TitleFromUpload);

                        TopicSCORM topicSCORM = new TopicSCORM
                        {
                            TopicId = topicId,
                            SCORMId = resourceModel.ResourceId,
                            SCORMName = scormName
                        };
                        topic.TopicSCORMs.Add(topicSCORM);

                        //update number of learning resources in topic
                        topic.NumberOfLearningResources++;

                        //remove scormId in scormIds (request)
                        scormIds.Remove(resourceModel.ResourceId);
                    }
                    else
                    {
                        //check existed resource in topic by resource id
                        if (topic.TopicOtherLearningResources.Any(tolr =>
                                otherLearningResourceIds.Contains(tolr.OtherLearningResourceId)))
                        {
                            throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.ResourceIsExistedInTopic,
                                ErrorMessages.ResourceIsExistedInTopic);
                        }

                        var resource = _otherLearningResourcesRepository.Get(r =>
                                    r.Id == resourceModel.ResourceId && r.IsDeleted != true)
                            .FirstOrDefault();
                        if (resource == null)
                        {
                            throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.ResourceNotAvaiable,
                            ErrorMessages.ResourceNotAvaiable);
                        }

                        //get new resource name in topic
                        List<string> resourceNameList = topic.TopicOtherLearningResources.Select(r =>
                                            r.OtherLearningResourceName).ToList();
                        string otherLearningResourceName = StringUtils.GetUniqueName(resourceNameList, resource.Title);

                        TopicOtherLearningResource topicOLR = new()
                        {
                            TopicId = topicId,
                            OtherLearningResourceId = resourceModel.ResourceId,
                            OtherLearningResourceName = otherLearningResourceName
                        };
                        topic.TopicOtherLearningResources.Add(topicOLR);

                        //update number of learning resources in topic
                        topic.NumberOfLearningResources++;

                        //remove other learning resource id in olr Ids (request)
                        otherLearningResourceIds.Remove(resourceModel.ResourceId);
                    }
                }
                await _unitOfWork.SaveChangeAsync();
            }
            return _mapper.Map<TopicViewModelWithResource>(topic);
        }
    }
}