using AutoMapper;
using LMS.Core.Application;
using LMS.Core.Entity;
using LMS.Core.Enum;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.IServices;
using LMS.Core.Models.SCORMModels;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static LMS.Core.Common.SCORMConstants;
using LMS.Core.Models.RequestModels;

namespace LMS.Infrastructure.Services
{
    public class SCORMService : ISCORMService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ISCORMRepository _scormRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITopicRepository _topicRepository;
        private readonly ITopicSCORMRepository _topicSCORMRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserCourseRepository _userCourseRepository;
        private readonly ISCORMCoreRepository _scormCoreRepo;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ISCORMObjectiveRepository _scormObjRepo;
        private readonly ISCORMLearnerPreferenceRepository _learnerPrefRepo;
        private readonly ISCORMNavigationRepository _scormNavRepo;
        private readonly ISectionRepository _sectionRepository;

        public SCORMService(ISubjectRepository subjectRepository, ISCORMRepository scormRepository, IUnitOfWork unitOfWork,
            ITopicRepository topicRepository, ITopicSCORMRepository topicSCORMRepository,
            ICurrentUserService currentUserService, IUserCourseRepository userCourseRepository,
            ISCORMCoreRepository scormCoreRepo, IMapper mapper, IUserRepository userRepository, ISCORMObjectiveRepository scormObjRepo,
            ISCORMLearnerPreferenceRepository learnerPrefRepo, ISCORMNavigationRepository scormNavRepo,
            ISectionRepository sectionRepository)
        {
            _subjectRepository = subjectRepository;
            _scormRepository = scormRepository;
            _unitOfWork = unitOfWork;
            _topicRepository = topicRepository;
            _topicSCORMRepository = topicSCORMRepository;
            _currentUserService = currentUserService;
            _userCourseRepository = userCourseRepository;
            _scormCoreRepo = scormCoreRepo;
            _mapper = mapper;
            _userRepository = userRepository;
            _scormObjRepo = scormObjRepo;
            _learnerPrefRepo = learnerPrefRepo;
            _scormNavRepo = scormNavRepo;
            _sectionRepository = sectionRepository;
        }

        private string ConvertToJson(Document document)
        {
            List<string> objectiveList = document?.Objectives?.ObjectiveList.Select(o => o.ObjectiveID).ToList();
            JObject manifestJson = JObject.FromObject(new
            {
                completionThreshold = document.CompletionThreshold,
                dataFromLMS = document.DataFromLMS,
                attemptAbsoluteDurationLimit = document.AttemptAbsoluteDurationLimit,
                objectives = new
                {
                    primaryObjective = new
                    {
                        objectiveID = document?.Objectives?.PrimaryObjective?.ObjectiveID,
                        minNormalizedMeasure = document?.MinNormalizedMeasure
                    },
                    objectiveList
                },
                timeLimitAction = document.TimeLimitAction
            });
            return manifestJson.ToString();
        }

        public async Task<SCORMViewModel> UploadSCORMInSection(int sectionId, IFormFile resource)
        {
            Section section = _sectionRepository.Get(s => s.Id == sectionId && s.IsDeleted != true)
                                                .Include(s => s.SCORMList).FirstOrDefault();
            if (section == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.SectionNotFound, 
                    ErrorMessages.SectionNotFound);
            }
            string originName = resource.FileName;
            ResourceInfoModel resourceInfo = await FileUtils.SaveFileToFolder(resource, isZipFile: true);

            #region get information of scorm
            string pathToCourseSCORMFolder = FileUtils.ExtractZipFile(resourceInfo.pathToFile);
            pathToCourseSCORMFolder = pathToCourseSCORMFolder.Trim();

            string pathToManifest = FileUtils.FindManifestFile(pathToCourseSCORMFolder);

            Document document = new Document(pathToManifest);
            string manifestJson = ConvertToJson(document);

            string url = FileUtils.GetUrlForSCORM(pathToCourseSCORMFolder, "/" + document.IndexPage);
            string pathToFolder = FileUtils.GetFolderUrl(pathToCourseSCORMFolder);
            #endregion

