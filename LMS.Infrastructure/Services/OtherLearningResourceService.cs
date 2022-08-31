using AutoMapper;
using LMS.Core.Entity;
using LMS.Core.Enum;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.IServices;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LMS.Core.Application;
using LMS.Core.Models.RequestModels;

namespace LMS.Infrastructure.Services
{
    public class OtherLearningResourceService : IOtherLearningResourceService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IOtherLearningResourceRepository _otherLearningResourcesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITopicRepository _topicRepository;
        private readonly ITopicOtherLearningResourceRepository _topicOtherLearningResourceRepository;
        private readonly IOtherLearningResourceTrackingRepository _otherLearningResourceTrackingRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserCourseRepository _userCourseRepository;
        private readonly ISectionRepository _sectionRepository;

        public OtherLearningResourceService(ISubjectRepository subjectRepository,
                                            IOtherLearningResourceRepository otherLearningResourcesRepository,
                                            IUnitOfWork unitOfWork,
                                            ITopicRepository topicRepository,
                                            ITopicOtherLearningResourceRepository topicOtherLearningResourceRepository,
                                            IOtherLearningResourceTrackingRepository otherLearningResourceTrackingRepository,
                                            IMapper mapper, ICurrentUserService currentUserService, 
                                            IUserCourseRepository userCourseRepository,
                                            ISectionRepository sectionRepository)
        {
            _subjectRepository = subjectRepository;
            _otherLearningResourcesRepository = otherLearningResourcesRepository;
            _unitOfWork = unitOfWork;
            _topicRepository = topicRepository;
            _topicOtherLearningResourceRepository = topicOtherLearningResourceRepository;
            _otherLearningResourceTrackingRepository = otherLearningResourceTrackingRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _userCourseRepository = userCourseRepository;
            _sectionRepository = sectionRepository;
        }