            FileType fileType = resourceInfo.FileType;
            if (fileType == FileType.zip)
            {
                List<string> scormNameList = section.SCORMList.Select(r => r.TitleFromUpload).ToList();
                string titleFromUpload = StringUtils.GetUniqueName(scormNameList, originName);

                var scorm = new SCORM
                {
                    TitleFromManifest = document.Title,
                    TitleFromUpload = titleFromUpload,
                    PathToIndex = url,
                    PathToFolder = pathToFolder,
                    SCORMVersion = document.Version,
                    SectionId = sectionId,
                    ManifestItemData = manifestJson,
                    StandAloneIndexPage = document.StandAloneIndexPage
                };
                await _scormRepository.AddAsync(scorm);
                await _unitOfWork.SaveChangeAsync();

                await FileUtils.UploadUnzipSCORMFolder(pathToFolder);
                return _mapper.Map<SCORMViewModel>(scorm);
            }
            else
            {
                throw new RequestException(HttpStatusCode.UnsupportedMediaType, ErrorCodes.UnsupportedFile,
                        ErrorMessages.UnsupportedFile);
            }
        }

        public async Task<TopicSCORMWithoutCoreViewModel> UploadSCORMInTopic(int topicId, IFormFile resource)
        {
            Topic topic = _topicRepository.Get(t => t.Id == topicId)?
                                          .Include(t => t.TopicSCORMs).FirstOrDefault();
            if (topic == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            string originName = resource.FileName;
            ResourceInfoModel resourceInfo = await FileUtils.SaveFileToFolder(resource, isZipFile: true);

            #region get information of scorm
            string pathToCourseSCORMFolder = FileUtils.ExtractZipFile(resourceInfo.pathToFile);
            pathToCourseSCORMFolder = pathToCourseSCORMFolder.Trim();

            string pathToManifest = FileUtils.FindManifestFile(pathToCourseSCORMFolder);

            Document document = new Document(pathToManifest);
            string manifestJson = ConvertToJson(document);

            string url = FileUtils.GetUrlForSCORM(pathToCourseSCORMFolder, "/" + document.IndexPage);
            string pathToFolder = FileUtils.GetFolderUrl(pathToCourseSCORMFolder);
            #endregion

            FileType fileType = resourceInfo.FileType;
            if (fileType == FileType.zip)
            {
                List<string> scormNameList = topic.TopicSCORMs.Select(r => r.SCORMName).ToList();
                string titleFromUpload = StringUtils.GetUniqueName(scormNameList, originName);

                var scorm = new SCORM
                {
                    TitleFromManifest = document.Title,
                    TitleFromUpload = titleFromUpload,
                    PathToIndex = url,
                    PathToFolder = pathToFolder,
                    SCORMVersion = document.Version,
                    ManifestItemData = manifestJson,
                    StandAloneIndexPage = document.StandAloneIndexPage
                };
                await _scormRepository.AddAsync(scorm);
                await _unitOfWork.SaveChangeAsync();

                TopicSCORM topicSCORM = new TopicSCORM
                {
                    TopicId = topicId,
                    SCORMId = scorm.Id,
                    SCORMName = titleFromUpload
                };
                await _topicSCORMRepository.AddAsync(topicSCORM);

                //update number of learning resources in topic
                topic.NumberOfLearningResources++;

                await _unitOfWork.SaveChangeAsync();

                await FileUtils.UploadUnzipSCORMFolder(pathToFolder);
                return new TopicSCORMWithoutCoreViewModel 
                { 
                    TopicId = topicId,
                    SCORMId = scorm.Id,
                    TopicSCORMId = topicSCORM.Id,
                    SCORMName = topicSCORM.SCORMName,
                    PathToIndex = scorm.PathToIndex,
                    PathToFolder = scorm.PathToFolder,
                    SCORMVersion = scorm.SCORMVersion,
                    StandAloneIndexPage = scorm.StandAloneIndexPage,
                    CreateTime = scorm.CreateTime,
                    CreateBy = scorm.CreateBy
                };
            }
            else
            {
                throw new RequestException(HttpStatusCode.UnsupportedMediaType, ErrorCodes.UnsupportedFile,
                        ErrorMessages.UnsupportedFile);
            }
        }

        //public async Task<TopicSCORMWithoutCoreViewModel> MoveSCORMToTopic(int topicId, int resourceId)
        //{
        //    Topic topic = _topicRepository.Get(t => t.Id == topicId)?
        //                                  .Include(t => t.TopicSCORMs).FirstOrDefault();
        //    if (topic == null)
        //    {
        //        throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.NotFound, ErrorMessages.NotFound);
        //    }
        //    SCORM scorm = await _scormRepository.FindAsync(resourceId);
        //    if (scorm == null)
        //    {
        //        throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.NotFound, ErrorMessages.NotFound);
        //    }
        //    var query = _topicSCORMRepository.Get(ts => ts.TopicId == topicId
        //                                                        && ts.SCORMId == resourceId);
        //    if (query.Any())
        //    {
        //        throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.ResourceIsExistedInTopic,
        //            ErrorMessages.ResourceIsExistedInTopic);
        //    }
        //    List<string> scormNameList = topic.TopicSCORMs.Select(r => r.SCORMName).ToList();
        //    string scormName = StringUtils.GetUniqueName(scormNameList, scorm.TitleFromUpload);

        //    TopicSCORM topicSCORM = new TopicSCORM
        //    {
        //        TopicId = topicId,
        //        SCORMId = resourceId,
        //        SCORMName = scormName
        //    };
        //    await _topicSCORMRepository.AddAsync(topicSCORM);

        //    //update number of learning resources in topic
        //    topic.NumberOfLearningResources++;

        //    await _unitOfWork.SaveChangeAsync();
        //    return new TopicSCORMWithoutCoreViewModel
        //    {
        //        TopicId = topicId,
        //        SCORMId = scorm.Id,
        //        TopicSCORMId = topicSCORM.Id,
        //        SCORMName = topicSCORM.SCORMName,
        //        PathToIndex = scorm.PathToIndex,
        //        PathToFolder = scorm.PathToFolder,
        //        SCORMVersion = scorm.SCORMVersion,
        //        StandAloneIndexPage = scorm.StandAloneIndexPage,
        //        CreateTime = scorm.CreateTime,
        //        CreateBy = scorm.CreateBy
        //    };
        //}

        public async Task DeleteSCORMInTopic(int topicSCORMId)
        {
            var topicSCORM = _topicSCORMRepository.Get(ts => ts.Id == topicSCORMId)
                                                  .Include(ts => ts.Topic).ThenInclude(t => t.Course)
                                                  .Include(ts => ts.SCORM).FirstOrDefault();
            if (topicSCORM == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.ParametersNotMatch,
                    ErrorMessages.ParametersNotMatch);
            }
            Topic topic = topicSCORM.Topic;
            SCORM scorm = topicSCORM.SCORM;

            DateTimeOffset now = DateTimeOffset.Now;
            if (topic.Course.StartTime <= now)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.ResourceDeleteError,
                    ErrorMessages.ResourceDeleteError);
            }

            if (scorm.SectionId == null)
            {
                await _scormRepository.Remove(scorm.Id);
                await FileUtils.DeleteSCORMFile(scorm.PathToFolder, scorm.TitleFromUpload);
            }
            else
            {
                await _topicSCORMRepository.Remove(topicSCORMId);
            }

            //update number of learning resources in topic
            topic.NumberOfLearningResources--;

            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteSCORMInSection(int scormId)
        {
            SCORM scorm = _scormRepository.Get(s => s.Id == scormId && s.SectionId != null)
                .Include(s => s.TopicSCORMs)
                .ThenInclude(ts => ts.Topic).ThenInclude(topic => topic.Course)
                .FirstOrDefault();
            if (scorm == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }

            //this variable change to true if resource is existed in a course has started
            bool flag = false;
            if (scorm.TopicSCORMs.Any())
            {
                DateTimeOffset now = DateTimeOffset.Now;
                List<TopicSCORM> deleteTopicScorm = new();
                foreach (var topicSCORM in scorm.TopicSCORMs)
                {
                    if (topicSCORM.Topic.Course.StartTime <= now) //course is started
                    {
                        flag = true;
                    } else //course is not start
                    {
                        deleteTopicScorm.Add(topicSCORM);
                    }           
                }
                //delete all topicScorm in courses that not start 
                if (deleteTopicScorm.Count > 0)
                {
                    _topicSCORMRepository.RemoveRange(deleteTopicScorm);
                    await _unitOfWork.SaveChangeAsync();

                    //update number of learning resources in topic
                    await UpdateNumOfLearningResourceInTopics(deleteTopicScorm.Select(to => to.TopicId).ToList());
                }
                if (flag)
                {
                    //scorm.IsActive = false;
                    scorm.IsDeleted = true;
                    _scormRepository.Update(scorm);
                }
                else
                {
                    await _scormRepository.Remove(scorm.Id);
                }
            }
            else
            {
                await _scormRepository.Remove(scorm.Id);
            }
            await _unitOfWork.SaveChangeAsync();
            if (!flag)
            {
                await FileUtils.DeleteSCORMFile(scorm.PathToFolder, scorm.TitleFromUpload);
            }
        }

        public async Task<SCORMViewContentModel> ViewContent(int topicSCORMId)
        {
            var topicSCORM = _topicSCORMRepository.Get(ts => ts.Id == topicSCORMId)?
                                                                    .Include(ts => ts.SCORM)?
                                                                    .Include(ts => ts.Topic)?
                                                                    .ThenInclude(t => t.Course)?
                                                                    .FirstOrDefault();

            if (topicSCORM == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.ParametersNotMatch,
                    ErrorMessages.ParametersNotMatch);
            }

            Guid userId = _currentUserService.UserId;
            ActionType type = _userCourseRepository.Get(uc => uc.UserId == userId
                                                     && uc.CourseId == topicSCORM.Topic.CourseId)
                                                    .Select(uc => uc.ActionType).FirstOrDefault();
            SCORM scorm = topicSCORM.SCORM;
            if (type == ActionType.Study)
            {
                var scormCore = _scormCoreRepo.Get(sc => sc.LearnerId == userId && sc.TopicSCORMId == topicSCORMId)
                                              .FirstOrDefault();
                if (scormCore == null)
                {
                    scormCore = await InitSCORMCore(userId, topicSCORMId, scorm.ManifestItemData);
                }
                return new SCORMViewContentModel()
                {
                    Url = scorm.PathToIndex,
                    SCORMCore = _mapper.Map<SCORMCoreViewModel>(scormCore)
                };
            }
            else
            {
                if (scorm.StandAloneIndexPage != null)
                {
                    return new SCORMViewContentModel()
                    {
                        Url = scorm.PathToFolder + "/" + scorm.StandAloneIndexPage
                    };
                }
                else
                {
                    throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.ResourceNotAvaiable,
                        ErrorMessages.ResourceNotAvaiable);
                }
            }

        }

        private async Task<SCORMCore> InitSCORMCore(Guid userId, int topicSCORMId, string manifestDataItem)
        {
            var user = await _userRepository.FindAsync(userId);
            SCORMCoreInitModel initData = null;
            if (manifestDataItem != null)
            {
                initData = JsonConvert.DeserializeObject<SCORMCoreInitModel>(manifestDataItem);
            }
            var SCORMCore = new SCORMCore
            {
                LearnerId = userId,
                TopicSCORMId = topicSCORMId,
                LearnerName = user.FirstName + " " + user.LastName,
                Mode = CMIVocabularyTokens.Mode.Normal, //default Mode
                Credit = CMIVocabularyTokens.Credit.IsCredit, //default Credit
                CompletionStatus = CMIVocabularyTokens.CompletionStatus.Unknown, //default CompletionStatus
                Entry = CMIVocabularyTokens.Entry.Empty, //means that the user has never accessed SCO
                ScaledPassingScore = initData?.Objectives.PrimaryObjective.MinNormalizedMeasure,
                MaxTimeAllowed = initData?.AttemptAbsoluteDurationLimit,
                TimeLimitAction = initData?.TimeLimitAction ?? CMIVocabularyTokens.DefaultTimeLimitAction,
                SuccessStatus = CMIVocabularyTokens.SuccessStatus.Unknown, //default SuccessStatus
                CompletionThreshold = initData?.CompletionThreshold,
                LaunchData = initData?.DataFromLMS,
                TotalTime = CMIVocabularyTokens.DefaultTotalTime,
                //SCORM 1.2
                LessonStatus12 = CMIVocabularyTokens.CoreLessonStatus.NotAttempted
            };
            await _scormCoreRepo.AddAsync(SCORMCore);
            await _unitOfWork.SaveChangeAsync();

            //init default SCORMLearnerPreference
            var SCORMLearnerPreference = new SCORMLearnerPreference
            {
                AudioLevel = CMIVocabularyTokens.LearnerPreference.DefaultAudioLevel,
                AudioCaptioning = CMIVocabularyTokens.LearnerPreference.DefaultAudioCaptioning,
                DeliverySpeed = CMIVocabularyTokens.LearnerPreference.DefaultDeliverySpeed,
                Language = CMIVocabularyTokens.LearnerPreference.DefaultLanguage,
                SCORMCoreId = SCORMCore.Id
            };
            await _learnerPrefRepo.AddAsync(SCORMLearnerPreference);

            //init default navigation
            var SCORMNavigation = new SCORMNavigation
            {
                SCORMCoreId = SCORMCore.Id,
                Request = NavigationVocabularyTokens.Request.None, //default navigation request
                ValidContinue = NavigationVocabularyTokens.ValidState.Unknown, //default valid continue
                ValidPrevious = NavigationVocabularyTokens.ValidState.Unknown //default valid previous
            };
            await _scormNavRepo.AddAsync(SCORMNavigation);

            //init objective
            var objectives = initData?.Objectives;
            var primaryObj = objectives?.PrimaryObjective;
            int count = 0;
            if (primaryObj.ObjectiveID != null)
            {
                var SCORMObjective = new SCORMObjective
                {
                    N = count++,
                    Nid = primaryObj.ObjectiveID,
                    SuccessStatus = CMIVocabularyTokens.SuccessStatus.Unknown, //default SuccessStatus
                    CompletionStatus = CMIVocabularyTokens.CompletionStatus.Unknown, //default CompletionStatus
                    SCORMCoreId = SCORMCore.Id,
                    Status12 = CMIVocabularyTokens.CoreLessonStatus.NotAttempted
                };
                await _scormObjRepo.AddAsync(SCORMObjective);
            }
            var objList = objectives?.ObjectiveList;
            if (objList != null)
            {
                foreach (var objID in objList)
                {
                    var SCORMObjective = new SCORMObjective
                    {
                        N = count++,
                        Nid = objID,
                        SuccessStatus = CMIVocabularyTokens.SuccessStatus.Unknown, //default SuccessStatus
                        CompletionStatus = CMIVocabularyTokens.CompletionStatus.Unknown, //default CompletionStatus
                        SCORMCoreId = SCORMCore.Id
                    };
                    await _scormObjRepo.AddAsync(SCORMObjective);
                }
            }
            await _unitOfWork.SaveChangeAsync();
            return await Task.FromResult(SCORMCore);
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

        public async Task<TopicSCORMWithoutCoreViewModel> UpdateSCORMInTopic(int topicSCORMId, 
            TopicScormUpdateRequestModel requestModel)
        {
            ValidateUtils.CheckStringNotEmpty("resource name", requestModel.SCORMName);
            var topicSCORM = _topicSCORMRepository.Get(ts => ts.Id == topicSCORMId)
                                                  .Include(ts => ts.SCORM)
                                                  .FirstOrDefault();
            if (topicSCORM == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }

            //update
            topicSCORM.SCORMName = requestModel.SCORMName;
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<TopicSCORMWithoutCoreViewModel>(topicSCORM);
        }
    }
}