        public async Task<OtherLearningResourceViewModel> UploadOtherLearningResourceInSection(int sectionId,
            IFormFile resource)
        {
            Section section = _sectionRepository.Get(s => s.Id == sectionId && s.IsDeleted != true)
                                                .Include(s => s.OtherLearningResourceList).FirstOrDefault();
            if (section == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.SectionNotFound,
                    ErrorMessages.SectionNotFound);
            }
            string originName = resource.FileName;
            ResourceInfoModel resourceInfo = await FileUtils.SaveFileToFolder(resource);
            FileType fileType = resourceInfo.FileType;
            if (fileType == FileType.pdf || fileType == FileType.video)
            {
                List<string> resourceNameList = section.OtherLearningResourceList.Select(r => r.Title).ToList();
                string title = StringUtils.GetUniqueName(resourceNameList, originName);

                var learningResource = new OtherLearningResource
                {
                    Title = title,
                    PathToFile = resourceInfo.Url,
                    Type = fileType == FileType.pdf ? LearningResourceType.PDF : LearningResourceType.Video,
                    SectionId = sectionId
                };
                await _otherLearningResourcesRepository.AddAsync(learningResource);
                await _unitOfWork.SaveChangeAsync();
                return _mapper.Map<OtherLearningResourceViewModel>(learningResource);
            }
            else
            {
                throw new RequestException(HttpStatusCode.UnsupportedMediaType, ErrorCodes.UnsupportedFile,
                        ErrorMessages.UnsupportedFile);
            }
        }

        public async Task<TopicOLRWithoutTrackingViewModel> UploadOtherLearningResourceInTopic(int topicId,
            IFormFile resource)
        {
            Topic topic = _topicRepository.Get(t => t.Id == topicId)?
                                          .Include(t => t.TopicOtherLearningResources).FirstOrDefault();
            if (topic == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            string originName = resource.FileName;
            ResourceInfoModel resourceInfo = await FileUtils.SaveFileToFolder(resource);
            FileType fileType = resourceInfo.FileType;
            if (fileType == FileType.pdf || fileType == FileType.video)
            {
                List<string> resourceNameList = topic.TopicOtherLearningResources.Select(r => r.OtherLearningResourceName)
                                                                            .ToList();
                string title = StringUtils.GetUniqueName(resourceNameList, originName);

                var learningResource = new OtherLearningResource
                {
                    Title = title,
                    PathToFile = resourceInfo.Url,
                    Type = fileType == FileType.pdf ? LearningResourceType.PDF : LearningResourceType.Video
                };
                await _otherLearningResourcesRepository.AddAsync(learningResource);
                await _unitOfWork.SaveChangeAsync();

                TopicOtherLearningResource topicOLR = new()
                {
                    TopicId = topicId,
                    OtherLearningResourceId = learningResource.Id,
                    OtherLearningResourceName = title
                };

                await _topicOtherLearningResourceRepository.AddAsync(topicOLR);

                //update number of learning resources in topic
                topic.NumberOfLearningResources++;

                await _unitOfWork.SaveChangeAsync();
                return new TopicOLRWithoutTrackingViewModel
                {
                    TopicId = topicId,
                    OtherLearningResourceId = learningResource.Id,
                    TopicOtherLearningResourceId = topicOLR.Id,
                    OtherLearningResourceName = title,
                    PathToFile = learningResource.PathToFile,
                    Type = learningResource.Type,
                    CreateTime = learningResource.CreateTime,
                    CreateBy = learningResource.CreateBy,
                    CompletionThreshold = topicOLR.CompletionThreshold
                };
            }
            else
            {
                throw new RequestException(HttpStatusCode.UnsupportedMediaType, ErrorCodes.UnsupportedFile,
                        ErrorMessages.UnsupportedFile);
            }
        }

        //public async Task<TopicOLRListViewModel> MoveOtherLearningResourcesToTopic(
        //    OtherLearningResourceListMovingRequestModel requestModel)
        //{
        //    Topic topic = _topicRepository.Get(t => t.Id == requestModel.TopicId)
        //                                  .Include(t => t.TopicOtherLearningResources)
        //                                  .ThenInclude(tolr => tolr.OtherLearningResource)
        //                                  .FirstOrDefault();
        //    if (topic == null)
        //    {
        //        throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.NotFound, ErrorMessages.NotFound);
        //    }

        //    //check existed resource in topic by resource id
        //    List<TopicOLRDetailViewModel> additionalTopicOLRDetails = new();
        //    if (topic.TopicOtherLearningResources != null && requestModel.ResourceIds != null)
        //    {
        //        if(topic.TopicOtherLearningResources.Any(tolr => 
        //            requestModel.ResourceIds.Contains(tolr.OtherLearningResourceId)))
        //        {
        //            throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.ResourceIsExistedInTopic,
        //                ErrorMessages.ResourceIsExistedInTopic);
        //        }
                
        //        foreach (var resourceId in requestModel.ResourceIds)
        //        {
        //            OtherLearningResource resource = _otherLearningResourcesRepository.Get(r => 
        //                        r.Id == resourceId && r.IsActive != false && r.IsDeleted != true)
        //                .FirstOrDefault();
        //            if(resource == null)
        //            {
        //                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.ResourceNotAvaiable,
        //                ErrorMessages.ResourceNotAvaiable);
        //            }

        //            //get new resource name in topic
        //            List<string> resourceNameList = topic.TopicOtherLearningResources.Select(r => 
        //                                r.OtherLearningResourceName).ToList();
        //            string otherLearningResourceName = StringUtils.GetUniqueName(resourceNameList, resource.Title);

        //            TopicOtherLearningResource topicOLR = new()
        //            {
        //                TopicId = requestModel.TopicId,
        //                OtherLearningResourceId = resourceId,
        //                OtherLearningResourceName = otherLearningResourceName
        //            };

        //            topic.TopicOtherLearningResources.Add(topicOLR);

        //            //update number of learning resources in topic
        //            topic.NumberOfLearningResources++;

        //            //await _unitOfWork.SaveChangeAsync();

        //            //get topic olr detail for view model
        //            additionalTopicOLRDetails.Add(new TopicOLRDetailViewModel
        //            {
        //                OtherLearningResourceId = resourceId,
        //                TopicOtherLearningResourceId = topicOLR.Id,
        //                OtherLearningResourceName = otherLearningResourceName,
        //                PathToFile = resource.PathToFile,
        //                Type = resource.Type,
        //                CreateTime = resource.CreateTime,
        //                CreateBy = resource.CreateBy
        //            });
        //        }
        //        await _unitOfWork.SaveChangeAsync();
        //    }
            
        //    return new TopicOLRListViewModel
        //    {
        //        TopicId = requestModel.TopicId,
        //        AdditionalTopicOLRDetails = additionalTopicOLRDetails
        //    };
        //}

        public async Task DeleteOtherLearningResourceInTopic(int topicOLRId)
        {
            var topicResource = _topicOtherLearningResourceRepository.Get(tr => tr.Id == topicOLRId)
                                                                     .Include(tr => tr.Topic).ThenInclude(t => t.Course)
                                                                     .Include(tr => tr.OtherLearningResource).FirstOrDefault();
            if (topicResource == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.ParametersNotMatch,
                    ErrorMessages.ParametersNotMatch);
            }

            Topic topic = topicResource.Topic;
            OtherLearningResource resource = topicResource.OtherLearningResource;

            DateTimeOffset now = DateTimeOffset.Now;
            if (topic.Course.StartTime <= now)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.ResourceDeleteError,
                    ErrorMessages.ResourceDeleteError);
            }


            if (resource.SectionId == null)
            {
                await _otherLearningResourcesRepository.Remove(resource.Id);
                FileUtils.DeleteOtherLearningResourceFile(resource.PathToFile);
            }
            else
            {
                await _topicOtherLearningResourceRepository.Remove(topicResource.Id);
            }

            //update number of learning resources in topic
            topic.NumberOfLearningResources--;

            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteOtherLearningResourceInSection(int OLRId)
        {
            OtherLearningResource resource = _otherLearningResourcesRepository.Get(r => r.Id == OLRId && r.SectionId != null)
                .Include(r => r.TopicOtherLearningResources)
                .ThenInclude(tr => tr.Topic).ThenInclude(topic => topic.Course)
                .FirstOrDefault();
            if (resource == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }

            //this variable change to true if resource is existed in a course has started
            bool flag = false;
            if (resource.TopicOtherLearningResources.Any())
            {
                DateTimeOffset now = DateTimeOffset.Now;
                List<TopicOtherLearningResource> deleteTopicOLR = new();
                foreach (var topicResource in resource.TopicOtherLearningResources)
                {
                    if (topicResource.Topic.Course.StartTime <= now) //course is started
                    {
                        flag = true;
                    }
                    else //course is not start
                    {
                        deleteTopicOLR.Add(topicResource);
                    }
                }
                //delete all topicOLR in courses that not start 
                if (deleteTopicOLR.Count > 0)
                {
                    _topicOtherLearningResourceRepository.RemoveRange(deleteTopicOLR);

                    await _unitOfWork.SaveChangeAsync();

                    //update number of learning resources in topic
                    await UpdateNumOfLearningResourceInTopics(deleteTopicOLR.Select(to => to.TopicId).ToList());
                }
                if (flag)
                {
                    resource.IsDeleted = true;
                    _otherLearningResourcesRepository.Update(resource);
                }
                else
                {
                    await _otherLearningResourcesRepository.Remove(resource.Id);
                }
            }
            else
            {
                await _otherLearningResourcesRepository.Remove(resource.Id);
            }
            await _unitOfWork.SaveChangeAsync();
            if (!flag)
            {
                FileUtils.DeleteOtherLearningResourceFile(resource.PathToFile);
            }
        }

        public async Task<OtherLearningResourceViewContentModel> ViewContent(int topicOtherLearningResourceId)
        {
            var topicResource = _topicOtherLearningResourceRepository.Get(tr => tr.Id == topicOtherLearningResourceId)?
                                                                    .Include(tr => tr.OtherLearningResource)?
                                                                    .Include(tr => tr.Topic).AsSplitQuery()
                                                                    .FirstOrDefault();

            if (topicResource == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.ParametersNotMatch,
                    ErrorMessages.ParametersNotMatch);
            }
            Guid userId = _currentUserService.UserId;
            ActionType type = _userCourseRepository.Get(uc => uc.UserId == userId
                                                     && uc.CourseId == topicResource.Topic.CourseId)
                                                    .Select(uc => uc.ActionType).FirstOrDefault();
            if (type == ActionType.Study)
            {
                var OLRTracking = _otherLearningResourceTrackingRepository.Get(ot => ot.LearnerId == userId
                && ot.TopicOtherLearningResourceId == topicOtherLearningResourceId).FirstOrDefault();
                if (OLRTracking == null)
                {
                    OLRTracking = new OtherLearningResourceTracking
                    {
                        TopicOtherLearningResourceId = topicOtherLearningResourceId,
                        LearnerId = userId,
                        IsCompleted = false
                    };
                    await _otherLearningResourceTrackingRepository.AddAsync(OLRTracking);
                    await _unitOfWork.SaveChangeAsync();
                }
                return new OtherLearningResourceViewContentModel
                {
                    PathToFile = topicResource.OtherLearningResource.PathToFile,
                    CompletionThreshold = topicResource.CompletionThreshold,
                    OLRTracking = _mapper.Map<OtherLearningResourceTrackingViewModel>(OLRTracking)
                };
            }
            else
            {
                return new OtherLearningResourceViewContentModel
                {
                    PathToFile = topicResource.OtherLearningResource.PathToFile
                };
            }
        }

        private async Task UpdateNumOfLearningResourceInTopics(List<int> topicIds)
        {
            //get topics that have deleted OLR
            var topics = _topicRepository.Get(t => topicIds.Contains(t.Id))
                .Include(t => t.TopicOtherLearningResources).ThenInclude(to => to.OtherLearningResource)
                .Include(t => t.TopicSCORMs).ThenInclude(ts => ts.SCORM)
                .AsSplitQuery().ToList();
            if (topics.Any())
            {
                foreach (var topic in topics)
                {
                    var availableScorm = topic.TopicSCORMs.Where(ts => ts.SCORM.IsDeleted != true);
                    var availableOLR = topic.TopicOtherLearningResources.Where(tolr => tolr.OtherLearningResource.IsDeleted != true);
                    topic.NumberOfLearningResources = availableScorm.Count() + availableOLR.Count();
                }
                await _unitOfWork.SaveChangeAsync();
            }
        }

        public async Task<TopicOLRWithoutTrackingViewModel> UpdateOtherLearningResourceInTopic(int topicOtherLearningResourceId, TopicOLRUpdateRequestModel requestModel)
        {
            ValidateUtils.CheckStringNotEmpty("resource name", requestModel.OtherLearningResourceName);
            var topicOLR = _topicOtherLearningResourceRepository.FindAsync(topicOtherLearningResourceId).Result;
            if (topicOLR == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }

            //update
            topicOLR.CompletionThreshold = requestModel.CompletionThreshold;
            topicOLR.OtherLearningResourceName = requestModel.OtherLearningResourceName;
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<TopicOLRWithoutTrackingViewModel>(topicOLR);
        }
    }
}
